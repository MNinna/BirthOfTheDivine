using System;
using Events;

namespace Bullets
{
    public class PlayerBullet : Bullet
    {
        private void Start()
        {
            GameEventManager.Instance.playerStatEvents.DamageChange += DamageChange;
            GameEventManager.Instance.playerStatEvents.BulletSpeedChange += SpeedChange;
        }
        
        private void DamageChange(int amount)
        {
            damage += amount;
        }
        
        private void SpeedChange(int amount)
        {
            speed += amount;
        }
    }
}