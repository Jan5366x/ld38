using System.Collections;
using Assets.scripts;
using logic.character.stats;
using UnityEngine;
using UnityEngine.UI;

namespace logic.character
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _healerObject;
        [SerializeField] private GameObject _deathMessageBoxPrefab;
       
        private GameObject _mainUi;
        private GameObject _healthBar;
        private GameObject _staminaBar;
        private GameObject _pickupCounter;
        
        private GameObject _deathMessageBox;

        private EnemyAttackLogic _enemyInRange;
        private float _lastAttack;

        public HitPoints HitPoints { get; private set; }
        public Stamina Stamina { get; private set; }
        public Pickups Pickups { get; private set; }

        private bool dead = false;

        public void Start()
        {
            _mainUi = GameObject.Find("MainUI");
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
            return "G-" + (int) transform.position.x + "-" + (int) -transform.position.y;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponentInParent<EnemyAttackLogic>() != null)
            {
                _enemyInRange = other.GetComponentInParent<EnemyAttackLogic>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponentInParent<EnemyAttackLogic>() != null)
            {
                _enemyInRange = null;
            }
        }


        private void triggerDeath()
        {
            if (_mainUi != null && _deathMessageBox == null)
            {
                _deathMessageBox = Instantiate(_deathMessageBoxPrefab, _mainUi.transform);
            }

            ui.TimerController.active = false;
            WorldController.active = false;


            dead = true;
        }

        public void Update()
        {

            if (dead)
                return;

            // update death logic
            if (HitPoints.Dead)
            {
                triggerDeath();
            }

            if (Input.GetButtonDown("Build"))
            {
                BuildHealer();
            }
            if (!Input.GetButton("Attack")) return;

            if (_enemyInRange != null)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (Time.time - _lastAttack < GameProperties.PLAYER_ATTACK_RATE) return;

            _enemyInRange.HitPoints.CurrentValue -= GameProperties.PLAYER_ATTACK;
            _lastAttack = Time.time;
        }
    }
}