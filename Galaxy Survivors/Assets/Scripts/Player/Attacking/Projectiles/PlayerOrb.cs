using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerOrb : MonoBehaviour
{
    public float damage;

    public GameObject player;
    public W_Orbs spawner;
    public int ID;

    public int particleID;

    [Header("Rotation")]
    public GameObject rotator;

    // Called before the first update frame
    public void Start()
    {
        // create the rotator that will manage the rotation of the orb
        rotator = new GameObject("OrbParent");
        transform.SetParent(rotator.transform);
    }

    // called once per frame
    public void Update()
    {
        // sets the position of the rotator to the player position, and then rotates to make the orb rotate
        rotator.transform.position = player.transform.position;
        transform.RotateAround(rotator.transform.position, rotator.transform.forward, 100 * Time.deltaTime);
    }

    // when a collision is entered
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if colliding with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // damage the enemy and then tell the orb spawner that this orb is dead
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            spawner.checkForOrbs(ID);
            // spawn death particle
            ParticlePooler.instance.spawnParticle(particleID, transform.position, Color.blue);
            // destroy the rotator and the orb
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
