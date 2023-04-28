using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    // public variables
    public PlayerPerks perks;

    // private variables
    private SaveManager _saveManager;

    // called before the first update frame
    void Start()
    {
        // reset the perks
        perks.minusDamageTaken = 0;
        perks.damageIncrease = 0;
        perks.movementSpeed = 0;
        perks.healthIncrease = 0;
        perks.healthRegen = 0;
        perks.fireRate = 0;
        perks.xpIncrease = 0;
        perks.coinsIncrease = 0;

        // load the save manager
        _saveManager = SaveManager.instance;

        // enable the current perks that are active
        enablePerk(_saveManager.data.currentPerk1);
        enablePerk(_saveManager.data.currentPerk2);
        enablePerk(_saveManager.data.currentPerk3);
    }

    /*
    *   this will enable the perk that was equipped in the menu
    */
    public void enablePerk(int perkID)
    {
        // switch depending on the perk that has been selected
        switch (perkID)
        {
            // Minus Damage Taken
            case 0:
                perks.minusDamageTaken = 1;
                break;
            // Damage Increase
            case 1:
                perks.damageIncrease = 1;
                break;
            // Movement Speed
            case 2:
                perks.movementSpeed = 1;
                break;
            // Health Increase
            case 3:
                perks.healthIncrease = 1;
                break;
            // Health Regen
            case 4:
                perks.healthRegen = 1;
                break;
            // Rate Of Fire
            case 5:
                perks.fireRate = 1;
                break;
            // XP Increase
            case 6:
                perks.xpIncrease = 1;
                break;
            // Coins Increase
            case 7:
                perks.coinsIncrease = 1;
                break;
            default:
                print("No Perk");
                break;
        }
    }
}
