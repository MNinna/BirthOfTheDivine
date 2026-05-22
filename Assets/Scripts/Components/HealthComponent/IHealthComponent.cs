using UnityEngine;

public interface IHealthComponent
{
    /// <summary>
    /// Heal the character.
    /// </summary>
    /// <param name="amount">Amount of health to heal.</param>
    public void Heal(int amount);
    
    /// <summary>
    /// Damage the character.
    /// </summary>
    /// <param name="amount">Amount of health to remove.</param>
    public void TakeDamage(int amount);

    /// <summary>
    /// dies.
    /// </summary>
    public void Die();
    
    /// <summary>
    /// Used to clamp health to the maximum.
    /// </summary>
    /// <param name="amount"></param>
    public void ClampHealth();
}
