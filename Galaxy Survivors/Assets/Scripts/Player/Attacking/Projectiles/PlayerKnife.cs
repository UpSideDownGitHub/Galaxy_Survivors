using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerKnife : MonoBehaviour
{
    public float damage;

    public int maxPeirce;
    public int pierceCount;

    public int particleID;

    public void Start()
    {
        pierceCount = maxPeirce;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (pierceCount - 1 >= 0)
            {
                pierceCount--;
                collision.GetComponent<EnemyHealth>().takeDamage(damage);
            }
            else
            {
                collision.GetComponent<EnemyHealth>().takeDamage(damage);
                ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
                Destroy(gameObject);
            }
        }
    }
}
