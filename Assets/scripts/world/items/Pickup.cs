using logic.character;
using UnityEngine;

namespace world.items
{
    public class Pickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Player") return;

            var playerLogic = other.GetComponentInParent<PlayerLogic>();
            playerLogic.Pickups.Increment();
            Destroy(gameObject);
        }
    }
}