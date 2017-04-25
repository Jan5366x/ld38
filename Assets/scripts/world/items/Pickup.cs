using logic.character;
using UnityEngine;

namespace world.items
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private int _healOnPickup = 3;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Player") return;

            var playerLogic = other.GetComponentInParent<PlayerLogic>();
            playerLogic.Pickups.Increment();
            playerLogic.HitPoints.CurrentValue += _healOnPickup;
            Destroy(gameObject);
        }
    }
}