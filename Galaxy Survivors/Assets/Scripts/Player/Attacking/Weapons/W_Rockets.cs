using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Rockets : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Shooting")]
    public int bullet;
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
        // if enough time has passed to shoot
        if (Time.time > shootRate / playerStats.attackSpeed * shootRatePowerup + _timeOfLastShot)
        {
            // change how many bullets to shoot based on the weapon level
            switch (base.getWeaponLevel())
            {
                // level 1
                case 0:
                    base.fire(bullet, firePoints[0], transform.position, maxDistance);
                    break;
                // level 2
                case 1:
                    base.fire(bullet, firePoints[0], transform.position, maxDistance);
                    base.fire(bullet, firePoints[1], transform.position, maxDistance);
                    break;
                // level 3
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

    // used to set the weapon level to the current level of the shotgun
    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.rocketsLevel);
    }
}
