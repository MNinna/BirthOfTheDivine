using System;

namespace Events
{
    public class PlayerStatEvents
    {
        public event Action<int> HealthChange;
        public event Action<int> DamageChange;
        public event Action<int> SpeedChange;
        public event Action<float> FireRateChange;
        public event Action<int> BulletSpeedChange;
        public event Action<float> InvincibilityTimerChange;

        public virtual void OnHealthChange(int obj)
        {
            HealthChange?.Invoke(obj);
        }

        public virtual void OnDamageChange(int obj)
        {
            DamageChange?.Invoke(obj);
        }

        public virtual void OnSpeedChange(int obj)
        {
            SpeedChange?.Invoke(obj);
        }

        public virtual void OnFireRateChange(float obj)
        {
            FireRateChange?.Invoke(obj);
        }

        public virtual void OnBulletSpeedChange(int obj)
        {
            BulletSpeedChange?.Invoke(obj);
        }

        public virtual void OnInvincibilityTimerChange(float obj)
        {
            InvincibilityTimerChange?.Invoke(obj);
        }
    }
}
