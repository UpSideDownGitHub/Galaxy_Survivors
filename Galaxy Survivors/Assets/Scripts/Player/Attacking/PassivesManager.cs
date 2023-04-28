using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// list of passive abilities the player can have
public enum PassiveAbilities
{
    INCREASEDAMAGE,
    DECREASEDAMAGETAKEN,
    INCREASEHEALTH,
    INCREASEATTACKSPEED,
    INCREASEPROJECTILES,
    INCREASEMOVEMENTSPEED,
    INCREASEPICKUPRANGE,
    INCREASEXP
}

public class PassivesManager : MonoBehaviour
{
    // public variables
    public PlayerStats stats;
    public PassiveLevel levels;

    public int maxPassives;

    public List<PassiveAbilities> equippedPassives = new();
    public List<int> passiveLevels = new();

    // enable all of the passives and set there default values in the player stats
    public void Start()
    {
        stats.damageModifyer = 1;
        stats.reduceDamageTakenModifyer= 1;
        stats.healthModifyer = 1;
        stats.attackSpeed = 1;
        stats.projectileCount = 0;
        stats.movementSpeed = 1;
        stats.pickupModifyer = 1;
        stats.xpModifyer = 1;

        // set the passives to the initial level
        for (int i = 0; i < equippedPassives.Count; i++)
        {
            setPassivesLevel(equippedPassives[i], passiveLevels[i]);
        }
    }

    /*
    *   if it is possible to equip the given passive, then equip it, otherwise
    *   don't equip the passive
    */
    public void passiveEquipped(PassiveAbilities passive)
    {
        // already have this passive then don't add
        if (equippedPassives.Contains(passive))
            return;
        // if have can equip another passive
        if (equippedPassives.Count < maxPassives)
        { 
            // add the passive to the list of equipped passives
            equippedPassives.Add(passive);
            passiveLevels.Add(0);
            setPassivesLevel(passive, 0);
        }
    }

    // increase the level of the given passive
    public void passiveUpgrade(PassiveAbilities passive)
    {
        int currentPassive = equippedPassives.FindIndex(val => val == passive);
        passiveLevels[currentPassive]++;
        setPassivesLevel(passive, passiveLevels[currentPassive]);
    }

    /*
    *   this will apply the level of the given passive 
    */
    public void setPassivesLevel(PassiveAbilities passive, int level)
    {
        switch (passive)
        {
            case PassiveAbilities.INCREASEDAMAGE:
                stats.damageModifyer = levels.damageModifyer[level];
                break;
            case PassiveAbilities.DECREASEDAMAGETAKEN:
                stats.reduceDamageTakenModifyer = levels.reduceDamageTakenModifyer[level];
                break;
            case PassiveAbilities.INCREASEHEALTH:
                stats.healthModifyer = levels.healthModifyer[level];
                break;
            case PassiveAbilities.INCREASEATTACKSPEED:
                stats.attackSpeed = levels.attackSpeed[level];
                break;
            case PassiveAbilities.INCREASEPROJECTILES:
                stats.projectileCount = levels.projectileCount[level];
                break;
            case PassiveAbilities.INCREASEMOVEMENTSPEED:
                stats.movementSpeed = levels.movementSpeed[level];
                break;
            case PassiveAbilities.INCREASEPICKUPRANGE:
                stats.pickupModifyer = levels.pickupModifyer[level];
                break;
            case PassiveAbilities.INCREASEXP:
                stats.xpModifyer = levels.xpModifyer[level];
                break;
            default:
                break;
        }
    }
}
