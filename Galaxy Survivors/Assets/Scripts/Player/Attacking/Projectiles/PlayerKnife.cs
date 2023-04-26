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
    public GameObject trail1;

    public void Start()
    {
        _spawnTime = Time.time;
    }
    public void OnEnable()
    {
        _spawnTime = Time.time;
        turnOnTrails();
    }

    public void Update()
    {
        if (Time.time > destroyTime + _spawnTime)
            customDestroy();
    }

    public void OnDisable()
    {
        turnOffTrails();
    }

    public void turnOffTrails()
    {
        trail1.SetActive(false);
    }

    public void turnOnTrails()
    {
        trail1.SetActive(true);
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
            if (pierceCount - 1 >= 0)
            {
                pierceCount--;
                collision.GetComponent<EnemyHealth>().takeDamage(damage);
            }
            else
            {
                collision.GetComponent<EnemyHealth>().takeDamage(damage);
                customDestroy();
            }
        }
    }
}
