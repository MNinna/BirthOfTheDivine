using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Upgrades
{
    public class Upgrade : MonoBehaviour
    {
        [SerializeField] private int bloodCost;
        
        public void HealthChange(int amount)
        {
            if (!CanBuy()) return;
            GameEventManager.Instance.playerStatEvents.OnHealthChange(amount);
            gameObject.SetActive(false);
        }

        public void DamageChange(int amount)
        {
            if (!CanBuy()) return;
            GameEventManager.Instance.playerStatEvents.OnDamageChange(amount);
            gameObject.SetActive(false);
        }

        public void SpeedChange(int amount)
        {
            if (!CanBuy()) return;
            GameEventManager.Instance.playerStatEvents.OnSpeedChange(amount);
            gameObject.SetActive(false);
        }

        public void FireRateChange(float amount)
        {
            if (!CanBuy()) return;
            GameEventManager.Instance.playerStatEvents.OnFireRateChange(amount);
            gameObject.SetActive(false);
        }

        public void BulletSpeedChange(int amount)
        {
            if (!CanBuy()) return;
            GameEventManager.Instance.playerStatEvents.OnBulletSpeedChange(amount);
            gameObject.SetActive(false);
        }

        public void InvincibilityTimerChange(float amount)
        {
            if (!CanBuy()) return;
            GameEventManager.Instance.playerStatEvents.OnInvincibilityTimerChange(amount);
            gameObject.SetActive(false);
        }

        private bool CanBuy()
        {
            if (bloodCost > GameEventManager.Instance.resourceEvents.OnGetBlood()) return false;
            GameEventManager.Instance.resourceEvents.OnTakeBlood(bloodCost);
            return true;
        }
    }
}
