using UnityEngine;

public class Baguette : MonoBehaviour
{
    public bool canAttack;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(canAttack && collision.CompareTag("enemy"))
        {
            Debug.Log("Hit"); 
        }
    }
}
