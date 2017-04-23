using UnityEngine;

namespace movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public float SpeedFactorWalk = 1f;
        public float SpeedFactorRun = 2f;
        public bool SprintOverride;

        private Animator _animator;

        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized
                                 * Time.deltaTime;

            if (Input.GetButton("Sprint") || SprintOverride)
                movementVector *= SpeedFactorRun;
            else
                movementVector *= SpeedFactorWalk;

            _animator.SetFloat("velocityX", movementVector.normalized.x);
            _animator.SetFloat("velocityY", movementVector.normalized.y);
            _animator.SetFloat("speed", movementVector.normalized.magnitude);

            transform.Translate(movementVector);
        }
    }
}

/**
* unused Dev code
*/
//var direction = -1;
//            if (movementVector.y > 0 && movementVector.x < movementVector.y)
//            {
//                direction = 2;
//            }
//            else if (movementVector.y < 0 && movementVector.x > movementVector.y)
//            {
//                direction = 0;
//            }
//            else if (movementVector.x > 0)
//            {
//                direction = 1;
//            }
//            else if (movementVector.x < 0)
//            {
//                direction = 3;
//            }
//
//            if (direction != -1)
//            {
//                _animator.SetInteger("direction", direction);
//            }