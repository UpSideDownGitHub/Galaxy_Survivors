using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLevels : ScriptableObject
{
    public int pistolLevel;
    public int shotGunLevel;
    public int orbsLevel;
    public int droneLevel;
    public int rocketsLevel;
    public int knifeLevel;
    public int acidLevel;
    public int lightningLevel;
    public int cannonsLevel;
    public int sheildLevel;

    public int[] weaponLevels
    {
        get
        {
            return new int[] {pistolLevel, shotGunLevel, orbsLevel, droneLevel, rocketsLevel, 
                knifeLevel, acidLevel, lightningLevel, cannonsLevel, sheildLevel};
        }
    }
}
