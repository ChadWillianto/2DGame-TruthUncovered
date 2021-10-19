using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 1;
    public float lifeTime = 0.5f;
    public float bulletForce = 20f;

    private Transform player;
    private Vector2 target;

    IEnumerator Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, bulletForce * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.GetHit(damage);
            var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect.gameObject, 0.5f);
        }
    }
}
