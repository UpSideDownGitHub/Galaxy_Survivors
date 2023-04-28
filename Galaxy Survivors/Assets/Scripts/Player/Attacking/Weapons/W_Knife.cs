using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Knife : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Objects")]
    public int bullet;
    public GameObject firePoint;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;
    public float[] peirceCount;

    [Header("Shooting")]
    public float shootRate;
    private float _timeOfLastShot;

    [Header("Level")]
    public WeaponLevels level;

    [HideInInspector] public float shootRatePowerup = 1f;

    // Start is called before the first frame update
    public override void startFrame()
    {
        updateWeaponLevel();
        // initialize the damage & shoot rate
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        shootRate = shootRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage * damageIncrease, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        // if enough time has passed to shoot again
        if (Time.time > shootRate / playerStats.attackSpeed * shootRatePowerup + _timeOfLastShot)
        {
            //shoot
            base.fire(bullet, firePoint, (int)peirceCount[base.getWeaponLevel()]);
            _timeOfLastShot = Time.time;
        }
    }

    // used to set the weapon level to the current level of the knife
    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.knifeLevel);
    }
}
