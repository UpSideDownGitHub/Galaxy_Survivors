using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlePooler : MonoBehaviour
{
    public int[] maxParticles;
    public List<Particle> particles = new List<Particle>();
    public GameObject[] particlePrefabs;

    public static ParticlePooler instance;

    public void Awake()
    {
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
