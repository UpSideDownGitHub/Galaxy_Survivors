using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isSpawnable;

    // used to set the position of the enem
    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }
    // used to set the enemy free
    public void setFree()
    {
        disable();
    }
    // when enabled set the object to be visable and make no longer poolable
    public void enable()
    {
        gameObject.SetActive(true);
        isSpawnable = false;
    }
    // when the obejct is desotyed set to turned off and now spawnable by the enemy pooler
    public void disable()
    {
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
