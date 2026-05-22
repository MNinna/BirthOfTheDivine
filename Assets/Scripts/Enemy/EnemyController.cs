using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damage;
    public int bloodReward;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var healthComponent = other.gameObject.GetComponent<IHealthComponent>();
        healthComponent?.TakeDamage(damage);
    }

    public enum EnemyType
    {
        Melee,
        Ranged
    }

    [Header("Enemy Type")]
    public EnemyType enemyType;

    [Header("References")]
    public Transform player;

    private Rigidbody2D rb;

    [Header("General Movement")]
    public float moveSpeed = 3f;
    public float detectionRadius = 8f;

    [Header("Melee Settings")]
    public float meleeAttackRange = 1.5f;

    [Header("Ranged Settings")]
    public float preferredDistance = 8f;
    public float dodgeDistance = 5f;
    public float dodgeSpeed = 12f;
    public float dodgeCooldown = 0.5f;

    [Header("Attack")]
    public float attackCooldown = 1f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    private float lastAttackTime;
    private float lastDodgeTime;

    private bool isDodging;
    private Vector2 dodgeDirection;

    public EnemyMeleeAttack _enemyMeleeAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        //if (distance > detectionRadius)
        //{
        //    rb.linearVelocity = Vector2.zero;
        //    return;
        //}

        switch (enemyType)
        {
            case EnemyType.Melee:
                HandleMeleeEnemy(distance);
                break;

            case EnemyType.Ranged:
                HandleRangedEnemy(distance);
                break;
        }
    }

    void HandleMeleeEnemy(float distance)
    {
        if (distance > meleeAttackRange)
        {
            MoveTowardsPlayer();
        }
    }


    void HandleRangedEnemy(float distance)
    {
        Vector2 toPlayer = (player.position - transform.position).normalized;

        if (isDodging)
            return;

        Vector2 directionFromPlayer =
            (transform.position - player.position).normalized;

        if (distance < preferredDistance)
        {
            rb.linearVelocity = directionFromPlayer * moveSpeed;

            if (Time.time >= lastDodgeTime + dodgeCooldown)
            {
                StartDodge(directionFromPlayer);
            }
        }

        if (distance > preferredDistance)
        {
            rb.linearVelocity = toPlayer * moveSpeed;
        }

        else
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                RangedAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    void StartDodge(Vector2 awayDirection)
    {
        isDodging = true;
        lastDodgeTime = Time.time;

        Vector2 perpendicular =
            Random.value > 0.5f
            ? new Vector2(-awayDirection.y, awayDirection.x)
            : new Vector2(awayDirection.y, -awayDirection.x);

        dodgeDirection = (awayDirection + perpendicular).normalized;

        Invoke(nameof(StopDodge), 0.2f);
    }

    void StopDodge()
    {
        isDodging = false;
    }

    void RangedAttack()
    {
        Debug.Log(name + " used RANGED attack!");

        if (projectilePrefab == null || firePoint == null)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        if (projectileRb != null)
        {
            Vector2 direction =
                (player.position - firePoint.position).normalized;

            projectileRb.linearVelocity = direction * projectileSpeed;
        }
    }

    void MoveTowardsPlayer()
    {
        if (_enemyMeleeAttack.isAttacking) return;
        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    void Update()
    {
        if (isDodging)
        {
            rb.linearVelocity = dodgeDirection * dodgeSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, preferredDistance);
    }
}
