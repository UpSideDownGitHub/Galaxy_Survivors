using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Cannon : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    public GameObject[] firePoints;
    public int[] levelFirepoints;
    public GameObject bullet;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;

    // shooting
    [Header("Shooting")]
    public float shootRate;
    private float _timeOfLastShot;

    [Header("Level")]
    public WeaponLevels level;

    // Start is called before the first frame update
    public override void startFrame()
    {
        updateWeaponLevel();
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        shootRate = shootRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > shootRate / playerStats.attackSpeed + _timeOfLastShot)
        {
            for (int i = 0; i < levelFirepoints[base.getWeaponLevel()]; i++)
            {
                base.fire(bullet, firePoints[i]);
            }
            _timeOfLastShot = Time.time;
        }
    }

    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.cannonsLevel);
    }
}
