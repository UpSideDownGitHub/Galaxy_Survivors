using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerAcid : MonoBehaviour
{
    public float damage;
    public float attackTime;
    public float deathTime;
    private float _spawnTime;
    private float _timeSinceLastAttack;

    public GameObject particlesReference;

    // called before first update frame
    public void Start()
    {
        _spawnTime = Time.time;
    }

    // called when the object is enabled
    public void OnEnable()
    {
        _spawnTime = Time.time;
    }

    // to destroy the object
    public void customDestroy()
    {
        // set the object free
        GetComponent<Proj>().setFree();
    }

    // called once per frame
    public void Update()
    {
        // if enough time has passed to destroy the object
        if (Time.time > deathTime + _spawnTime)
            customDestroy();
    }

    // when entering a collision with a trigger
    public void OnTriggerStay2D(Collider2D collision)
    {
        // if not colliding with and enemy then return
        if (!collision.CompareTag("Enemy"))
            return;
        // if enough time has passed then attack that enemy
        if (Time.time > attackTime + _timeSinceLastAttack)
        {
            // increase the time and attack the enemy
            _timeSinceLastAttack = Time.time;
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
        }
    }
}
