
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float range, xStretch, yStretch;
    public GameObject player;

    public EnemyPool pool;

    [Header("Advanced Spawning")]
    public Timer timer;

    [Header("End The Game")]
    public GameObject endScreen;

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
        Vector2 point;
        int ID;
        switch (timer.min)
        {
            case 0:
                point = randomCircle(player.transform.position, range);
                pool.spawnEnemy(0, point);
                break;
            case 1:
            case 2:
                point = randomCircle(player.transform.position, range);
                ID = Random.Range(0, 2);
                pool.spawnEnemy(ID, point);
                break;
            case 3:
                point = randomCircle(player.transform.position, range);
                ID = Random.Range(1, 4);
                pool.spawnEnemy(ID, point);
                break;
            case 4:
            case 5:
                point = randomCircle(player.transform.position, range);
                ID = Random.Range(0, 3);
                pool.spawnEnemy(ID, point);
                break;

            case 6:
                point = randomCircle(player.transform.position, range);
                ID = Random.Range(2, 5);
                pool.spawnEnemy(ID, point);
                break;
            case 7:
            case 8:
                point = randomCircle(player.transform.position, range);
                ID = Random.Range(1, 4);
                pool.spawnEnemy(ID, point);
                break;
            case 9:
                point = randomCircle(player.transform.position, range);
                ID = Random.Range(0, 5);
                pool.spawnEnemy(ID, point);
                break;
            case 10:
                // END THE GAME
                endScreen.SetActive(true);
                endScreen.GetComponent<EndScreen>().notDied = true;
                break;
            default:
                print("NO SPAWN SELECTED");
                break;
        }
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
