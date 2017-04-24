using UnityEngine;
using UnityEngine.UI;

namespace logic.character
{
    public class HitPoints : BarStat
    {
        public HitPoints(Image content) : base(content)
        {
            MaxValue = 100;
            MinValue = 0;
        }

        public bool IsDead()
        {
            return CurrentValue >= MinValue;
        }
    }
}