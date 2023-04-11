using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isSpawnable;

    public int ID;

    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public void setFree()
    {
        disable();
    }

    public void enable(int pickupID)
    {
        gameObject.SetActive(true);
        isSpawnable = false;
        ID = pickupID;
    }
    public void disable()
    {
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
