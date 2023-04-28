using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerBullet : MonoBehaviour
{
    public float damage;
    public float destroyTime;
    private float _spawnTime;
    public int particleID;

    // called before the first update frame
    public void Start()
    {
        _spawnTime = Time.time;
    }

    // called when the object is enabled
    public void OnEnable()
    {
        _spawnTime = Time.time;
    }

    // called once per frame
    public void Update()
    {
        // if enough time has passed to destroy them then destroy
        if (Time.time > _spawnTime + destroyTime)
            customDestroy();
    }

    // custom destroy for the object
    public void customDestroy()
    {
        // spawn an instance of the death particle, then set it free to be used with pooler
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
        GetComponent<Proj>().setFree();
    }

    // when entering a collision with a trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if collides with enemy
        if (collision.CompareTag("Enemy"))
        {
            // damage the enemy and then destroy
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            customDestroy();
        }
    }
}
