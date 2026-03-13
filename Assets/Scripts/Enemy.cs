using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed=5f;
    [SerializeField] private SpriteRenderer Sr;
    [SerializeField] private AudioSource AS;
    public TimingManager Tim;
    GameObject player;
    public Player plyer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        plyer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Tim = GameObject.FindGameObjectWithTag("Tim").GetComponent<TimingManager>();
    }
    void Update()
    {
        if(!plyer.playing)
        {
            return;
        }
        if(!Tim.showingUI)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Sr.flipX = direction.x > 0;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "attack" || collision.tag == "Player")
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        AS.Play();
        Score.Points += 1;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
