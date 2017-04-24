using ui.StatusBars;
using UnityEngine;
using UnityEngine.UI;

namespace logic.character.stats
{
    public abstract class BarStat
    {
        private readonly StatusBarController _barController;

        protected BarStat(Image content, int minValue, int maxValue)
        {
            _barController = new StatusBarController(content, this);
            MinValue = minValue;
            MaxValue = maxValue;
        }

        private int _currentValue;

        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                var newValue = Mathf.Clamp(value, MinValue, MaxValue);
                var oldValue = _currentValue;

                if (newValue == oldValue) return;

                _currentValue = newValue;
                ValueChanged(oldValue, _currentValue);

                if (_barController != null)
                {
                    _barController.BarValue = _currentValue;
                }
            }
        }

        public int GetMaxValue()
        {
            return MaxValue;
        }

        public int GetMinValue()
        {
            return MinValue;
        }

        public virtual void ValueChanged(int oldValue, int newValue)
        {
        }

        public int MaxValue { get; protected set; }
        public int MinValue { get; protected set; }
    }
}