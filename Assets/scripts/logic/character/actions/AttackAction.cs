using UnityEngine;

namespace logic.character.actions
{
    public class AttackAction : GOAPAction
    {
        private bool _attacked;

        public AttackAction()
        {
            addEffect("damagePlayer", true);
            cost = 100f;
        }

        public override void reset()
        {
            _attacked = false;
            target = null;
        }

        public override bool isDone()
        {
            return _attacked;
        }

        public override bool checkProceduralPrecondition(GameObject agent)
        {
            target = GameObject.Find("Player");

            return target != null;
        }

        public override bool perform(GameObject agent)
        {
            Debug.Log("ATTACK!");

            _attacked = true;

            return true;
        }

        public override bool requiresInRange()
        {
            return true;
        }
    }
}