using System.Collections;
using UnityEngine;

namespace world.action
{
    public class SpawnEnemyAction : MonoBehaviour
    {
        public float SpawnDelay = 5f;
        public GameObject[] EnemyPrefab;
        private Vector3 _spawnPos;

        private GameObject _player;

        public void Start()
        {
            _player = GameObject.Find("Player");
            _spawnPos = transform.position;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (EnemyPrefab.Length > 0)
                if (other.gameObject.Equals(_player)) StartCoroutine("SpawnEnemies");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (EnemyPrefab.Length > 0)
                if (other.gameObject.Equals(_player)) StopCoroutine("SpawnEnemies");
        }

        public IEnumerator SpawnEnemies()
        {
            for (;;)
            {

                if (EnemyPrefab.Length == 0)
                    break;

                Debug.Log("SPAWN!");
                Debug.Log(_spawnPos);

                int index = Random.Range(0, EnemyPrefab.Length);
                Debug.Log("spawn:" + index);
                Instantiate(EnemyPrefab[index], _spawnPos, new Quaternion());

                //_spawnPos.x += 1f;
                yield return new WaitForSeconds(SpawnDelay);
            }
        }
    }
}