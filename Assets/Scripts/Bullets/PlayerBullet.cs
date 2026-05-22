using System;
using Events;
using Managers;

namespace Bullets
{
    public class PlayerBullet : Bullet
    {
        private void Start()
        {
            damage = PlayerStatManager.Instance.damage;
            speed = PlayerStatManager.Instance.bulletSpeed;
        }
    }
}