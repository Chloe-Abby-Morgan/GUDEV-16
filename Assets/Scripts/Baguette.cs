using UnityEngine;

public class Baguette : MonoBehaviour
{
    [SerializeField] private bool attacking;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(attacking && collision.CompareTag("enemy"))
        {
            Debug.Log("Hit");
        }
    }
}
