using System.Collections;
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
    [SerializeField] private float dashInputTime = 0.15f;
    private bool isGrounded = true;
    private Vector2 Movement;
    private bool isDashing = false;
    private bool dInputWait;
    private float dashTimer = 0f;
    private float dInputTimer;
    private MusicManager.NoteDirection inD;
    private Vector2 dashDirection;
    public int health;

    void Update()
    {
        if(health < 0)
        {
            Debug.Log("Dead");
        }
        if (dInputWait)
        {
        dInputTimer += Time.deltaTime;

        if (dInputTimer > dashInputTime)
        {
            dInputWait = false;
        }
        else
        {
            if (CheckDashKey(inD))
            {
                dInputWait = false;
                PerformDash(inD);
            }
        }
    }

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

       dInputWait = true;
       inD = dir;
       dInputTimer = 0f;
    }

    void PerformDash(MusicManager.NoteDirection dir)
    {  
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


    bool CheckDashKey(MusicManager.NoteDirection dir)
    {
        switch (dir)
            {
                case MusicManager.NoteDirection.Up:
                    return Input.GetKeyDown(KeyCode.W);

                case MusicManager.NoteDirection.Down:
                    return Input.GetKeyDown(KeyCode.S);

                case MusicManager.NoteDirection.Left:
                    return Input.GetKeyDown(KeyCode.A);

                case MusicManager.NoteDirection.Right:
                    return Input.GetKeyDown(KeyCode.D);
                
            }

        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            StartCoroutine(takingDamage());
        }
    }

    IEnumerator takingDamage()
    {
        yield return new WaitForSeconds(0.2f);
        health -= 1;
    }
}
