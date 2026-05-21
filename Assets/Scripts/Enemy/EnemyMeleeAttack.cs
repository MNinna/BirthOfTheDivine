using System.Collections;
using UnityEngine;

public class EnemyMeleeAI : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float chargeSpeed = 7f;

    [Header("Ranges")]
    public float detectionRange = 8f;
    public float attackRange = 1.5f;

    [Header("Attack Timing")]
    public float attackCooldown = 2f;

    private bool isAttacking;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {

        if (player == null || isAttacking) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > detectionRange)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(DoAttackDecision());
        }
        else
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * walkSpeed;
        }
    }

    IEnumerator DoRandomAttack()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        int roll = Random.Range(0, 5);

        switch (roll)
        {
            case 0:
                yield return StartCoroutine(ChargeDashHeavy());
                break;

            case 1:
                yield return StartCoroutine(DashSlashThrough());
                break;

            case 2:
                yield return StartCoroutine(SmallWalkAttack());
                break;

            case 3:
                yield return StartCoroutine(RunChargeHeavy());
                break;

            case 4:
                yield return StartCoroutine(JumpToPositionAOE());
                break;
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }
    IEnumerator DoAttackDecision()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > 6f)
        {
            if (Random.value > 0.5f)
                yield return StartCoroutine(JumpToPositionAOE());
            else
                yield return StartCoroutine(DashSlashThrough());
        }

        else if (dist > 2f)
        {
            if (Random.value > 0.5f)
                yield return StartCoroutine(ChargeDashHeavy());
            else
                yield return StartCoroutine(RunChargeHeavy());
        }

        else
        {
            if (Random.value > 0.5f)
                yield return StartCoroutine(SmallWalkAttack());
            else
                yield return StartCoroutine(ChargeDashHeavy());
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }

    // 1. charge -> dash -> heavy attack
    IEnumerator ChargeDashHeavy()
    {
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.6f);
        Vector2 dir = (player.position - transform.position).normalized;

        float dashDistance = 2.5f;
        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + dir * dashDistance;

        float dashTime = 0.2f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / dashTime;
            transform.position = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;

        HeavyAttack();

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1.0f);
    }

    // 2. dash through player
    IEnumerator DashSlashThrough()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        float dashDistance = 4f;
        Vector2 startPos = transform.position;
        Vector2 targetPos = (Vector2)player.position + dir * dashDistance;

        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.15f);

        float t = 0f;
        float duration = 0.25f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        SlashAttack();
    }

    // 3. walk to player -> small attack
    IEnumerator SmallWalkAttack()
    {
        while (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * walkSpeed;

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);

        SmallAttack();
    }

    // 4. run -> charge -> heavy attack
    IEnumerator RunChargeHeavy()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        rb.linearVelocity = dir * runSpeed;
        yield return new WaitForSeconds(0.4f);

        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);

        dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * chargeSpeed;

        yield return new WaitForSeconds(0.25f);

        rb.linearVelocity = Vector2.zero;

        RunHeavyAttack();
    }

    // 5. jump to position aoe
    IEnumerator JumpToPositionAOE()
    {
        Vector2 targetPos = player.position;

        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);

        Vector2 start = transform.position;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            transform.position = Vector2.Lerp(start, targetPos, t);
            yield return null;
        }

        AOEAttack(targetPos);
    }


    void SmallAttack()
    {
        Debug.Log("Small Attack");
    }

    void HeavyAttack()
    {
        Debug.Log("Charge-Dash-Heavy Attack");
    }

    void RunHeavyAttack()
    {
        Debug.Log("Run-Heavy Attack");
    }

    void SlashAttack()
    {
        Debug.Log("Slash Through Attack");
    }

    void AOEAttack(Vector2 pos)
    {
        Debug.Log("AOE at " + pos);
    }
}
