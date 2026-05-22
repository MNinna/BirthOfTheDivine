using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float preferredDistance = 8f;

    [Header("Dodge")]
    public float dodgeCooldown = 0.5f;
    public float dodgeSpeed = 12f;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float attackCooldown = 1f;

    private Rigidbody2D rb;

    private float lastAttackTime;
    private float lastDodgeTime;

    private bool isDodging;
    private Vector2 dodgeDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Tick(Transform playerTarget)
    {
        player = playerTarget;

        float distance = Vector2.Distance(transform.position, player.position);

        if (isDodging)
            return;

        Vector2 toPlayer = (player.position - transform.position).normalized;
        Vector2 away = -toPlayer;

        if (distance < preferredDistance)
        {
            rb.linearVelocity = away * moveSpeed;

            if (Time.time >= lastDodgeTime + dodgeCooldown)
                StartDodge(away);
        }
        else if (distance > preferredDistance)
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

        if (isDodging)
        {
            rb.linearVelocity = dodgeDirection * dodgeSpeed;
        }
    }

    void StartDodge(Vector2 away)
    {
        isDodging = true;
        lastDodgeTime = Time.time;

        Vector2 perp =
            Random.value > 0.5f
            ? new Vector2(-away.y, away.x)
            : new Vector2(away.y, -away.x);

        dodgeDirection = (away + perp).normalized;

        Invoke(nameof(StopDodge), 0.2f);
    }

    void StopDodge()
    {
        isDodging = false;
    }

    void RangedAttack()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D prb = proj.GetComponent<Rigidbody2D>();

        Vector2 dir = (player.position - firePoint.position).normalized;

        prb.linearVelocity = dir * projectileSpeed;
    }
}