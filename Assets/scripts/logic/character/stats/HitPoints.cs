using UnityEngine;
using UnityEngine.UI;

namespace logic.character.stats
{
    public class HitPoints : BarStat
    {
        public bool Dead { get; private set; }

        public HitPoints(Image content, int minValue, int maxValue) : base(content, minValue, maxValue)
        {
            CurrentValue = maxValue;
        }

        public override void ValueChanged(int oldValue, int newValue)
        {
            if (newValue <= MinValue)
            {
                Die();
            }
        }

        private void Die()
        {
            Dead = true;
            Debug.LogWarning("YOU ARE DEAD!");
        }
    }
}