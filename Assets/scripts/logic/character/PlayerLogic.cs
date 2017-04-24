using Assets.scripts;
using logic.character.stats;
using UnityEngine;
using UnityEngine.UI;

namespace logic.character
{
    public class PlayerLogic : MonoBehaviour
    {
        private GameObject _healthBar;
        private GameObject _staminaBar;
        private GameObject _pickupCounter;

        public HitPoints HitPoints { get; private set; }
        public Stamina Stamina { get; private set; }
        public Pickups Pickups { get; private set; }

        public void Start()
        {
            _healthBar = GameObject.Find("Canvas/HealthBar/Mask/Content");
            _staminaBar = GameObject.Find("Canvas/StaminaBar/Mask/Content");
            _pickupCounter = GameObject.Find("Canvas/PickupCounter/PickupCount");

            Image healthBarImage = null;
            if (_healthBar != null) healthBarImage = _healthBar.GetComponent<Image>();
            else Debug.LogWarning("Health Bar not found.");

            Image staminaBarImage = null;
            if (_staminaBar != null) staminaBarImage = _staminaBar.GetComponent<Image>();
            else Debug.LogWarning("Stamina Bar not found.");

            Text pickupCounterText = null;
            if (_pickupCounter != null) pickupCounterText = _pickupCounter.GetComponent<Text>();
            else Debug.LogWarning("Pickup Counter not found.");

            HitPoints = new HitPoints(healthBarImage, 0, GameProperties.PLAYER_MAX_HEALTH);
            Stamina = new Stamina(staminaBarImage, 0, GameProperties.PLAYER_STAMINA);
            Pickups = new Pickups(pickupCounterText);
        }

        public void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.E))
            {
                HitPoints.CurrentValue -= 10;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                HitPoints.CurrentValue += 10;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Pickups.Increment();
            }
            if (!Input.GetKeyDown(KeyCode.B)) return;
            Debug.LogWarning(Pickups.BuildHealer() ? "HEALER SPAWNED BRAH" : "NOT ENOUGH PICKUPS BRAH");
#endif
        }
    }
}