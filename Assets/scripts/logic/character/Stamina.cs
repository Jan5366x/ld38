using Assets.scripts;
using UnityEngine.UI;

namespace logic.character
{
    public class Stamina : BarStat
    {
        public Stamina(Image content) : base(content)
        {
            MaxValue = GameProperties.PLAYER_STAMINA;
            MinValue = 0;
        }
    }
}