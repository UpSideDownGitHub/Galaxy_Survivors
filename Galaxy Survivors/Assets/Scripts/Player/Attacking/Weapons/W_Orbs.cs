using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class W_Orbs : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;
    public PlayerPerks perks;

    [Header("Objects")]
    public Sprite[] orbSprites;
    public GameObject Orb;
    public Vector3 spawnPoint = new Vector3(0, 3, 0);

    [Header("Values")]
    [SerializeField] private float _damage;
    
    // max orbs & spawn time
    public int[] maxOrbCount;
    public float[] orbSpawnWaitTime;

    // list of the orbs
    private GameObject[] orbs;

    // for checking order
    private bool allOrbsDestoyed;
    private bool checkingTime;

    // orbs cool down
    public float orbSpawnTime;
    private float orbDestroyedAt;

    [Header("Level")]
    public WeaponLevels level;

    [HideInInspector] public float shootRatePowerup = 1f;


    // Start is called before the first frame update
    public override void startFrame()
    {
        updateWeaponLevel();
        // initialize the damage & shoot rate
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        base.initiate(_damage * damageIncrease, playerStats);
        // spawn all of the orbs
        StartCoroutine(spawnOrbs());
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        // if all of the orbs are destroyed
        if (allOrbsDestoyed)
        {
            // set time for the orb cool down
            orbDestroyedAt = Time.time;
            checkingTime = true;
            allOrbsDestoyed = false;
        }
        // if not checking the time currently then stop
        if (!checkingTime)
            return;
        // if enough time has passed to start spawning orbs again
        if (Time.time > orbDestroyedAt + orbSpawnTime / playerStats.attackSpeed * shootRatePowerup)
        {
            checkingTime = false;
            // spawn the orbs
            StartCoroutine(spawnOrbs());
        }
    }

    /*
    *   used to spawn the orbs around the player, with the correct time 
    *   difference to make them look nice
    */
    public IEnumerator spawnOrbs()
    {
        // change the amount spawned based on the weapon level
        switch (base.getWeaponLevel())
        {
            // level 1
            case 0:
                // spawn 4 orbs around the player
                orbs = new GameObject[maxOrbCount[0]];
                for (int i = 0; i < maxOrbCount[0]; i++)
                {
                    // spawn orb
                    var tempOrb = Instantiate(Orb, spawnPoint, Quaternion.identity);
                    orbs[i] = tempOrb;
                    tempOrb.GetComponent<PlayerOrb>().ID = i;
                    tempOrb.GetComponent<PlayerOrb>().damage = _damage;
                    tempOrb.GetComponent<PlayerOrb>().spawner = this;
                    tempOrb.GetComponent<PlayerOrb>().player = transform.parent.gameObject;
                    tempOrb.GetComponent<SpriteRenderer>().sprite = orbSprites[0];
                    yield return new WaitForSeconds(orbSpawnWaitTime[0]);
                }
                break;
            // level 2
            case 1:
                orbs = new GameObject[maxOrbCount[1]];
                for (int i = 0; i < maxOrbCount[1]; i++)
                {
                    var tempOrb = Instantiate(Orb, spawnPoint, Quaternion.identity);
                    orbs[i] = tempOrb;
                    tempOrb.GetComponent<PlayerOrb>().ID = i;
                    tempOrb.GetComponent<PlayerOrb>().damage = _damage;
                    tempOrb.GetComponent<PlayerOrb>().spawner = this;
                    tempOrb.GetComponent<PlayerOrb>().player = transform.parent.gameObject;
                    tempOrb.GetComponent<SpriteRenderer>().sprite = orbSprites[1];
                    yield return new WaitForSeconds(orbSpawnWaitTime[1]);
                }
                break;
            // level 3
            case 2:
                orbs = new GameObject[maxOrbCount[2]];
                for (int i = 0; i < maxOrbCount[2]; i++)
                {
                    var tempOrb = Instantiate(Orb, spawnPoint, Quaternion.identity);
                    orbs[i] = tempOrb;
                    tempOrb.GetComponent<PlayerOrb>().ID = i;
                    tempOrb.GetComponent<PlayerOrb>().damage = _damage;
                    tempOrb.GetComponent<PlayerOrb>().spawner = this;
                    tempOrb.GetComponent<PlayerOrb>().player = transform.parent.gameObject;
                    tempOrb.GetComponent<SpriteRenderer>().sprite = orbSprites[2];
                    yield return new WaitForSeconds(orbSpawnWaitTime[2]);
                }
                break;
            default:
                break;
        }
    }

    /*
    *   this will check if there are any orbs left
    */
    public void checkForOrbs(int ID)
    {
        // for the amount of orbs that have been spawned
        orbs[ID] = null;
        for (int i = 0; i < orbs.Length; i++)
        {
            // if there is a orb then there are some left so stop
            if (orbs[i] != null)
                return;
        }
        allOrbsDestoyed = true;
    }

    // used to set the weapon level to the current level of the shotgun
    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.orbsLevel);
    }
}
