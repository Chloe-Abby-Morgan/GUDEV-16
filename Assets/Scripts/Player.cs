using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D[] atackRange = new Rigidbody2D[3];
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpUp = 1f;
    [SerializeField] private float jumpDown = 2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.5f;
    private bool isGrounded = true;
    private Vector2 Movement;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector2 dashDirection;

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpUp * 1000f);
        }

        rb.gravityScale = rb.linearVelocityY > 0 ? jumpUp : jumpDown;

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (!isDashing && dashCooldownTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                BeginDash(Vector2.up);
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                BeginDash(Vector2.down);
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                BeginDash(Vector2.left);
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                BeginDash(Vector2.right);
            }
        }

        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            rb.linearVelocity = dashDirection * dashSpeed;

            if (dashTimer >= dashTime)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }

            return; 
        }

        Movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + Movement * moveSpeed * Time.deltaTime);
    }

    void BeginDash(Vector2 dir)
    {
        isDashing = true;
        dashTimer = 0f;
        dashDirection = dir.normalized;
    }
}
