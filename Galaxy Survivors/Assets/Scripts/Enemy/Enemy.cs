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



    public void enable() { gameObject.SetActive(true); }
    public void disable() { gameObject.SetActive(false); }

}
