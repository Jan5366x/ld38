using System.Collections;
using logic.character.stats;
using UnityEngine;

namespace logic.character
{
    public class EnemyAttackLogic : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _attackRate = 1.0f;

        private PlayerLogic _player;
        private Animator _animator;
        public HitPoints HitPoints { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Player") return;

            _player = other.GetComponentInParent<PlayerLogic>();
            StartCoroutine("Attack");

            if (_animator != null)
            {
                _animator.SetBool("aggressiv", true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.name != "Player") return;

            StopCoroutine("Attack");

            if (_animator != null)
            {
                _animator.SetBool("aggressiv", false);
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                _player.HitPoints.CurrentValue -= _damage;
                yield return new WaitForSeconds(_attackRate);
            }
        }

        // Use this for initialization
        void Start()
        {
            HitPoints = new HitPoints(null, 0, 50);
            _animator = gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            if (HitPoints.Dead)
            {
                Destroy(gameObject);
            }
        }
    }
}