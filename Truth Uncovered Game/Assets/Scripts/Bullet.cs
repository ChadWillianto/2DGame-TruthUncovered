using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 1;
    public float lifeTime = 0.5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.GetHit(damage);
            var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect.gameObject, 0.5f);
        }

        EnemyMovementStationary enemyStationary = collision.GetComponent<EnemyMovementStationary>();
        if (enemyStationary != null)
        {
            enemyStationary.GetHit(damage);
            var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect.gameObject, 0.5f);
        }

        if (collision.tag == "foreground")
            {
                Destroy(gameObject);

            }

    }
}
