using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // public variables
    public bool isSpawnable;

    public int ID;

    // set the position of the pickups, to the given position
    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    // set the pickup free to be used by the pooler again
    public void setFree()
    {
        disable();
    }

    // enable the object and sets it to active and no longer spawnable
    public void enable(int pickupID)
    {
        gameObject.SetActive(true);
        isSpawnable = false;
        ID = pickupID;
    }

    // disable the object and make it so that is is spawnable
    public void disable()
    {
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
