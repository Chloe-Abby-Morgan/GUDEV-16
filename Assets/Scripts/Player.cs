using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject[] atackRange = new GameObject[3];
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpUp = 1f;
    [SerializeField] private float jumpDown = 2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.15f;
    private bool isGrounded = true;
    private Vector2 Movement;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector2 dashDirection;

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpUp * 1000f);
        }

        rb.gravityScale = rb.linearVelocityY > 0 ? jumpUp : jumpDown;

        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            rb.linearVelocity = dashDirection * dashSpeed;

            if (dashTimer >= dashTime)
            {
                isDashing = false;
            }

            return;
        }
        else
        {
            atackRange[0].SetActive(false);
            atackRange[1].SetActive(false);
            atackRange[2].SetActive(false);
            atackRange[3].SetActive(false);
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

    public void DashFromMusic(MusicManager.NoteDirection dir)
    {
        if (isDashing)
        {
            return;
        }

        switch (dir)
        {
            case MusicManager.NoteDirection.Up:
                BeginDash(Vector2.up);
                atackRange[0].SetActive(true);
                break;

            case MusicManager.NoteDirection.Down:
                BeginDash(Vector2.down);
                atackRange[1].SetActive(true);
                break;

            case MusicManager.NoteDirection.Left:
                BeginDash(Vector2.left);
                atackRange[3].SetActive(true);
                break;

            case MusicManager.NoteDirection.Right:
                BeginDash(Vector2.right);
                atackRange[2].SetActive(true);
                break;
        }
    }
}
