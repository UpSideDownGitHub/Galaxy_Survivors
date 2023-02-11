using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // NEED TO DEAL DAMAGE TO THE ENEMY HERE
            Destroy(gameObject);
        }
    }
}
