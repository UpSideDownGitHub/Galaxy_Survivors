using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlePooler : MonoBehaviour
{
    // variables
    public int[] maxParticles;
    public List<Particle> particles = new List<Particle>();
    public GameObject[] particlePrefabs;
    public static ParticlePooler instance;

    // called as the object is being loaded
    public void Awake()
    {
        // set the instance of this to be the current object menaing this can be accessed from anyware in the project
        instance = this;

        // this will spawn maxParticles ammount of each particle and store them in the particles list
        for (int j = 0; j < particlePrefabs.Length; j++)
        {
            for (int i = 0; i < maxParticles[j]; i++)
            {
                particles.Add(Instantiate(particlePrefabs[j], new Vector3(0, 0, 0), new Quaternion()).GetComponent<Particle>());
            }
        }

        // turn off all of the particles
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].disable();
        }
    }

    /*
     *  when called will try and spawn a particle of type enemyID,
     *  at position spawnPos, with the given color.
     *  
     *  if it cannot spawn a particle then it will return false meaning nothing
     *  spawned, this would be caused by there being none available
    */
    public bool spawnParticle(int enemyID, Vector2 spawnPos, Color color)
    {
        // get the bound in the list of the current particle type to be spawned
        int minPos = 0;
        for (int i = 0; i < enemyID; i++)
            minPos += maxParticles[i];

        // for all of the particles of the given type
        for (int i = minPos; i < minPos + maxParticles[enemyID]; i++)
        {
            // check if they can be spawned
            if (particles[i].isSpawnable)
            {
                // spawn the particle
                particles[i].setPosition(spawnPos);
                particles[i].enable(color);
                return true; // successfully spawned an particle
            }
        }
        return false; // particle not spawned as there are none avaiable
    }
}
