using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Lightning : Weapon
{
    [Header("Spawning")]
    public float spawnRate;
    public float spawnRateModifyer;
    private float _lastSpawnTime;

    [Header("Attacking")]
    public int _lightningCount;
    public float _radius;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _damageModifyer;




    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, _damageModifyer);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > spawnRate * spawnRateModifyer + _lastSpawnTime)
        {
            _lastSpawnTime = Time.time;
            base.placeLightning(transform.position, _radius, _lightningCount);
        }
    }
}
