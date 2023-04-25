using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public PlayerPerks perks;
    private SaveManager _saveManager;

    void Start()
    {
        perks.minusDamageTaken = 0;
        perks.damageIncrease = 0;
        perks.movementSpeed = 0;
        perks.healthIncrease = 0;
        perks.healthRegen = 0;
        perks.fireRate = 0;
        perks.xpIncrease = 0;
        perks.coinsIncrease = 0;

        _saveManager = SaveManager.instance;
        enablePerk(_saveManager.data.currentPerk1);
        enablePerk(_saveManager.data.currentPerk2);
        enablePerk(_saveManager.data.currentPerk3);
    }

    public void enablePerk(int perkID)
    {
        switch (perkID)
        {
            case 0:
                perks.minusDamageTaken = 1;
                break;
            case 1:
                perks.damageIncrease = 1;
                break;
            case 2:
                perks.movementSpeed = 1;
                break;
            case 3:
                perks.healthIncrease = 1;
                break;
            case 4:
                perks.healthRegen = 1;
                break;
            case 5:
                perks.fireRate = 1;
                break;
            case 6:
                perks.xpIncrease = 1;
                break;
            case 7:
                perks.coinsIncrease = 1;
                break;
            default:
                print("No Perk");
                break;
        }
    }
}
