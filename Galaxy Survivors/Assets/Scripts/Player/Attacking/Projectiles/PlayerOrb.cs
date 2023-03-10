using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOrb : MonoBehaviour
{
    public float damage;

    public GameObject player;
    public W_Orbs spawner;
    public int ID;

    [Header("Rotation")]
    public GameObject rotator;

    public void Start()
    {
        rotator = new GameObject("OrbParent");
        transform.SetParent(rotator.transform);
    }

    public void Update()
    {
        rotator.transform.position = player.transform.position;
        transform.RotateAround(rotator.transform.position, rotator.transform.forward, 100 * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
            spawner.checkForOrbs(ID);
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
