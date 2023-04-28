using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // public variables
    public ParticleSystem particle;
    public bool isSpawnable;

    /*
     * will set the position of the particle
    */
    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }
    /*
     * will tell the pooler that this particle can be spawned again
    */
    public void setFree()
    {
        disable();
    }

    /*
     * when called will give the particle the correct color, and enable it, 
     * it will then play the particle effect, and invoke the setFree() method
     * when the particle has finished playing is effect
    */
    public void enable(Color color)
    {
        gameObject.SetActive(true);
        isSpawnable = false;
        var main = particle.main;
        main.startColor = color;

        // play the particle effect then when it is finished set it free again
        particle.Play();
        Invoke("setFree", particle.main.duration + particle.main.startLifetime.constant + 0.1f);
    }

    /*
     *  turn off the particle and make it so it can be used again 
    */
    public void disable()
    {
        particle.Stop();
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
