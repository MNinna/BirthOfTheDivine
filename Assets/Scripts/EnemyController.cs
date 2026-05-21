using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var healthComponent = other.gameObject.GetComponent<IHealthComponent>();
        healthComponent?.TakeDamage(damage);
    }
}
