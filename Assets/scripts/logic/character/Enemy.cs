//using System.Collections.Generic;
//using UnityEngine;
//
//namespace logic.character
//{
//    public abstract class Enemy : HitPoints, IGOAP
//    {
//        public int speed;
//
//        protected float terminalSpeed;
//        protected float initialSpeed;
//        protected float acceleration;
//        protected float minDist = 1.5f;
//        protected float aggroDist = 5f;
//
//        public HashSet<KeyValuePair<string, object>> getWorldState()
//        {
//            var worldData = new HashSet<KeyValuePair<string, object>>();
//            worldData.Add(new KeyValuePair<string, object>("damagePlayer", false));
//
//            return worldData;
//        }
//
//        public abstract HashSet<KeyValuePair<string, object>> createGoalState();
//
//        public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
//        {
//        }
//
//        public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
//        {
//        }
//
//        public void actionsFinished()
//        {
//        }
//
//        public void planAborted(GOAPAction aborter)
//        {
//        }
//
//        public virtual bool moveAgent(GOAPAction nextAction)
//        {
//            var dist = Vector3.Distance(transform.position, nextAction.target.transform.position);
//            if (dist < aggroDist)
//            {
//                Vector3 moveDirection = nextAction.target.transform.position - transform.position;
//
//                setSpeed(speed);
//
//                if (initialSpeed < terminalSpeed)
//                    initialSpeed += acceleration;
//
//                Vector3 newPosition = moveDirection * initialSpeed * Time.deltaTime;
//                transform.position += newPosition;
//            }
//            if (!(dist <= minDist)) return false;
//
//            nextAction.setInRange(true);
//        }
//
//        public void setSpeed(float val)
//        {
//            terminalSpeed = val / 10;
//            initialSpeed = (val / 10) / 2;
//            acceleration = (val / 10) / 4;
//        }
//    }
//}