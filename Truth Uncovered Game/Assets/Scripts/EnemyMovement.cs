using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource onDeathAudio;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool right;
    float scale = 3f;
    public float stopDistance = 3f;
    public float retreatDistance = 2f;
    public float range = 5f;
    public float moveSpeed = 5f;
    public float startShoot = 1f;
    public int enemyHealth = 1;
    private float reload;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        reload = startShoot;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stopDistance && Vector2.Distance(transform.position, player.position) < range)
        {
            animator.SetFloat("Shoot", 1);
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, player.position) < stopDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            animator.SetFloat("Shoot", 1);
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            animator.SetFloat("Shoot", 1);
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }

        if(reload <= 0 && Vector2.Distance(transform.position, player.position) < range)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            reload = startShoot;
        }
        else
        {
            reload -= Time.deltaTime;
        }

        //Enemy Pathing
        if (right)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            transform.localScale = new Vector2(scale, scale);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            transform.localScale = new Vector2(-scale, scale);
        }
    }

    public void GetHit(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {   
            onDeathAudio.Play();
            animator.SetBool("Die", true);
            this.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Boundary")
                {
                if (right)
                        right = false;
                else
                        right = true;
                }
        }
}
