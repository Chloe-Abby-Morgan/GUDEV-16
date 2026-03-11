using System.Collections;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    private Transform Player;
    private bool shooting=false;
    private Rigidbody2D rb;
    [SerializeField] private float speed=5f;
    [SerializeField] private float stoppingDistance=15f;
    [SerializeField] private float bulletSpeed=20f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Rigidbody2D shooter;
    [SerializeField] private float fireInterval=2f;
    public TimingManager Tim;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Tim = GameObject.FindGameObjectWithTag("Tim").GetComponent<TimingManager>();
    }

    void Update()
    {
        if(!Tim.showingUI)
        {
            Vector3 lookDir = Player.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            shooter.rotation = angle;
        }

        if (Vector2.Distance(transform.position, Player.position) > stoppingDistance)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            if(shooting == false)
            {
                StartCoroutine(Shooter());
            }
        }
    }

    IEnumerator Shooter()
    {
        shooting = true;
        yield return new WaitForSeconds(fireInterval);
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = (Player.position - shootingPoint.position).normalized;
        rb.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
        shooting = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player" || collision.tag=="attack")
        {
            Destroy(gameObject);
        }
    }
}
