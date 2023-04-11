
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public float range, xStretch, yStretch;
    public GameObject player;

    public PickupsPool pool;

    [Header("Spawn Timing")]
    public float spawnTime;
    private float _timeSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        pool = PickupsPool.instance;
        _timeSinceLastSpawn = -100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime + _timeSinceLastSpawn)
            spawnPickup();
    }

    public void spawnPickup()
    {
        _timeSinceLastSpawn = Time.time;

        Vector2 point;
        int ID = Random.Range(1, 5);
        point = randomCircle(player.transform.position, range);
        pool.spawnPickup(ID, point); // ID = 1-4
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
