using logic.character.stats;
using UnityEngine.UI;

namespace ui.StatusBars
{
    public class StatusBarController
    {
        private float _fillAmount;
        private int _barValue;

        private readonly Image _content;
        private readonly BarStat _stat;

        public int BarValue
        {
            get { return _barValue; }
            set
            {
                _fillAmount = MapStatToBar(value);
                _barValue = value;
                UpdateBar();
            }
        }

        public StatusBarController(Image content, BarStat stat)
        {
            _content = content;
            _stat = stat;
        }

        public void UpdateBar()
        {
            if (_content != null)
            {
                _content.fillAmount = _fillAmount;
            }
        }

        private float MapStatToBar(int statVal)
        {
            return (statVal - _stat.GetMinValue()) * 1.0f / (_stat.GetMaxValue() - _stat.GetMinValue());
        }
    }
}