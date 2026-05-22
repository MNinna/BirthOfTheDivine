using Events;
using UnityEngine;

public class Upgrade : MonoBehaviour
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

    public void FireRateChange(float amount)
    {
        GameEventManager.Instance.playerStatEvents.OnFireRateChange(amount);
    }

    public void BulletSpeedChange(int amount)
    {
        GameEventManager.Instance.playerStatEvents.OnBulletSpeedChange(amount);
    }

    public void InvincibilityTimerChange(float amount)
    {
        GameEventManager.Instance.playerStatEvents.OnInvincibilityTimerChange(amount);
    }
}
