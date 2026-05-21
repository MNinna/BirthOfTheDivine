using System;

namespace Events
{
    public class PlayerStatEvents
    {
        public event Action<int> HealthChange;
        public event Action<int> DamageChange;
        public event Action<int> SpeedChange;
        public event Action<int> FireRateChange;
        public event Action<int> BulletSpeedChange;

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

        public virtual void OnFireRateChange(int obj)
        {
            FireRateChange?.Invoke(obj);
        }

        public virtual void OnBulletSpeedChange(int obj)
        {
            BulletSpeedChange?.Invoke(obj);
        }
    }
}
