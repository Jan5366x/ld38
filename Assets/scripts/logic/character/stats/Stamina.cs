using UnityEngine.UI;

namespace logic.character.stats
{
    public class Stamina : BarStat
    {
        public Stamina(Image content, int minValue, int maxValue) : base(content, minValue, maxValue)
        {
            CurrentValue = maxValue;
        }
    }
}