using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damage;

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

    [Header("Charged Ranged Attack")]
    public float chargedAttackCooldown = 4f;
    public float chargeTime = 1.2f;

    private float lastChargedAttackTime;
    private bool isChargingAttack;
    public bool chargedAttack;

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

        rb.linearVelocity = Vector2.zero;

        if (!isChargingAttack && Time.time >= lastChargedAttackTime + chargedAttackCooldown)
        {
            StartCoroutine(ChargedDoubleShot());
            lastChargedAttackTime = Time.time;
        }

        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            RangedAttack();
            lastAttackTime = Time.time;
        }
    }

    IEnumerator ChargedDoubleShot()
    {
        isChargingAttack = true;

        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(chargeTime);

        if (player == null)
        {
            isChargingAttack = false;
            yield break;
        }

        Vector2 baseDir =
            (player.position - firePoint.position).normalized;

        Vector2 perp = new Vector2(-baseDir.y, baseDir.x);

        float spread = 0.4f;

        SpawnProjectile(baseDir + perp * spread);

        SpawnProjectile(baseDir - perp * spread);

        chargedAttack = true;

        yield return new WaitForSeconds(0.4f);

        chargedAttack = false;
        isChargingAttack = false;
    }

    void SpawnProjectile(Vector2 direction)
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed * 1.2f;
        }
    }

    void StartDodge(Vector2 awayDirection)
    {
        if (isChargingAttack) return;

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
        if (isChargingAttack) return;

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
        if (isChargingAttack) return;
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
