using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerPerks perks;
    public GameStats gameStats;

    private GameObject _player;
    private Pickup _pickup;

    public float lerpTime = 0.01f;
    public float minDistance;
    public float maxDistance;

    private float _orignalMaxDistance;

    public bool attracted = false;
    public int ID;
    public float _maxDistanceX;
    public float _maxDistanceY;

    public void OnDisable()
    {
        attracted = false;
    }
    public void OnEnable()
    {
        maxDistance = _orignalMaxDistance * stats.pickupModifyer;
    }
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
    }

    // Update is called once per frame
    void Update()
    {
        float distX = Mathf.Abs(transform.position.x - _player.transform.position.x);
        float distY = Mathf.Abs(transform.position.y - _player.transform.position.y);
        if (distX > _maxDistanceX || distY > _maxDistanceY)
        {
            // Despawn the Pickup
            _pickup.setFree();
        }

        if (!attracted)
        {
            if (Vector2.Distance(transform.position, _player.transform.position) < maxDistance)
            {
                attracted = true;
            }
            return;
        }

        transform.position = Vector2.Lerp(transform.position, _player.transform.position, lerpTime);

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
            _pickup.setFree();
        }
    }

    public void killAllEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().killEnemy();
        }
    }

    public void suckAllPickups()
    {
        var pickups = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (var pickup in pickups)
        {
            pickup.GetComponent<PickupMovement>().attracted = true;
        }
    }

    public void increasePlayerHealth()
    {
        _player.GetComponent<PlayerHealth>().increaseHealth(30);
    }

    public void increasePickupRange(float val)
    {
        maxDistance = maxDistance * val;
    }
}
