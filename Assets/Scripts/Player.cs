using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed=5f;
    Vector2 Movement;

    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);
    }
}
