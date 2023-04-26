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
    public void Start()
    {
        _spawnTime = Time.time;
    }
    public void OnEnable()
    {
        _spawnTime = Time.time;
    }

    public void customDestroy()
    {
        GetComponent<Proj>().setFree();
    }

    public void Update()
    {
        if (Time.time > deathTime + _spawnTime)
            customDestroy();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        if (Time.time > attackTime + _timeSinceLastAttack)
        {
            _timeSinceLastAttack = Time.time;
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
        }
    }
}
