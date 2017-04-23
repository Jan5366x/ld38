using System.Collections.Generic;
using ai.goap;
using UnityEngine;

namespace logic.character
{
    public abstract class Enemy : HitPoints, IGoap
    {
        public float AggroRange;
        public float InteractRange;

        public HashSet<KeyValuePair<string, object>> GetWorldState()
        {
            var worldData = new HashSet<KeyValuePair<string, object>>();
            worldData.Add(new KeyValuePair<string, object>("damagePlayer", false));

            return worldData;
        }

        public abstract HashSet<KeyValuePair<string, object>> CreateGoalState();

        public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
        {
        }

        public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
        {
        }

        public void ActionsFinished()
        {
        }

        public void PlanAborted(GoapAction aborter)
        {
        }

        public virtual bool MoveAgent(GoapAction nextAction)
        {
            var distance = Vector2.Distance(transform.position, nextAction.Target.transform.position);

            if (distance < AggroRange)
            {
                var parent = GetComponentInParent<Transform>();
                parent.position = Vector2.MoveTowards(
                    transform.position,
                    nextAction.Target.transform.position,
                    AggroRange
                );
            }
            if (distance < InteractRange) return false;
            nextAction.SetInRange(true);
            return true;
        }
    }
}