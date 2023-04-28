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
    public int bullet;
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

    [Header("Drone Looks")]
    public Sprite[] droneSprites;

    [Header("Level")]
    public WeaponLevels level;

    [HideInInspector] public float shootRatePowerup = 1f;

    // Start is called before the first frame update
    public override void startFrame()
    {
        // spawn the 3 possible drones, and set there fire points
        _drones[0] = Instantiate(droneObject, transform.position + droneSpawnOffsets[0], Quaternion.identity);
        firePoints[0] = _drones[0].transform.GetChild(0).gameObject;
        _drones[1] = Instantiate(droneObject, transform.position + droneSpawnOffsets[1], Quaternion.identity);
        firePoints[1] = _drones[1].transform.GetChild(0).gameObject;
        _drones[2] = Instantiate(droneObject, transform.position + droneSpawnOffsets[2], Quaternion.identity);
        firePoints[2] = _drones[2].transform.GetChild(0).gameObject;

        // update the weapon level
        updateWeaponLevel();

        // based on the weapon level show the correct number of drones
        if (base.getWeaponLevel() == 0)
        {
            _drones[1].SetActive(false);
            _drones[2].SetActive(false);
        }
        else if (base.getWeaponLevel() == 1)
        {
            _drones[2].SetActive(false);
        }

        // initialize the damage & shoot rate
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        shootRate = shootRate * (perks.fireRate == 0 ? 1 : perks.fireRateLevels[perks.fireRate - 1]);
        base.initiate(_damage * damageIncrease, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        // update the positions of the drones
        _drones[0].transform.position = Vector3.Lerp(_drones[0].transform.position, transform.position + droneSpawnOffsets[0], lerpTime);
        _drones[1].transform.position = Vector3.Lerp(_drones[1].transform.position, transform.position + droneSpawnOffsets[1], lerpTime);
        _drones[2].transform.position = Vector3.Lerp(_drones[2].transform.position, transform.position + droneSpawnOffsets[2], lerpTime);

        // if enough time has passed to shoot
        if (Time.time > shootRate / playerStats.attackSpeed * shootRatePowerup + _timeOfLastShot)
        {
            // shoot based on the weapon level
            switch (base.getWeaponLevel())
            {
                // Level 1 (1 bullet)
                case 0:
                    base.fire(bullet, firePoints[0], _drones[0].transform.position, maxDistance);
                    break;
                // Level 2 (2 bullets)
                case 1:
                    base.fire(bullet, firePoints[0], _drones[0].transform.position, maxDistance);
                    base.fire(bullet, firePoints[1], _drones[1].transform.position, maxDistance);
                    break;
                // Level 3 (3 bullets)
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

    /*
    *   update the weapon level (this will require changeing the ammount
    *   of drones that are spawned)
    */
    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.droneLevel);

        // turn on all of the drones
        _drones[0].SetActive(true);
        _drones[1].SetActive(true);
        _drones[2].SetActive(true);

        // set the drones to be the level 3 
        _drones[0].GetComponent<SpriteRenderer>().sprite = droneSprites[2];
        _drones[1].GetComponent<SpriteRenderer>().sprite = droneSprites[2];
        _drones[2].GetComponent<SpriteRenderer>().sprite = droneSprites[2];

        // if level 1 then set to level 1 
        if (base.getWeaponLevel() == 0)
        {
            _drones[0].GetComponent<SpriteRenderer>().sprite = droneSprites[0];
            _drones[1].SetActive(false);
            _drones[2].SetActive(false);
        }
        // if level 1 then set to level 2
        else if (base.getWeaponLevel() == 1)
        {
            _drones[0].GetComponent<SpriteRenderer>().sprite = droneSprites[1];
            _drones[1].GetComponent<SpriteRenderer>().sprite = droneSprites[1];
            _drones[2].SetActive(false);
        }
    }
}
