using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Rockets : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Shooting")]
    public GameObject bullet;
    public GameObject[] firePoints;
    public float maxDistance;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;

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
        base.initiate(_damage * damageIncrease, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > shootRate / playerStats.attackSpeed + _timeOfLastShot)
        {
            switch (base.getWeaponLevel())
            {
                case 0:
                    base.fire(bullet, firePoints[0], transform.position, maxDistance);
                    break;
                case 1:
                    base.fire(bullet, firePoints[0], transform.position, maxDistance);
                    base.fire(bullet, firePoints[1], transform.position, maxDistance);
                    break;
                case 2:
                    base.fire(bullet, firePoints[0], transform.position, maxDistance);
                    base.fire(bullet, firePoints[1], transform.position, maxDistance);
                    base.fire(bullet, firePoints[2], transform.position, maxDistance);
                    break;
                default:
                    break;
            }
            _timeOfLastShot = Time.time;
        }
    }

    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.rocketsLevel);
    }
}
