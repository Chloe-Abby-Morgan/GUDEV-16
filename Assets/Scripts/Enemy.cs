using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed=5f;
    public TimingManager Tim;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Tim = GameObject.FindGameObjectWithTag("Tim").GetComponent<TimingManager>();
    }
    void Update()
    {
        if(!Tim.showingUI)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "attack" || collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
