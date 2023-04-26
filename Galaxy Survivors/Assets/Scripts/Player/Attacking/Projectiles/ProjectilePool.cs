using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public int[] maxProj;
    public List<Proj> proj = new List<Proj>();
    public GameObject[] projPrefabs;

    public static ProjectilePool instance;

    public void Awake()
    {
        instance = this;

        // this will spawn maxProj ammount of each pickup and store them in the proj list
        for (int j = 0; j < projPrefabs.Length; j++)
        {
            for (int i = 0; i < maxProj[j]; i++)
            {
                proj.Add(Instantiate(projPrefabs[j], new Vector3(0, 0, 0), new Quaternion()).GetComponent<Proj>());
            }
        }

        // turn off all of the proj
        for (int i = 0; i < proj.Count; i++)
        {
            proj[i].disable();
        }
    }

    public GameObject spawnPickup(int projID, Vector2 spawnPos, Quaternion rot)
    {
        // get the bound in the list of the current pickup type to be spawned
        int minPos = 0;
        for (int i = 0; i < projID; i++)
            minPos += maxProj[i];

        // for all of the proj of the given type
        for (int i = minPos; i < minPos + maxProj[projID]; i++)
        {
            // check if they can be spawned
            if (proj[i].isSpawnable)
            {
                // spawn the pickup
                proj[i].setPosition(spawnPos);
                proj[i].setRotation(rot);
                proj[i].enable();
                return proj[i].gameObject; // successfully spawned an projectile
            }
        }
        return null; // projectile not spawned as there are none avaiable
    }
}
