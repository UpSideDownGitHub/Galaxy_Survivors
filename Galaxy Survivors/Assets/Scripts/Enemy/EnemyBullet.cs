using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float destroyTime;

    public int particleID;

    // called before the first update frame
    public void Start()
    {
        Destroy(gameObject, destroyTime);
        Physics.IgnoreLayerCollision(6, 7, true);
    }

    // when a collision with a trigger hasppens
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if collided with a player
        if (collision.CompareTag("Player"))
        {
            // damage the player and die
            collision.gameObject.GetComponent<PlayerHealth>().removeHealth(damage);
            Destroy(gameObject);
        }
        // if collided with a player projectile then die as been hit
        else if (collision.CompareTag("PlayerProj"))
        {
            Destroy(gameObject);
        }
    }

    // when destroyed spawn a explosion particle
    public void OnDestroy()
    {
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.red);
    }
}
