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


    // Start is called before the first frame update
    public override void startFrame()
    {
        updateWeaponLevel();
        var damageIncrease = perks.damageIncrease == 0 ? 1 : perks.damageIncreaseLevels[perks.damageIncrease - 1];
        base.initiate(_damage * damageIncrease, playerStats);
        // spawn all of the orbs
        StartCoroutine(spawnOrbs());
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (allOrbsDestoyed)
        {
            orbDestroyedAt = Time.time;
            checkingTime = true;
            allOrbsDestoyed = false;
        }
        if (!checkingTime)
            return;
        if (Time.time > orbDestroyedAt + orbSpawnTime / playerStats.attackSpeed)
        {
            checkingTime = false;
            // spawn the orbs
            StartCoroutine(spawnOrbs());
        }
    }

    public IEnumerator spawnOrbs()
    {
        switch (base.getWeaponLevel())
        {
            case 0:
                orbs = new GameObject[maxOrbCount[0]];
                for (int i = 0; i < maxOrbCount[0]; i++)
                {
                    var tempOrb = Instantiate(Orb, spawnPoint, Quaternion.identity);
                    orbs[i] = tempOrb;
                    tempOrb.GetComponent<PlayerOrb>().ID = i;
                    tempOrb.GetComponent<PlayerOrb>().damage = _damage;
                    tempOrb.GetComponent<PlayerOrb>().spawner = this;
                    tempOrb.GetComponent<PlayerOrb>().player = transform.parent.gameObject;
                    yield return new WaitForSeconds(orbSpawnWaitTime[0]);
                }
                break;
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
                    yield return new WaitForSeconds(orbSpawnWaitTime[1]);
                }
                break;
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
                    yield return new WaitForSeconds(orbSpawnWaitTime[2]);
                }
                break;
            default:
                break;
        }
    }

    public void checkForOrbs(int ID)
    {
        orbs[ID] = null;
        for (int i = 0; i < orbs.Length; i++)
        {
            if (orbs[i] != null)
                return;
        }
        allOrbsDestoyed = true;
    }

    public override void updateWeaponLevel()
    {
        base.setWeaponLevel(level.orbsLevel);
    }
}
