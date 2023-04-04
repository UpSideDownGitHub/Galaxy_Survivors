using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float range, xStretch, yStretch;
    public GameObject test_obj;
    public GameObject player;

    public EnemyPool pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = EnemyPool.instance;
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy();
    }

    public void spawnEnemy()
    {
        Debug.Log("Spawning Item");
        // find a point around the player in (in range)
        Vector2 point = randomCircle(player.transform.position, range);

        // if there is an enemy avaialbe then spawn the enemy
        pool.spawnEnemy(0, point);
        //Instantiate(test_obj, point, Quaternion.identity);
    }

    Vector2 randomCircle(Vector2 center, float radius)
    {
        float ang = Random.value * 360;
        Vector2 pos;
        pos.x = center.x + (radius * xStretch) * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + (radius * yStretch) * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
