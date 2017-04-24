using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class TimerController : MonoBehaviour
    {
        private static Text _timerText;
        private float _startTime;

        private void Start()
        {
            var timerObj = GameObject.Find("MainUI/Timer");
            if (timerObj != null) _timerText = timerObj.GetComponent<Text>();

            _startTime = Time.time;
        }

        private void FixedUpdate()
        {
            var elapsed = Time.time - _startTime;

            var minutes = Mathf.RoundToInt(Mathf.Floor(elapsed / 60));
            var seconds = Mathf.RoundToInt(elapsed%60);

            if (_timerText != null)
            {
                    _timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
            }
        }
    }
}