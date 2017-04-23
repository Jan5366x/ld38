using ai.goap;
using UnityEngine;

namespace logic.character.actions
{
    public class AttackAction : GoapAction
    {
        private bool _attacked = false;

        public AttackAction()
        {
            AddEffect("damagePlayer", true);
            Cost = 100f;
        }

        public override void Reset()
        {
            _attacked = false;
            Target = null;
        }

        public override bool IsDone()
        {
            return _attacked;
        }

        public override bool CheckProceduralPrecondition(GameObject agent)
        {
            Target = GameObject.Find("Player");

            return Target != null;
        }

        public override bool Perform(GameObject agent)
        {
            Debug.Log("ATTACK!");

            _attacked = true;

            return true;
        }

        public override bool RequiresInRange()
        {
            return true;
        }
    }
}