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

    public void Start()
    {
        _spawnTime = Time.time;
    }
    public void OnEnable()
    {
        _spawnTime = Time.time;
    }

    public void Update()
    {
        if (Time.time > _spawnTime + destroyTime)
            customDestroy();
    }

    public void customDestroy()
    {
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
        GetComponent<Proj>().setFree();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            customDestroy();
        }
    }
}
