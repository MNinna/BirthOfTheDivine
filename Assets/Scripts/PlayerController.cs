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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameEventManager.Instance.inputEvents.MovePressed += UpdatePlayerMoveDirection;
        
        invincibleTimeBuffer = invincibleTime;
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
    }

    public void MakeInvincible()
    {
        invincibleTimeBuffer = invincibleTime;
    }
}
