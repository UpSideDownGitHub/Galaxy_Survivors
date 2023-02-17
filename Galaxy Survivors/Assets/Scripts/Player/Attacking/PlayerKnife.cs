using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerKnife : MonoBehaviour
{
    public float damage;

    public int maxPeirce;
    public int pierceCount;

    public void Start()
    {
        pierceCount = maxPeirce;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (pierceCount - 1 >= 0)
            {
                pierceCount--;
                // DEAL DAMAGE TO THE ENEMY
            }
            else
            {
                // NEED TO DEAL DAMAGE TO THE ENEMY HERE
                Destroy(gameObject);
            }
        }
    }
}
