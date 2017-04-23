using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ai.goap
{
    public class GoapPlanner
    {
        public Queue<GoapAction> Plan(
            GameObject agent,
            HashSet<GoapAction> availableActions,
            HashSet<KeyValuePair<string, object>> worldState,
            HashSet<KeyValuePair<string, object>> goal
        )
        {
            foreach (var action in availableActions)
                action.DoReset();

            var usableActions = new HashSet<GoapAction>();
            foreach (var action in availableActions)
                if (action.CheckProceduralPrecondition(agent))
                    usableActions.Add(action);

            var leaves = new List<Node>();

            // build graph
            var start = new Node(null, 0, worldState, null);
            var success = BuildGraph(start, leaves, usableActions, goal);

            if (!success)
            {
                // no plan
                Debug.Log("NO PLAN");
                return null;
            }

            // find cheapest leaf
            Node cheapest = null;
            foreach (var leaf in leaves)
            {
                if (cheapest == null)
                    cheapest = leaf;
                else if (leaf.RunningCost < cheapest.RunningCost)
                    cheapest = leaf;
            }

            // get all parents of the cheapest node
            var result = new List<GoapAction>();
            var node = cheapest;
            while (node != null)
            {
                if (node.Action != null)
                    result.Insert(0, node.Action);
                node = node.Parent;
            }

            var queue = new Queue<GoapAction>();
            foreach (var action in result)
                queue.Enqueue(action);

            return queue;
        }

        /**
        * Returns true if at least one solution was found.
        * The possible paths are stored in the leaves list. Each leaf has a
        * 'runningCost' value where the lowest cost will be the best action
        * sequence.
        */
        private static bool BuildGraph(
            Node parent,
            ICollection<Node> leaves,
            HashSet<GoapAction> usableActions,
            HashSet<KeyValuePair<string, object>> goal
        )
        {
            var foundOne = false;

            // go through all usable actions and check prerequesites against the world state and any hypothetical state changes
            foreach (var action in usableActions)
            {
                if (parent.Action.Preconditions.IsSubsetOf(parent.State))
                {
                    // create new state after action would be performed
                    var newState = CreateNewState(parent.State, action.Effects);
                    var node = new Node(parent, parent.RunningCost + action.Cost, newState, action);

                    if (goal.IsSubsetOf(newState))
                    {
                        // solution found
                        leaves.Add(node);
                        foundOne = true;
                    }
                    else
                    {
                        // keep testing
                        var remaining = Subset(usableActions, action);
                        if (BuildGraph(node, leaves, remaining, goal)) foundOne = true;
                    }
                }
            }

            return foundOne;
        }

        /**
        * Create subset of HashSet, excluding a specific action
        */
        private static HashSet<GoapAction> Subset(IEnumerable<GoapAction> actions, GoapAction exclude)
        {
            var subset = new HashSet<GoapAction>();
            foreach (var action in actions)
                if (!action.Equals(exclude))
                    subset.Add(action);
            return subset;
        }

        private static HashSet<KeyValuePair<string, object>> CreateNewState(
            IEnumerable<KeyValuePair<string, object>> currentState,
            IEnumerable<KeyValuePair<string, object>> stateChange
        )
        {
            var newState = new HashSet<KeyValuePair<string, object>>();
            foreach (var stateKeyValuePair in currentState)
            {
                newState.Add(new KeyValuePair<string, object>(stateKeyValuePair.Key, stateKeyValuePair.Value));
            }

            foreach (var change in stateChange)
            {
                // update kvp if it exists, add if it doesn't
                if (newState.Any(newStateKeyValuePair => newStateKeyValuePair.Key == change.Key))
                {
                    newState.RemoveWhere(pair => pair.Key.Equals(change.Key));
                    newState.Add(new KeyValuePair<string, object>(change.Key, change.Value));
                }
                else newState.Add(new KeyValuePair<string, object>(change.Key, change.Value));
            }

            return newState;
        }

        /**
        * Used for building up the graph and holding the running costs of actions.
        */
        private class Node
        {
            public readonly Node Parent;
            public readonly float RunningCost;
            public readonly HashSet<KeyValuePair<string, object>> State;
            public readonly GoapAction Action;

            public Node(Node parent, float runningCost, HashSet<KeyValuePair<string, object>> state, GoapAction action)
            {
                Parent = parent;
                RunningCost = runningCost;
                State = state;
                Action = action;
            }
        }
    }
}