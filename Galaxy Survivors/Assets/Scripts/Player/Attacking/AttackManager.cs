using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // the list of weapons that are currently active
    public List<Weapon> activeWeapons = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        callStartFunctions();
    }

    public void callStartFunctions()
    {
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            activeWeapons[i].startFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // call all of the updates on each of the active wepaons
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            activeWeapons[i].updateFrame();
        }
    }
}
