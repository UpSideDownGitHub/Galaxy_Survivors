using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

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
    PlayerStats stats;
    PassiveLevel levels;

    public int maxPassives;

    public List<PassiveAbilities> equippedPassives = new();
    public List<int> passiveLevels = new();

    public void passiveEquipped(PassiveAbilities passive)
    {
        if (equippedPassives.Contains(passive))
            return;
        if (equippedPassives.Count < maxPassives)
        { 
            equippedPassives.Add(passive);
            passiveLevels.Add(0);
            setPassivesLevel(passive, 0);
        }
    }

    public void passiveUpgrade(PassiveAbilities passive)
    {
        int currentPassive = equippedPassives.FindIndex(val => val == passive);
        passiveLevels[currentPassive]++;
        setPassivesLevel(passive, passiveLevels[currentPassive]);
    }

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
