using System;
using Events;
using UnityEngine;

namespace Managers
{
    public class PlayerStatManager : MonoBehaviour
    {
        public int health;
        public int maxHealth;
        public int damage;
        public int speed;
        public float fireRate;
        public int bulletSpeed;
        public float invincibilityTimer;
        public static PlayerStatManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple instances of PlayerStatManager detected!");
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start()
        {
            GameEventManager.Instance.playerStatEvents.HealthChange += HealthChange;
            GameEventManager.Instance.playerStatEvents.DamageChange += DamageChange;
            GameEventManager.Instance.playerStatEvents.SpeedChange += SpeedChange;
            GameEventManager.Instance.playerStatEvents.FireRateChange += FireRateChange;
            GameEventManager.Instance.playerStatEvents.BulletSpeedChange += BulletSpeedChange;
            GameEventManager.Instance.playerStatEvents.InvincibilityTimerChange += InvincibilityTimerChange;
        }

        public void HealthChange(int amount)
        {
            maxHealth += amount;
            health += amount;
        }

        public void DamageChange(int amount)
        {
            damage += amount;
        }

        public void SpeedChange(int amount)
        {
            speed += amount;
        }

        public void FireRateChange(float amount)
        {
            fireRate -= amount;
        }

        public void BulletSpeedChange(int amount)
        {
            bulletSpeed += amount;
        }

        public void InvincibilityTimerChange(float amount)
        {
            invincibilityTimer += amount;
        }
    }
}
