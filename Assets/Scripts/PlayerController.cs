using System;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed;
    private Vector2 moveDirection;
    [SerializeField]
    private float invincibleTime;
    [HideInInspector]
    public float invincibleTimeBuffer;

    [Header("Attacking")] 
    [SerializeField]
    private GameObject attackObj;
    private SpriteRenderer attackSprite;
    private Collider2D attackCollider;
    [Header("Debug")] 
    [SerializeField] 
    private float attackDuration;
    private float attackDurationBuffer;
    [SerializeField] 
    private float attackRate;
    private float attackRateBuffer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameEventManager.Instance.inputEvents.MovePressed += UpdatePlayerMoveDirection;
        GameEventManager.Instance.inputEvents.AttackPressed += Attack;
        
        invincibleTimeBuffer = invincibleTime;
        
        attackSprite = attackObj.GetComponent<SpriteRenderer>();
        attackCollider = attackObj.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdatePlayerMoveDirection(InputAction.CallbackContext direction)
    {
        moveDirection = direction.ReadValue<Vector2>();
    }

    private void Move()
    {
        // Move
        rb.MovePosition(rb.position + moveDirection * (movementSpeed * Time.fixedDeltaTime));
    }

    private void Update()
    {
        invincibleTimeBuffer -= Time.deltaTime;
        attackDurationBuffer -= Time.deltaTime;
        attackRateBuffer -= Time.deltaTime;
        
        if (attackDurationBuffer <= 0) StopAttack();
    }

    public void MakeInvincible()
    {
        invincibleTimeBuffer = invincibleTime;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (attackRateBuffer > 0) return;
        if (!context.performed) return;
        if (attackDurationBuffer > 0) return;
        StartAttack();
    }

    private void StartAttack()
    {
        // Set atk duration
        attackDurationBuffer = attackDuration;
        // Set atk downtime
        attackRateBuffer = attackRate;
        // Attack
        attackCollider.enabled = true;
        attackSprite.enabled = true;
        // Slow player down
    }

    private void StopAttack()
    {
        // Quick fail
        if (!attackCollider.enabled) return;
        // Stop attack
        attackCollider.enabled = false;
        attackSprite.enabled = false;
    }
}
