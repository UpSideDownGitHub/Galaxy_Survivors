using System.Collections;
using System.Collections.Generic;
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
    public GameObject trail1;
    public GameObject trail2;

    public void Start()
    {
        _spawnTime = Time.time;
    }
    public void OnEnable()
    {
        _spawnTime = Time.time;
        turnOnTrails();
    }

    public void OnDisable()
    {
        turnOffTrails();
    }

    public void turnOffTrails()
    {
        trail1.SetActive(false);
        trail2.SetActive(false);
    }

    public void turnOnTrails()
    {
        trail1.SetActive(true);
        trail2.SetActive(true);
    }

    public void customDestroy()
    {
        ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
        GetComponent<Proj>().setFree();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Vector2 direction = toFollow.transform.position - transform.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -turnSpeed * rotateAmount;
            _rb.velocity = transform.up * movingSpeed;
        }
        catch 
        {
            customDestroy();
            return;
        }

        if (Time.time > _spawnTime + destroyTime)
            customDestroy();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            CancelInvoke();
            customDestroy();
        }
    }
}
