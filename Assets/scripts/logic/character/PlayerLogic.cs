using ui.StatusBars;
using UnityEngine;
using UnityEngine.UI;

namespace logic.character
{
    public class PlayerLogic : MonoBehaviour
    {
        private GameObject _healthBar;
        private GameObject _staminaBar;

        public HitPoints HitPoints { get; private set; }
        public Stamina Stamina { get; private set; }

        public void Start()
        {
            _healthBar = GameObject.Find("Canvas/HealthBar/Mask/Content");
            _staminaBar = GameObject.Find("Canvas/StaminaBar/Mask/Content");

            HitPoints = new HitPoints(_healthBar.GetComponent<Image>());
            Stamina = new Stamina(_staminaBar.GetComponent<Image>());
        }
    }
}