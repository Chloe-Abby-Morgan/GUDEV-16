using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D baguetteHandle;
    [SerializeField] private float moveSpeed=5f;
    [SerializeField] private float jumpUp=1f;
    [SerializeField] private float jumpDown=2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded=true;
    private Camera cam;
    Vector2 Movement;
    Vector2 MousePos;

    void Start()
    {
        isGrounded = true;
        cam = Camera.main;
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpUp * 1000f);
        }

        rb.gravityScale = rb.linearVelocityY > 0 ? jumpUp : jumpDown;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, whatIsGround);

            MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = MousePos - baguetteHandle.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            baguetteHandle.rotation = angle;
            Movement.x = Input.GetAxisRaw("Horizontal");
            rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);
            baguetteHandle.position = rb.position;
    }
}
