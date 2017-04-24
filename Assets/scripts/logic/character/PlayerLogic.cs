using Assets.scripts;
using logic.character.stats;
using UnityEngine;
using UnityEngine.UI;

namespace logic.character
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _healerObject;

        private GameObject _healthBar;
        private GameObject _staminaBar;
        private GameObject _pickupCounter;

        public HitPoints HitPoints { get; private set; }
        public Stamina Stamina { get; private set; }
        public Pickups Pickups { get; private set; }

        public void Start()
        {
            _healthBar = GameObject.Find("MainUI/HealthBar/Mask/Content");
            _staminaBar = GameObject.Find("MainUI/StaminaBar/Mask/Content");
            _pickupCounter = GameObject.Find("MainUI/PickupCounter/PickupCount");

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

        private void BuildHealer()
        {
            var tileName = GetCurrentTileName();
            var groundTile = GameObject.Find(tileName);
            if (groundTile == null) return;

            var worldData = groundTile.GetComponent<WorldData>();

            if (
                worldData == null
                || !worldData.CanPlaceHealer()
                || !Pickups.BuildHealer()
                || _healerObject == null
            ) return;

            Instantiate(_healerObject, groundTile.transform);
        }

        private string GetCurrentTileName()
        {
            return "G-" + Mathf.RoundToInt(transform.position.x) + "-" + -Mathf.RoundToInt(transform.position.y);
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
            BuildHealer();
#endif
        }
    }
}