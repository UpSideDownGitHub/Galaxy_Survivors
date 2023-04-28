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

    [Header("Difficulty Scaling")]
    private SaveManager _saveManager;
    public float mapID;

    // called before the first update frame
    public void Start()
    {
        _saveManager = SaveManager.instance;
        mapID = _saveManager.data.currentMap + 1;
        currentHealth = maxHealth * mapID;
        _enemy = GetComponent<Enemy>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // called when the object is enabled
    public void OnEnable()
    {
        currentHealth = maxHealth * mapID;
    }

    // called when the enemy needs to take damage
    public void takeDamage(float damage)
    {
        // if the damage is enough to kill the enemy
        if (currentHealth - damage < 0)
        {
            // the the enemy
            killEnemy();
            return;
        }
        currentHealth -= damage;
    }


    // kills the enemi
    public void killEnemy()
    {
        // update the stats
        stats.kills++;
        StatManager.statsChanged = true;

        // spawn particle and set the enemy free to be pooled again
        PickupsPool.instance.spawnPickup(0, transform.position);
        ParticlePooler.instance.spawnParticle(1, transform.position, _renderer.color);
        _enemy.setFree();
    }
}
