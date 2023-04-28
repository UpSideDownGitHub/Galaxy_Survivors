using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class W_Shotgun : Weapon
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
    public float spread;
    public int bulletCount;

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
            // based on the weapon level shoot a different ammount of bullets
            switch (base.getWeaponLevel())
            {
                // level 1
                case 0:
                    for (int i = 0; i < bulletCount; i++)
                    {
                        base.fire(bullet, firePoint, spread);
                    }
                    break;
                // level 2
                case 1:
                    for (int i = 0; i < bulletCount * 1.5; i++)
                    {
                        base.fire(bullet, firePoint, spread);
                    }
                    break;
                // level 3
                case 2:
                    for (int i = 0; i < bulletCount * 2; i++)
                    {
                        base.fire(bullet, firePoint, spread);
                    }
                    break;
                default:
                    break;
            }
            _timeOfLastShot = Time.time;
        }
    }

    // used to set the weapon level to the current level of the shotgun
    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.shotGunLevel);
    }
}