using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Shotgun : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Objects")]
    public GameObject bullet;
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
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        shootRate = shootRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage * damageIncrease, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > shootRate / playerStats.attackSpeed * shootRatePowerup + _timeOfLastShot)
        {
            switch (base.getWeaponLevel())
            {
                case 0:
                    for (int i = 0; i < bulletCount; i++)
                    {
                        base.fire(bullet, firePoint, spread);
                    }
                    break;
                case 1:
                    for (int i = 0; i < bulletCount * 1.5; i++)
                    {
                        base.fire(bullet, firePoint, spread);
                    }
                    break;
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

    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.shotGunLevel);
    }
}