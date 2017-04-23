using System.Collections;
using UnityEngine;

namespace world.action
{
    public class SpawnEnemyAction : MonoBehaviour
    {
        public float SpawnDelay = 5f;

        private GameObject _player;

        public void Start()
        {
            _player = GameObject.Find("Player");
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.Equals(_player)) StartCoroutine("SpawnEnemies");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.Equals(_player)) StopCoroutine("SpawnEnemies");
        }

        public IEnumerator SpawnEnemies()
        {
            for (;;)
            {
                Debug.Log("SPAWN!");
                yield return new WaitForSeconds(SpawnDelay);
            }
        }
    }
}