using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SearchService;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

public class PlayerRocket : MonoBehaviour
{
    [Header("Following")]
    public GameObject toFollow;
    public float turnSpeed;
    public float movingSpeed;
    private Rigidbody2D _rb;

    public int particleID;

    [Header("Rocket")]
    public float damage;
    public float explosionRadius;

    [Header("Destroying")]
    public float destroyTime;
    private float _spawnTime;

    [Header("Trails")]
    public TrailRenderer trail1;
    public TrailRenderer trail2;
    public float trailRendererTime;

    // called before the first update frame
    public void Start()
    {
        // set up all components of the player
        toFollow = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _spawnTime = Time.time;
    }
    // turn on the trail of the object
    public void OnEnable()
    {
        _spawnTime = Time.time;
        turnOnTrails();
    }

    // turn off the trail on the object
    public void OnDisable()
    {
        turnOffTrails();
    }

    // this will disable the trails
    public void turnOffTrails()
    {
        trail1.time = 0;
        trail2.time = 0;
    }

    // turn on the trails 
    public void turnOnTrails()
    {
        trail1.time = trailRendererTime;
        trail2.time = trailRendererTime;
        trail1.Clear();
        trail2.Clear();
    }

    // spawn a destruction particle and then set this projectile free
    public void customDestroy()
    {
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
        GetComponent<Proj>().setFree();
    }

    // Update is called once per frame
    void Update()
    {
        // try and follow the player but if cant then destroy as there is no longer a player
        try
        {
            Vector2 direction = toFollow.transform.position - transform.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -turnSpeed * rotateAmount;
            _rb.velocity = transform.up * movingSpeed;
        }
        catch (Exception e)
        {
            print(e);
            customDestroy();
            return;
        }

        // if enough time has passed then kill the rocket
        if (Time.time > _spawnTime + destroyTime)
            customDestroy();
    }

    // if collides with a enemy then damage the enemy, and destroy the rocket
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if enemy
        if (collision.CompareTag("Enemy"))
        {
            // kill & suicide
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            customDestroy();
        }
    }
}
