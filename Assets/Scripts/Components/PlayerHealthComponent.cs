using System;
using System.Data;
using DefaultNamespace;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    public class PlayerHealthComponent : HealthComponent
    {
        private PlayerController _playerController;
        private SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            GameEventManager.Instance.playerStatEvents.HealthChange += IncreaseMaxHealth;
            GameEventManager.Instance.levelEvents.LevelTimerFinished += DisableHealth;
        }

        private void DisableHealth()
        {
            enabled = false;
        }

        public override void Die()
        {
            if (_currentHealth > 0) return;
            _sr.color = Color.red;
        }

        public override void TakeDamage(float amount)
        {
            if (_playerController.invincibleTimeBuffer > 0) return;
            base.TakeDamage(amount);
            _playerController.MakeInvincible();
            Random.InitState(DateTime.Now.Millisecond);
            GameEventManager.Instance.resourceEvents.OnTakeBlood(Random.Range(3, 8));
            Random.InitState(DateTime.Now.Millisecond);
            GameEventManager.Instance.resourceEvents.OnTakeBones(Random.Range(1, 3));
        }

        private void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            _currentHealth += amount;
        }
    }
}
