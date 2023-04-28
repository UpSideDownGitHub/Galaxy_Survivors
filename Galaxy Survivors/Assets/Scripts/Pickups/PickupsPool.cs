using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsPool : MonoBehaviour
{
    public int[] maxPickups;
    public List<Pickup> pickups = new List<Pickup>();
    public GameObject[] pickupPrefabs;

    public static PickupsPool instance;

    // called when the object is being loaded
    public void Awake()
    {
        instance = this;

        // this will spawn maxPickups ammount of each pickup and store them in the pickups list
        for (int j = 0; j < pickupPrefabs.Length; j++)
        {
            for (int i = 0; i < maxPickups[j]; i++)
            {
                pickups.Add(Instantiate(pickupPrefabs[j], new Vector3(0, 0, 0), new Quaternion()).GetComponent<Pickup>());
            }
        }

        // turn off all of the pickups
        for (int i = 0; i < pickups.Count; i++)
        {
            pickups[i].disable();
        }
    }

    // will return true if a pickup is spawned
    public bool spawnPickup(int enemyID, Vector2 spawnPos)
    {
        // get the bound in the list of the current pickup type to be spawned
        int minPos = 0;
        for (int i = 0; i < enemyID; i++)
            minPos += maxPickups[i];

        // for all of the pickups of the given type
        for (int i = minPos; i < minPos + maxPickups[enemyID]; i++)
        {
            // check if they can be spawned
            if (pickups[i].isSpawnable)
            {
                // spawn the pickup
                pickups[i].setPosition(spawnPos);
                pickups[i].enable(enemyID);
                return true; // successfully spawned an pickup
            }
        }
        return false; // pickup not spawned as there are none avaiable
    }
}
