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

            Image healthBarImage = null;
            if (_healthBar != null) healthBarImage = _healthBar.GetComponent<Image>();
            else Debug.LogWarning("Health Bar not found.");

            Image staminaBarImage = null;
            if (_staminaBar != null) staminaBarImage = _staminaBar.GetComponent<Image>();
            else Debug.LogWarning("Stamina Bar not found.");

            HitPoints = new HitPoints(healthBarImage);
            Stamina = new Stamina(staminaBarImage);
        }
    }
}