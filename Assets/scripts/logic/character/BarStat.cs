using ui.StatusBars;
using UnityEngine;
using UnityEngine.UI;

namespace logic.character
{
    public abstract class BarStat
    {
        private readonly StatusBarController _barController;


        protected BarStat(Image content)
        {
            _barController = new StatusBarController(content, this);
        }

        private int _currentValue;

        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (_barController != null)
                {
                    _barController.BarValue = value;
                }
                _currentValue = value;
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

        public int MaxValue { get; protected set; }
        public int MinValue { get; protected set; }
    }
}