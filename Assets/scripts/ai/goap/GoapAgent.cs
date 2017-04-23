using System;
using System.Collections.Generic;
using System.Linq;
using ai.fsm;
using UnityEngine;

namespace ai.goap
{
    public sealed class GoapAgent : MonoBehaviour
    {
        private Fsm _stateMachine;

        private Fsm.FsmState _idleState; // finds something to do
        private Fsm.FsmState _moveToState; // moves to a target
        private Fsm.FsmState _performActionState; // performs an action

        private HashSet<GoapAction> _availableActions;
        private Queue<GoapAction> _currentActions;

        private IGoap _dataProvider;

        private GoapPlanner _planner;


        void Start()
        {
            _stateMachine = new Fsm();
            _availableActions = new HashSet<GoapAction>();
            _currentActions = new Queue<GoapAction>();
            _planner = new GoapPlanner();
            FindDataProvider();
            CreateIdleState();
            CreateMoveToState();
            CreatePerformActionState();
            _stateMachine.PushState(_idleState);
            LoadActions();
        }

        void Update()
        {
            _stateMachine.Update(gameObject);
        }

        public void AddAction(GoapAction action)
        {
            _availableActions.Add(action);
        }

        public GoapAction GetAction(Type action)
        {
            return _availableActions.FirstOrDefault(availableAction => availableAction.GetType() == action);
        }

        public void RemoveAction(GoapAction action)
        {
            _availableActions.Remove(action);
        }

        private bool HasActionPlan()
        {
            return _currentActions.Count > 0;
        }

        private void CreateIdleState()
        {
            _idleState = (fsm, gameObj) =>
            {
                var worldState = _dataProvider.GetWorldState();
                var goal = _dataProvider.CreateGoalState();

                var plan = _planner.Plan(gameObject, _availableActions, worldState, goal);
                if (plan != null)
                {
                    _currentActions = plan;
                    _dataProvider.PlanFound(goal, plan);

                    fsm.PopState();
                    fsm.PushState(_performActionState);
                }
                else
                {
                    Debug.Log("<color=orange>Failed Plan:</color>" + PrettyPrint(goal));
                    _dataProvider.PlanFailed(goal);

                    fsm.PopState();
                    fsm.PushState(_idleState);
                }
            };
        }

        private void CreateMoveToState()
        {
            _moveToState = (fsm, gameObj) =>
            {
                var action = _currentActions.Peek();
                if (action.RequiresInRange() && action.Target == null)
                {
                    Debug.Log(
                        "<color=red>Fatal error:</color> Action requires a target but has none. Planning failed. You did not assign the target in your Action.checkProceduralPrecondition()");
                    fsm.PopState(); // move
                    fsm.PopState(); // perform
                    fsm.PushState(_idleState);
                    return;
                }

                if (_dataProvider.MoveAgent(action))
                {
                    fsm.PopState();
                }
            };
        }

        private void CreatePerformActionState()
        {
            _performActionState = (fsm, gameObj) =>
            {
                if (!HasActionPlan())
                {
                    // no actions to perform
                    Debug.Log("<color=red>Done actions</color>");
                    fsm.PopState();
                    fsm.PushState(_idleState);
                    _dataProvider.ActionsFinished();
                    return;
                }

                GoapAction action = _currentActions.Peek();
                if (action.IsDone())
                {
                    _currentActions.Dequeue();
                }

                if (HasActionPlan())
                {
                    // perform the next action
                    action = _currentActions.Peek();
                    var inRange = !action.RequiresInRange() || action.IsInRange();

                    if (inRange)
                    {
                        var success = action.Perform(gameObj);

                        if (success) return;

                        // action failed, need to plan again
                        fsm.PopState();
                        fsm.PushState(_idleState);
                        _dataProvider.PlanAborted(action);
                    }
                    else
                    {
                        // need to move there first
                        // push moveToState
                        fsm.PushState(_moveToState);
                    }
                }
                else
                {
                    // no actions left, move to plan state
                    fsm.PopState();
                    fsm.PushState(_idleState);
                    _dataProvider.ActionsFinished();
                }
            };
        }

        private void FindDataProvider()
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (!(component is IGoap)) continue;
                _dataProvider = (IGoap) component;
                return;
            }
        }

        private void LoadActions()
        {
            var actions = gameObject.GetComponents<GoapAction>();
            foreach (var goapAction in actions)
            {
                _availableActions.Add(goapAction);
            }
            Debug.Log("Found actions: " + PrettyPrint(actions));
        }

        public static string PrettyPrint(HashSet<KeyValuePair<string, object>> state)
        {
            return state.Aggregate("",
                (current, keyValuePair) => current + keyValuePair.Key + ":" + keyValuePair.Value + ", ");
        }

        public static string PrettyPrint(Queue<GoapAction> actions)
        {
            return actions.Aggregate("", (current, action) => current + action.GetType().Name + "-> ") + "GOAL";
        }

        public static string PrettyPrint(GoapAction[] actions)
        {
            return actions.Aggregate("", (current, action) => current + action.GetType().Name + ", ");
        }

        public static string PrettyPrint(GoapAction action)
        {
            return action.GetType().Name;
        }
    }
}