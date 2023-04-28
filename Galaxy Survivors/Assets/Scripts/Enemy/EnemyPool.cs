using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // public variables
    public int[] maxEnemies;
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject[] enemyPrefabs;

    public static EnemyPool instance;

    // called whislt the object is being loaded
    public void Awake()
    {
        instance = this;

        // this will spawn maxenemies ammount of each enemy and store them in the enemies list
        for (int j = 0; j < enemyPrefabs.Length; j++)
        {
            for (int i = 0; i < maxEnemies[j]; i++)
            {
                enemies.Add(Instantiate(enemyPrefabs[j], new Vector3(0, 0, 0), new Quaternion()).GetComponent<Enemy>());
            }
        }

        // turn off all of the enemies
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].disable();
        }
    }

    public bool spawnEnemy(int enemyID, Vector2 spawnPos)
    {
        // get the bound in the list of the current enemy type to be spawned
        int minPos = 0;
        for (int i = 0; i < enemyID; i++)
            minPos += maxEnemies[i];

        // for all of the enemies of the given type
        for (int i = minPos; i < minPos + maxEnemies[enemyID]; i++)
        {
            // check if they can be spawned
            if (enemies[i].isSpawnable)
            {
                // spawn the enemy
                enemies[i].setPosition(spawnPos);
                enemies[i].enable();
                return true; // successfully spawned an enemy
            }
        }
        return false; // enemy not spawned as there are none avaiable
    }
}
