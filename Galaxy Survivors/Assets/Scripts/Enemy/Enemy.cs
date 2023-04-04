using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isSpawnable;

    public void setPosition(Vector2 pos)
    {
        transform.position = pos;
    }
    public void setFree()
    {
        disable();
    }
    public void enable()
    {
        gameObject.SetActive(true);
        isSpawnable = false;
    }
    public void disable()
    {
        gameObject.SetActive(false);
        isSpawnable = true;
    }
}
