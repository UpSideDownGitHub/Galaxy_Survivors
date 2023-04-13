using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Drone : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    public GameObject droneObject;
    public GameObject[] _drones;
    public GameObject[] firePoints;
    public GameObject bullet;
    public Vector3[] droneSpawnOffsets;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;

    public float maxDistance;

    // shooting
    public float shootRate;
    private float _timeOfLastShot;

    [Header("Drone Movement")]
    public float lerpTime;

    [Header("Level")]
    public WeaponLevels level;

    // Start is called before the first frame update
    public override void startFrame()
    {
        _drones[0] = Instantiate(droneObject, transform.position + droneSpawnOffsets[0], Quaternion.identity);
        firePoints[0] = _drones[0].transform.GetChild(0).gameObject;
        _drones[1] = Instantiate(droneObject, transform.position + droneSpawnOffsets[1], Quaternion.identity);
        firePoints[1] = _drones[1].transform.GetChild(0).gameObject;
        _drones[2] = Instantiate(droneObject, transform.position + droneSpawnOffsets[2], Quaternion.identity);
        firePoints[2] = _drones[2].transform.GetChild(0).gameObject;

        updateWeaponLevel();

        if (base.getWeaponLevel() == 0)
        {
            _drones[1].SetActive(false);
            _drones[2].SetActive(false);
        }
        else if (base.getWeaponLevel() == 1)
        {
            _drones[2].SetActive(false);
        }

        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        shootRate = shootRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage * damageIncrease, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        _drones[0].transform.position = Vector3.Lerp(_drones[0].transform.position, transform.position + droneSpawnOffsets[0], lerpTime);
        _drones[1].transform.position = Vector3.Lerp(_drones[1].transform.position, transform.position + droneSpawnOffsets[1], lerpTime);
        _drones[2].transform.position = Vector3.Lerp(_drones[2].transform.position, transform.position + droneSpawnOffsets[2], lerpTime);

        if (Time.time > shootRate / playerStats.attackSpeed + _timeOfLastShot)
        {
            switch (base.getWeaponLevel())
            {
                case 0:
                    base.fire(bullet, firePoints[0], _drones[0].transform.position, maxDistance);
                    break;
                case 1:
                    base.fire(bullet, firePoints[0], _drones[0].transform.position, maxDistance);
                    base.fire(bullet, firePoints[1], _drones[1].transform.position, maxDistance);
                    break;
                case 2:
                    base.fire(bullet, firePoints[0], _drones[0].transform.position, maxDistance);
                    base.fire(bullet, firePoints[1], _drones[1].transform.position, maxDistance);
                    base.fire(bullet, firePoints[2], _drones[2].transform.position, maxDistance);
                    break;
                default:
                    break;
            }
            _timeOfLastShot = Time.time;
        }
    }

    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.droneLevel);

        _drones[0].SetActive(true);
        _drones[1].SetActive(true);
        _drones[2].SetActive(true);

        if (base.getWeaponLevel() == 0)
        {
            _drones[1].SetActive(false);
            _drones[2].SetActive(false);
        }
        else if (base.getWeaponLevel() == 1)
        {
            _drones[2].SetActive(false);
        }
    }
}
