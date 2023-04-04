using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private Enemy _enemy;

    public void Start()
    {
        currentHealth = maxHealth;
        _enemy = GetComponent<Enemy>();
    }
    
    public void takeDamage(float damage)
    {
        if (currentHealth - damage < 0)
        {
            _enemy.setFree();
            return;
        }
        currentHealth -= damage;
    }
}
