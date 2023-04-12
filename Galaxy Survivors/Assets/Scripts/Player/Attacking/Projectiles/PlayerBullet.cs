using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerBullet : MonoBehaviour
{
    public float damage;

    public int particleID;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
            Destroy(gameObject);
        }
    }
}
