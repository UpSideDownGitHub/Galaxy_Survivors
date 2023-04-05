using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem particle;

    public bool isSpawnable;

    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }
    public void setFree()
    {
        disable();
    }
    public void enable(Color color)
    {
        gameObject.SetActive(true);
        isSpawnable = false;
        var main = particle.main;
        main.startColor = color;
        particle.Play();
        Invoke("setFree", particle.main.duration + particle.main.startLifetime.constant + 0.1f);
    }

    public void disable()
    {
        particle.Stop();
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
