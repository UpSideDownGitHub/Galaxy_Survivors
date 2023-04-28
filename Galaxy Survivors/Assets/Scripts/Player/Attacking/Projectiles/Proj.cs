using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj : MonoBehaviour
{
    public bool isSpawnable;

    // set the position of the object to the given position
    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    // set the rotation of the object to the given rotation
    public void setRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

    // set the object free
    public void setFree()
    {
        disable();
    }

    // enable the object
    public void enable()
    {
        gameObject.SetActive(true);
        isSpawnable = false;
    }

    // turn of the object so it can be spawned again
    public void disable()
    {
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
