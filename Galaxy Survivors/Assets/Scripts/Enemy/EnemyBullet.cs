using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float destroyTime;

    public int particleID;


    public void Start()
    {
        Destroy(gameObject, destroyTime);
        Physics.IgnoreLayerCollision(6, 7, true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("HIT");
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().removeHealth(damage);
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.red);
    }
}
