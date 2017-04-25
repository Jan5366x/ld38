using System.Collections;
using UnityEngine;

namespace world.action
{
    public class SpawnEnemyAction : MonoBehaviour
    {
        public float SpawnDelay = 5f;

        private float timer = 0f;
        public GameObject[] EnemyPrefab;
        private Vector3 _spawnPos;

        private GameObject _player;


        bool active = false;
        public void Start()
        {
            _player = GameObject.Find("Player");
            _spawnPos = transform.position;
        }

        public void Update()
        { 
            if (timer <= 0)
            {
                if (!active || EnemyPrefab.Length == 0)
                    return;

                Debug.Log("SPAWN!");
                Debug.Log(_spawnPos);

                int index = Random.Range(0, EnemyPrefab.Length);
                Debug.Log("spawn:" + index);
                Instantiate(EnemyPrefab[index], _spawnPos, new Quaternion());

                // reset timer
                timer = SpawnDelay;
            }
            else
            {
                // update timer
                timer -= Time.deltaTime;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            active = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            active = false;
        }

}