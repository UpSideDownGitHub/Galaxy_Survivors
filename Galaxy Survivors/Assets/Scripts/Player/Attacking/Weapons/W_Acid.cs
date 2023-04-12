using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Acid : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;

    [Header("Objects")]
    public GameObject acid;

    [Header("Acid Spawning")]
    public float spawnRate;
    private float _lastSpawnTime;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _acidDuration;
    [SerializeField] private float _acidAttackTime;
    [SerializeField] private float _acidAttackTimeModifyer;

    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, _acidDuration, _acidAttackTime, _acidAttackTimeModifyer, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if(Time.time > spawnRate / playerStats.attackSpeed + _lastSpawnTime)
        { 
            _lastSpawnTime = Time.time;
            base.placeAcid(acid);
            
        }
    }
}
