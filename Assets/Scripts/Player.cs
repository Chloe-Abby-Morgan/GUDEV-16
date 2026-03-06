using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed=5f;
    [SerializeField] private float jumpUp=1f;
    [SerializeField] private float jumpDown=2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    public bool isGrounded=true;
    Vector2 Movement;

    void Start()
    {
        isGrounded = true;
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpUp * 1000f);
        }

         rb.gravityScale = rb.linearVelocityY > 0 ? jumpUp : jumpDown;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, whatIsGround);
        Movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);
    }
}
