using DefaultNamespace;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    private PlayerController _playerController;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
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
}
