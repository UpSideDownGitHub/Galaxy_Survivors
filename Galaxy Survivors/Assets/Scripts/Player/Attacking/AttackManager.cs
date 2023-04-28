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

    /*
    *   this will call all of the updates functions of all of the weapons, that are
    *   currently equipped
    */
    public void callStartFunctions()
    {
        // loop through all of the active weapons
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            // call the start frame of the weapons
            weapons[(int)activeWeapons[i]].startFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // call all of the updates on each of the active weapons
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            // call the update from to the weapons
            weapons[(int)activeWeapons[i]].updateFrame();
        }
    }
    
    /*
    *   when called this will add a weapon to the list of weapons,
    *   as well as this it will also call the start frame of the added weapon
    *   so it is initialize properly
    */
    public void addWeapon(EquippedWeapons weapon)
    {
        // add the weapon to the list and call the start frame
        activeWeapons.Add(weapon);
        weapons[(int)activeWeapons[activeWeapons.Count - 1]].startFrame();
    }
}
