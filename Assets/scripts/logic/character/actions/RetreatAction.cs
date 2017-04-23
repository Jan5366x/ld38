using ai.goap;
using UnityEngine;

namespace logic.character.actions
{
    public class RetreatAction : GoapAction
    {
        public float MovementSpeed = 0.5f;
        private bool _returned = false;
        private Vector3 _targetPosition;
        private bool _isReturning = false;

        public RetreatAction()
        {
            AddEffect("returnToInfector", true);
            Cost = 200f;
        }

        public void Update()
        {
            var current = GetComponentInParent<Transform>();

            if (_isReturning) current.position = Vector3.MoveTowards(current.position, _targetPosition, MovementSpeed);
            if (current.position == _targetPosition) _isReturning = false;
        }

        public override void Reset()
        {
            _returned = false;
            _targetPosition = new Vector2();
            _isReturning = false;
            Target = null;
        }

        public override bool IsDone()
        {
            return _returned;
        }

        public override bool CheckProceduralPrecondition(GameObject agent)
        {
            Target = FindClosestInfector();

            return Target != null;
        }

        private GameObject FindClosestInfector()
        {
            var infectors = GameObject.FindGameObjectsWithTag("Infector");
            var parentPosition = GetComponentInParent<Transform>().position;
            var closest = new GameObject();
            var shortestDistance = 1.0f;
            var found = false;

            foreach (var infector in infectors)
            {
                var distance = Vector3.Distance(parentPosition, infector.transform.position);
                if (shortestDistance != -1.0f && !(distance < shortestDistance)) continue;

                closest = infector;
                shortestDistance = distance;
                found = true;
            }

            return found ? closest : null;
        }

        public override bool Perform(GameObject agent)
        {
            var enemy = agent.GetComponent<Enemy>();

            _isReturning = true;
            _targetPosition = Target.transform.position;

            _returned = true;
            return true;
        }

        public override bool RequiresInRange()
        {
            return true;
        }
    }
}