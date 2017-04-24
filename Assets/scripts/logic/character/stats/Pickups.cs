using Assets.scripts;
using UnityEngine.UI;

namespace logic.character.stats
{
    public class Pickups
    {
        public Pickups(Text counterText)
        {
            _counterText = counterText;
            _counterValue = GameProperties.PLAYER_STARTING_PICKUPS;

            UpdateCounter();
        }

        private readonly Text _counterText;
        private int _counterValue;

        public int CounterValue
        {
            get { return _counterValue; }
            private set
            {
                if (value == _counterValue) return;
                _counterValue = value;
                UpdateCounter();
            }
        }

        private void UpdateCounter()
        {
            if (_counterText != null)
            {
                _counterText.text = _counterValue.ToString();
            }
        }

        public void Increment()
        {
            CounterValue += 1;
        }

        public bool BuildHealer()
        {
            var constructionCost = GameProperties.HEALER_CONSTRUCTION_PICKUP_COST;
            if (CounterValue < constructionCost) return false;

            CounterValue -= GameProperties.HEALER_CONSTRUCTION_PICKUP_COST;
            return true;
        }
    }
}