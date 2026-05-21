using System;
using DefaultNamespace;
using Events;
using UnityEngine;

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
        }

        private void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            _currentHealth += amount;
        }
    }
}
