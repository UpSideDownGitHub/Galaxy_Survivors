using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void takeDamage(float damage)
    {
        if (currentHealth - damage < 0)
        {
            Destroy(gameObject);
            return;
        }
        currentHealth -= damage;
    }

}
