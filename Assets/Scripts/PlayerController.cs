using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    InputAction moveAction;
    InputAction jumpAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocityX = moveAction.ReadValue<Vector2>().x * moveSpeed;

        Jump();
    }

    void Jump()
    {
        if (jumpAction.triggered && GroundCheck())
        {
            rb.linearVelocityY = jumpHeight;
        }
    }

    bool GroundCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer); ;
    }
}
