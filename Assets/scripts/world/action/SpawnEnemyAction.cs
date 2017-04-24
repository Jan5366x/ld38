using System.Collections;
using UnityEngine;

namespace world.action
{
    public class SpawnEnemyAction : MonoBehaviour
    {
        public float SpawnDelay = 5f;
        public GameObject EnemyPrefab;
        private Vector3 _spawnPos;

        private GameObject _player;

        public void Start()
        {
            _player = GameObject.Find("Player");
            _spawnPos = transform.position;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (EnemyPrefab != null)
                if (other.gameObject.Equals(_player)) StartCoroutine("SpawnEnemies");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (EnemyPrefab != null)
                if (other.gameObject.Equals(_player)) StopCoroutine("SpawnEnemies");
        }

        public IEnumerator SpawnEnemies()
        {
            for (;;)
            {
                Debug.Log("SPAWN!");
                Debug.Log(_spawnPos);

                Instantiate(EnemyPrefab, _spawnPos, new Quaternion());

                _spawnPos.x += 1f;
                yield return new WaitForSeconds(SpawnDelay);
            }
        }
    }
}