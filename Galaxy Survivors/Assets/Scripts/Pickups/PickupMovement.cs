using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    // public variable
    public PlayerStats stats;
    public PlayerPerks perks;
    public GameStats gameStats;
    public float lerpTime = 0.01f;
    public float minDistance;
    public float maxDistance;
    public bool attracted = false;
    public int ID;
    public float _maxDistanceX;
    public float _maxDistanceY;

    // private variables 
    private GameObject _player;
    private Pickup _pickup;
    private float _orignalMaxDistance;

    [Header("Time Limit")]
    public float despawnTime;
    private float _timeSpawned;

    // when the object is disabled
    public void OnDisable()
    {
        attracted = false;
    }
    // when the object is enabled
    public void OnEnable()
    {
        // set the spawn time and the max distance that is allowed from the stats of the player
        _timeSpawned = Time.time;
        maxDistance = _orignalMaxDistance * stats.pickupModifyer;
    }

    // called when the object is being loaded
    private void Awake()
    {
        _orignalMaxDistance = maxDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pickup = GetComponent<Pickup>();
        ID = _pickup.ID;
        _timeSpawned = Time.time;
    }

    // this will make the pickup be attracted to the player
    public void freePickup()
    {
        attracted = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if the time scale is 0 then return
        if (Time.timeScale == 0)
            return;

        // if enough time has passed, then despawn the pickup
        if (Time.time > _timeSpawned + despawnTime && ID == 0)
            _pickup.setFree();

        // calculate the distance to the player
        float distX = Mathf.Abs(transform.position.x - _player.transform.position.x);
        float distY = Mathf.Abs(transform.position.y - _player.transform.position.y);
        // if the distance is less than max
        if (distX > _maxDistanceX || distY > _maxDistanceY)
        {
            // if the is an XP then go to the player
            if (ID == 0)
            { 
                attracted = true;
                return;
            }
            // otherwise set the pickup free
            _pickup.setFree();
        }

        // is not currently being attracted
        if (!attracted)
        {
            // if within range
            if (Vector2.Distance(transform.position, _player.transform.position) < maxDistance)
            {
                // go to the player
                attracted = true;
            }
            return;
        }

        // lerp the position of pickup towards the player (accounting for the distance (higher speed higher distance))
        float speedChanger = Vector2.Distance(transform.position, _player.transform.position);
        transform.position = Vector2.Lerp(transform.position, _player.transform.position, lerpTime * speedChanger);

        // if within a certain distance of the player
        if (Vector2.Distance(transform.position, _player.transform.position) < minDistance)
        {
            // remove the object and apply the effect
            switch(ID)
            {
                case 0: // XP
                    var temp = 1 * stats.xpModifyer;
                    temp *= perks.xpIncrease == 0 ? 1 : perks.xpIncreaseLevels[perks.xpIncrease - 1];
                    gameStats.XP += (int)temp;
                    StatManager.statsChanged = true;
                    break;
                case 1: // Coin
                    var temp2 = 1 * perks.coinsIncrease == 0 ? 1 : perks.coinsIncreaseLevels[perks.coinsIncrease - 1];
                    gameStats.coins += (int)temp2;
                    StatManager.statsChanged = true;
                    break;
                case 2: // Nuke
                    killAllEnemies();
                    break;
                case 3: // Vacum
                    suckAllPickups();
                    break;
                case 4: // Food
                    increasePlayerHealth();
                    break;
                default:
                    print("NO OPTION SELECTED");
                    break;
            }
            // set it free
            _pickup.setFree();
        }
    }

    /*
    *   this will handle killing all of the enemies that are currently alive
    */
    public void killAllEnemies()
    {
        // loop through all of the enemies and kill them
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().killEnemy();
        }
    }

    /*
    *   absorb all of the pickups towards the players
    */
    public void suckAllPickups()
    {
        // loop through all of the pickups and attract them
        var pickups = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (var pickup in pickups)
        {
            pickup.GetComponent<PickupMovement>().attracted = true;
        }
    }

    /*
    *   Increase the total health of the player
    */
    public void increasePlayerHealth()
    {
        _player.GetComponent<PlayerHealth>().increaseHealth(30);
    }

    /*
    *   increase the range of the pickup to that the player can pick them up from
    *   further away
    */
    public void increasePickupRange(float val)
    {
        maxDistance = maxDistance * val;
    }
}
