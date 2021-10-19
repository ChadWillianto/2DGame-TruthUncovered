using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float duration = 1f;
    public float playerHealth = 1;
    public float bulletForce = 20f;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource shootAudio;
    public AudioSource deathSound;
    public AudioSource ambient;
    public bool ending = false;
    float m_Timer;
    Vector2 movement;
    Vector2 mousePos;
    Vector2 lookDir;
    
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(movement.x) != 0 || Mathf.Abs(movement.y) != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetFloat("Shoot", 1);
            shootAudio.Play();
            Shoot();
        }
        else
        {
            animator.SetFloat("Shoot", 0);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void GetHit(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            animator.SetBool("Die", true);
            this.enabled = false;
            
            if(playerHealth == 0){
                ambient.Stop();
                deathSound.Play();
            }
            Invoke("Restart", 4.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "WinningCondition")
                {
                    ending = true;
                }
}

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
