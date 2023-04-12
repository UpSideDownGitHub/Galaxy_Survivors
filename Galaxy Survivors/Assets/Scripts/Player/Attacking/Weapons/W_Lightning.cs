using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Lightning : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;

    [Header("Spawning")]
    public float spawnRate;
    private float _lastSpawnTime;

    [Header("Attacking")]
    public int _lightningCount;
    public float _radius;

    [Header("Values")]
    [SerializeField] private float _damage;




    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > spawnRate / playerStats.attackSpeed + _lastSpawnTime)
        {
            _lastSpawnTime = Time.time;
            base.placeLightning(transform.position, _radius, _lightningCount);
        }
    }
}
