using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Lightning : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Spawning")]
    public float spawnRate;
    private float _lastSpawnTime;

    [Header("Attacking")]
    public int[] lightningCounts;
    public float radius;

    [Header("Values")]
    [SerializeField] private float _damage;

    [Header("Level")]
    public WeaponLevels level;

    [HideInInspector] public float shootRatePowerup = 1f;

    // Start is called before the first frame update
    public override void startFrame()
    {
        updateWeaponLevel();
        // initialize the damage & shoot rate
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        spawnRate = spawnRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage * damageIncrease, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        // if enough time has passed to spawn a lighting obejct
        if (Time.time > spawnRate / playerStats.attackSpeed * shootRatePowerup + _lastSpawnTime)
        {
            // spawn lightning
            _lastSpawnTime = Time.time;
            base.placeLightning(transform.position, radius, lightningCounts[base.getWeaponLevel()]);
        }
    }

    // used to set the weapon level to the current level of the lightning
    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.lightningLevel);
    }
}
