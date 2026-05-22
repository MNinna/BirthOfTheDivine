using UnityEngine;

namespace DefaultNamespace
{
    public class HealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField]
        protected int _currentHealth;
        [SerializeField]
        protected int _maxHealth;

        public virtual void Heal(int amount)
        {
            _currentHealth += amount;
            ClampHealth();
        }

        public virtual void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            Die();
        }

        public virtual void Die()
        {
            if (_currentHealth > 0) return;
            Destroy(gameObject);
            GetComponent<ParticleThingo>().SpawnParticle();
        }

        public virtual void ClampHealth()
        {
            if (_currentHealth <= _maxHealth) return;
            _currentHealth = _maxHealth;
        }
        
        
    }
}