using JetBrains.Annotations;
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

    [Header("Custom Destroy")]
    public float destroyTime;
    private float _spawnTime;

    [Header("Trails")]
    public TrailRenderer trail1;
    public float trailRendererTime;

    // called before the first update frame
    public void Start()
    {
        _spawnTime = Time.time;
    }

    // when the object is turned on
    public void OnEnable()
    {
        // enable the trails
        _spawnTime = Time.time;
        turnOnTrails();
    }

    // called once per frame
    public void Update()
    {
        // if enough time has passed then destroy the object
        if (Time.time > destroyTime + _spawnTime)
            customDestroy();
    }

    // when the object is turned off
    public void OnDisable()
    {
        turnOffTrails();
    }

    // turn off the trails
    public void turnOffTrails()
    {
        trail1.time = 0;
    }

    // turns on the trials of the knife
    public void turnOnTrails()
    {
        trail1.time = trailRendererTime;
        trail1.Clear();
    }

    // spawns the death particle and then sets free the object to be spawned by the pooler again
    public void customDestroy()
    {
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
        GetComponent<Proj>().setFree();
    }

    // when entering a collision with a trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // if there is still a peirce left
            if (pierceCount - 1 >= 0)
            {
                // minus a peirce and then damage the enemy
                pierceCount--;
                collision.GetComponent<EnemyHealth>().takeDamage(damage);
            }
            else
            {
                // as no peirce left then damage then enemy and destroy the knife
                collision.GetComponent<EnemyHealth>().takeDamage(damage);
                customDestroy();
            }
        }
    }
}
