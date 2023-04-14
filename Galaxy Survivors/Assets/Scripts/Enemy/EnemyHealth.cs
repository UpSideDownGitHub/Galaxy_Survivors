using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private Enemy _enemy;
    private SpriteRenderer _renderer;

    public GameStats stats;

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
        // update the stats
        stats.kills++;
        StatManager.statsChanged = true;

        PickupsPool.instance.spawnPickup(0, transform.position);
        ParticlePooler.instance.spawnParticle(1, transform.position, _renderer.color);
        _enemy.setFree();
    }
}
