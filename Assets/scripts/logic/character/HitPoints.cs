using UnityEngine;

namespace logic.character
{
    public class HitPoints : MonoBehaviour
    {
        public int StartingHitPoints = 100;
        private int _currentHitPoints;
        private bool _dead;


        // Use this for initialization
        public void Start()
        {
            _currentHitPoints = StartingHitPoints;
            if (_currentHitPoints > 0) _dead = false;
        }

        // Update is called once per frame
        public void Update()
        {
            if (_currentHitPoints <= 0) _dead = true;
        }

        public bool IsDead()
        {
            return _dead;
        }

        public void AdjustHealth(int damage)
        {
            _currentHitPoints += damage;
        }
    }
}