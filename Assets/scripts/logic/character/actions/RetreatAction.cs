using UnityEngine;

namespace logic.character.actions
{
    public class RetreatAction : GOAPAction
    {
        public float MovementSpeed = 0.25f;
        private bool _returned;
        private Vector3 _targetPosition;
        private bool _isReturning;

        public RetreatAction()
        {
            addEffect("returnToInfector", true);
            cost = 200f;
        }

        public void Update()
        {
            var current = GetComponentInParent<Transform>();

            if (_isReturning) current.position = Vector3.MoveTowards(current.position, _targetPosition, MovementSpeed);
            if (current.position == _targetPosition) _isReturning = false;
        }

        public override void reset()
        {
            _returned = false;
            _targetPosition = new Vector2();
            _isReturning = false;
            target = null;
        }

        public override bool isDone()
        {
            return _returned;
        }

        public override bool checkProceduralPrecondition(GameObject agent)
        {
            target = FindClosestInfector();

            return target != null;
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

        public override bool perform(GameObject agent)
        {
            _isReturning = true;
            _targetPosition = target.transform.position;

            _returned = true;
            return true;
        }

        public override bool requiresInRange()
        {
            return false;
        }
    }
}