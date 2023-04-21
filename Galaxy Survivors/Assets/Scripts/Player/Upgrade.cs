using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Upgrade : MonoBehaviour
{
    /*
     * - Button
     * - Image
     * - Title
     * - Description
     * - New
     * - Level
    */
    [Header("UI Elements")]
    public GameObject[] upgradeUIElements;
    public GameObject mainLevelUpUI;
    public UpgradeInformation info;

    [Header("Links")]
    public PassivesManager passives;
    public AttackManager weapons;
    public PlayerHealth health;
    public WeaponLevels weaponLevels;

    [Header("Other")]
    public EquippedWeapons[] listOfWeapons;
    public PassiveAbilities[] listOfPassives;

    [Header("Selected Weapon")]
    List<EquippedWeapons> newWeapons = new List<EquippedWeapons>();
    List<EquippedWeapons> upgradeWeapons = new List<EquippedWeapons>();
    List<PassiveAbilities> newPassives = new List<PassiveAbilities>();
    List<PassiveAbilities> upgradePassives = new List<PassiveAbilities>();
    public itemSelected[] items = new itemSelected[3];

    public struct itemSelected
    {
        public int list;
        public int index;
    }

    public void Start()
    {
        weaponLevels.pistolLevel = 0;
        weaponLevels.shotGunLevel = 0;
        weaponLevels.orbsLevel = 0;
        weaponLevels.droneLevel = 0;
        weaponLevels.rocketsLevel = 0;
        weaponLevels.knifeLevel = 0;
        weaponLevels.acidLevel = 0;
        weaponLevels.lightningLevel = 0;
        weaponLevels.cannonsLevel = 0;
    }


    public void levelUp()
    {
        // stop gameplay and enable the UI
        Time.timeScale = 0f;
        mainLevelUpUI.SetActive(true);

        generate3Upgrades();
        getInfo();
    }



    public void getInfo()
    {
        for (int i = 0; i < upgradeUIElements.Length / 6; i++)
        {
            bool newItem = false;

            int index = 0;
            if (items[i].list == 1)
            {
                index = (int)newWeapons[items[i].index];
                newItem = true;
            }
            else if (items[i].list == 2)
                index = (int)upgradeWeapons[items[i].index];
            else if (items[i].list == 3)
            {
                index = (int)newPassives[items[i].index];
                newItem = true;
            }
            else if (items[i].list == 4)
                index = (int)upgradePassives[items[i].index];

            int index2;
            string title;
            Sprite upgrade1Icon;
            string description;

            if (items[i].list <= 2)
            {
                index2 = index * 3 + weaponLevels.weaponLevels[index];
                title = info.weaponTitle[index];
                upgrade1Icon = info.weaponSprites[index2];
                description = info.weaponDescription[index2];
            }
            else if (items[i].list == 3)
            {
                // as new will be the base level
                index2 = index * 3 + 0;
                title = info.passiveTitle[index];
                upgrade1Icon = info.passiveSprites[index2];
                description = info.passiveDescription[index2];
            }
            else
            {
                int index3 = passives.equippedPassives.IndexOf(listOfPassives[index]);
                index2 = index * 3 + passives.passiveLevels[index3];
                title = info.passiveTitle[index];
                upgrade1Icon = info.passiveSprites[index2];
                description = info.passiveDescription[index2];
            }

            upgradeUIElements[(i * 6) + 1].GetComponent<Image>().sprite = upgrade1Icon;
            upgradeUIElements[(i * 6) + 2].GetComponent<TMP_Text>().text = title;
            upgradeUIElements[(i * 6) + 3].GetComponent<TMP_Text>().text = description;
            
            if (newItem)
            {
                upgradeUIElements[(i * 6) + 4].SetActive(true);
                upgradeUIElements[(i * 6) + 5].SetActive(false);
            }
            else
            {
                upgradeUIElements[(i * 6) + 4].SetActive(false);
                upgradeUIElements[(i * 6) + 5].SetActive(true);
            }
        }
    }

    public void buttonPressed(int ID)
    {
        // apply the upgrade
        int i = ID;
        if (items[i].list == 1)
        {
            // new weapon
            weapons.addWeapon(newWeapons[items[i].index]);
        }
        else if (items[i].list == 2)
        {
            // weapon upgrade
            switch (upgradeWeapons[items[i].index])
            {
                case EquippedWeapons.PISTOL:
                    weaponLevels.pistolLevel++;
                    weapons.weapons[0].updateWeaponLevel();
                    break;
                case EquippedWeapons.SHOTGUN:
                    weaponLevels.shotGunLevel++;
                    weapons.weapons[1].updateWeaponLevel();
                    break;
                case EquippedWeapons.ORBS:
                    weaponLevels.orbsLevel++;
                    weapons.weapons[2].updateWeaponLevel();
                    break;
                case EquippedWeapons.DRONE:
                    weaponLevels.droneLevel++;
                    weapons.weapons[3].updateWeaponLevel();
                    break;
                case EquippedWeapons.ROCKETS:
                    weaponLevels.rocketsLevel++;
                    weapons.weapons[4].updateWeaponLevel();
                    break;
                case EquippedWeapons.KNIFE:
                    weaponLevels.knifeLevel++;
                    weapons.weapons[5].updateWeaponLevel();
                    break;
                case EquippedWeapons.ACID:
                    weaponLevels.acidLevel++;
                    weapons.weapons[6].updateWeaponLevel();
                    break;
                case EquippedWeapons.LIGHTNING:
                    weaponLevels.lightningLevel++;
                    weapons.weapons[7].updateWeaponLevel();
                    break;
                case EquippedWeapons.CANNONS:
                    weaponLevels.cannonsLevel++;
                    weapons.weapons[8].updateWeaponLevel();
                    break;
                default:
                    print("Should not be here");
                    break;
            }

        }
        else if (items[i].list == 3)
        {
            // new passive
            passives.passiveEquipped(newPassives[items[i].index]);
        }
        else if (items[i].list == 4)
        {
            // passive upgrade
            passives.passiveUpgrade(upgradePassives[items[i].index]);
        }
        // disable UI and enable the gameplay
        Time.timeScale = 1f;
        mainLevelUpUI.SetActive(false);
    }


    public void generate3Upgrades()
    {
        newWeapons = new List<EquippedWeapons>();
        upgradeWeapons = new List<EquippedWeapons>();
        newPassives = new List<PassiveAbilities>();
        upgradePassives = new List<PassiveAbilities>();

        // get the list of new and upgrade wepaons
        for (int i = 0; i < System.Enum.GetValues(typeof(EquippedWeapons)).Length; i++)
        {
            // if the weapon is allready equipped
            if (weapons.activeWeapons.Contains(listOfWeapons[i]))
            {
                // need to check if the weapon is at max level
                if (weaponLevels.weaponLevels[i] < 2)
                    upgradeWeapons.Add(listOfWeapons[i]);
            }
            // add the weapon to the new category
            else
                newWeapons.Add(listOfWeapons[i]);
        }

        // get the list of new and upgrade wepaons
        for (int i = 0; i < System.Enum.GetValues(typeof(PassiveAbilities)).Length; i++)
        {
            // if the weapon is allready equipped
            if (passives.equippedPassives.Contains(listOfPassives[i]))
            {
                // get the index of the already existing passive to get the same item from the level list
                int index = passives.equippedPassives.IndexOf(listOfPassives[i]);
                // if the level is not max level then add to list of upgrades
                if (passives.passiveLevels[index] < 2)
                    upgradePassives.Add(listOfPassives[i]);
            }
            // add the passive to the new category
            else
                newPassives.Add(listOfPassives[i]);
        }

        // generate which list should be used IE what type of item to offer the player
        // there are 4 items (new & upgrades of passives & weapons) then loop through them 
        // choosing the item to show the player from each of them.
        // NEED TO ACTOUNT FOR IF NONE OF THEM ARE AVAIABLE
        List<int> available = new List<int>();
        if (newWeapons.Count > 0)
            available.Add(1);
        if (upgradeWeapons.Count > 0)
            available.Add(2);
        if (newPassives.Count > 0)
            available.Add(3);
        if (upgradePassives.Count > 0)
            available.Add(4);

        var upgradeOptions = new int[] { 
            available[Random.Range(0, available.Count)],
            available[Random.Range(0, available.Count)],
            available[Random.Range(0, available.Count)]
        };

        for (int i = 0; i < upgradeOptions.Length; i++)
        {
            switch (upgradeOptions[i])
            {
                // New Weapon
                case 1:
                    items[i].list = 1;
                    items[i].index = Random.Range(0, newWeapons.Count);
                    break;
                // Weapon Upgrade
                case 2:
                    items[i].list = 2;
                    items[i].index = Random.Range(0, upgradeWeapons.Count);
                    break;
                // New Passive
                case 3:
                    items[i].list = 3;
                    items[i].index = Random.Range(0, newPassives.Count);
                    break;
                // Passive Upgrade
                case 4:
                    items[i].list = 4;
                    items[i].index = Random.Range(0, upgradePassives.Count);
                    break;
                default:
                    print("Should be impossible to be here");
                    break;
            }
        }
    }
}
