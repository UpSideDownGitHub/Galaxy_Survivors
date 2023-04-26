using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class W_Acid : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Objects")]
    public int acid;

    [Header("Acid Spawning")]
    public float spawnRate;
    private float _lastSpawnTime;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _acidDuration;
    [SerializeField] private float _acidAttackTime;
    [SerializeField] private float _acidAttackTimeModifyer;
    public float[] sizes;

    [Header("Level")]
    public WeaponLevels level;

    [HideInInspector] public float shootRatePowerup = 1f;

    // Start is called before the first frame update
    public override void startFrame()
    {
        updateWeaponLevel();
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        spawnRate = spawnRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage * damageIncrease, _acidDuration, _acidAttackTime, _acidAttackTimeModifyer, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if(Time.time > spawnRate / playerStats.attackSpeed * shootRatePowerup + _lastSpawnTime)
        { 
            _lastSpawnTime = Time.time;
            base.placeAcid(acid, sizes[base.getWeaponLevel()]);
        }
    }

    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.acidLevel);
    }
}
