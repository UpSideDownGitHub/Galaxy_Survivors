using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum EquippedWeapons
{
    PISTOL,
    SHOTGUN,
    ORBS,
    DRONE,
    ROCKETS,
    KNIFE,
    ACID,
    LIGHTNING,
    CANNONS
}

public class AttackManager : MonoBehaviour
{
    // the list of weapons that are currently active
    public List<Weapon> weapons = new List<Weapon>();
    public List<EquippedWeapons> activeWeapons = new List<EquippedWeapons>();

    // Start is called before the first frame update
    void Start()
    {
        callStartFunctions();
    }

    public void callStartFunctions()
    {
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            weapons[(int)activeWeapons[i]].startFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // call all of the updates on each of the active wepaons
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            weapons[(int)activeWeapons[i]].updateFrame();
        }
    }
    
    public void addWeapon(EquippedWeapons weapon)
    {
        activeWeapons.Add(weapon);
        weapons[(int)activeWeapons[activeWeapons.Count - 1]].startFrame();
    }
}
