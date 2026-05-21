using Events;
using UnityEngine;

namespace Managers
{
    public class PlayerStatManager : MonoBehaviour
    {
        public void HealthChange(int amount)
        {
            GameEventManager.Instance.playerStatEvents.OnHealthChange(amount);
        }

        public void DamageChange(int amount)
        {
            GameEventManager.Instance.playerStatEvents.OnDamageChange(amount);
        }

        public void SpeedChange(int amount)
        {
            GameEventManager.Instance.playerStatEvents.OnSpeedChange(amount);
        }

        public void FireRateChange(int amount)
        {
            GameEventManager.Instance.playerStatEvents.OnFireRateChange(amount);
        }

        public void BulletSpeedChange(int amount)
        {
            GameEventManager.Instance.playerStatEvents.OnBulletSpeedChange(amount);
        }
    }
}
