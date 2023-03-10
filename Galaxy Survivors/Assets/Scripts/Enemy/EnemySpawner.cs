using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float range, xStretch, yStretch;
    public GameObject test_obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
            spawnEnemy();
    }

    public void spawnEnemy()
    {
        Debug.Log("Spawning Item");
        // find a point around the player in (in range)
        Vector2 point = randomCircle(Vector2.zero, range);
        Debug.Log(point);
        Instantiate(test_obj, point, Quaternion.identity);
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
