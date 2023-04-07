using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private Enemy _enemy;
    private SpriteRenderer _renderer;

    public void Start()
    {
        currentHealth = maxHealth;
        _enemy = GetComponent<Enemy>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public void takeDamage(float damage)
    {
        if (currentHealth - damage < 0)
        {
            killEnemy();
            return;
        }
        currentHealth -= damage;
    }

    public void killEnemy()
    {
        ParticlePooler.instance.spawnParticle(1, transform.position, _renderer.color);
        _enemy.setFree();
    }
}
