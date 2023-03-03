using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class W_Orbs : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;

    [Header("Objects")]
    public GameObject Orb;
    public Vector3 spawnPoint = new Vector3(0, 3, 0);

    [Header("Values")]
    [SerializeField] private float _damage;
    
    // max orbs & spawn time
    public int maxOrbCount;
    public float orbSpawnWaitTime;

    // list of the orbs
    private GameObject[] orbs;

    // for checking order
    private bool allOrbsDestoyed;
    private bool checkingTime;

    // orbs cool down
    public float orbSpawnTime;
    private float orbDestroyedAt;


    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, playerStats);
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
        if (Time.time > orbDestroyedAt + orbSpawnTime)
        {
            checkingTime = false;
            // spawn the orbs
            StartCoroutine(spawnOrbs());
        }
    }

    public IEnumerator spawnOrbs()
    {
        orbs = new GameObject[maxOrbCount];
        for (int i = 0; i < maxOrbCount; i++)
        {
            var tempOrb = Instantiate(Orb, spawnPoint, Quaternion.identity);
            orbs[i] = tempOrb;
            tempOrb.GetComponent<PlayerOrb>().ID = i;
            tempOrb.GetComponent<PlayerOrb>().spawner = this;
            tempOrb.GetComponent<PlayerOrb>().player = transform.parent.gameObject;
            yield return new WaitForSeconds(orbSpawnWaitTime);
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
}
