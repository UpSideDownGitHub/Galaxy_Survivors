using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    private GameObject _player;
    private Pickup _pickup;

    public float lerpTime = 0.01f;
    public float minDistance;
    public float maxDistance;
    public bool attracted = false;
    public int ID;

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
                    StatsManager.instance.setXP(1, true);
                    break;
                case 1: // Coin
                    StatsManager.instance.setCoins(1, true);
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
            Destroy(gameObject);
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
}
