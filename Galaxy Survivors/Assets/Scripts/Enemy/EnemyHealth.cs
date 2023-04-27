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

    public void Start()
    {
        _saveManager = SaveManager.instance;
        mapID = _saveManager.data.currentMap + 1;
        currentHealth = maxHealth * mapID;
        _enemy = GetComponent<Enemy>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        currentHealth = maxHealth * mapID;
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
