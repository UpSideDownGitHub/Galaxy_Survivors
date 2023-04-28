using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
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

    [Header("Equipped UI")]
    public Image[] weaponSprites;
    public EquippedWeapons[] equippedWeaponSprites;
    public Image[] passiveSprites;
    public PassiveAbilities[] equippedPassivesSprites;
    private int _maxEquips = 5;
    private int _currentWeapon = 1;
    private int _currentPassive = 0;

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

    private int _level = 0;

    public struct itemSelected
    {
        public int list;
        public int index;
    }

    // called when the object is being loaded
    public void Awake()
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

    /*
     *  is called when the player reaches the next level
    */ 
    public void levelUp()
    {
        // if the player has reached the max level, then dont
        if (_level > 28)
            return;
        
        // stop gameplay and enable the UI
        Time.timeScale = 0f;
        mainLevelUpUI.SetActive(true);

        // get the 3 upgrades, then get the info for them to show the upgrades to the user
        generate3Upgrades();
        getInfo();
    }


    /*
     *  gets all of the information, from the 3 upgrades, then displays them to the user 
    */ 
    public void getInfo()
    {
        // for all of the upgrades that have been chosen, which is 3
        for (int i = 0; i < upgradeUIElements.Length / 6; i++)
        {
            // enable the current upgrade options
            upgradeUIElements[(i * 6) + 1].transform.parent.gameObject.SetActive(true);
            // if there is not one then this will fail so in that case just turn it off
            try
            {
                bool newItem = false;

                // if a New Weapon then add it
                int index = 0;
                if (items[i].list == 1)
                {
                    index = (int)newWeapons[items[i].index];
                    newItem = true;
                }
                // if a Upgrade Weapon then add it
                else if (items[i].list == 2)
                    index = (int)upgradeWeapons[items[i].index];
                // if a New Passive then add it
                else if (items[i].list == 3)
                {
                    index = (int)newPassives[items[i].index];
                    newItem = true;
                }
                // if a Upgrade Passive then add it
                else if (items[i].list == 4)
                    index = (int)upgradePassives[items[i].index];

                // VARIABLES
                int index2;
                string title;
                Sprite upgrade1Icon;
                string description;

                // upgrading weapons
                if (items[i].list <= 2)
                {
                    // get all of the info to show the user
                    index2 = index * 3 + weaponLevels.weaponLevels[index] + 1;
                    title = info.weaponTitle[index];
                    upgrade1Icon = info.weaponSprites[index2];
                    description = info.weaponDescription[index2];
                }
                // new Passive
                else if (items[i].list == 3)
                {
                    // get all of the info to show the user
                    index2 = index * 3 + 0;
                    title = info.passiveTitle[index];
                    upgrade1Icon = info.passiveSprites[index2];
                    description = info.passiveDescription[index2];
                }
                // for new passives & new wepaons
                else
                {
                    // get all of the info to show the user
                    int index3 = passives.equippedPassives.IndexOf(listOfPassives[index]);
                    index2 = index * 3 + passives.passiveLevels[index3] + 1;
                    title = info.passiveTitle[index];
                    upgrade1Icon = info.passiveSprites[index2];
                    description = info.passiveDescription[index2];
                }

                // set the information to be show
                upgradeUIElements[(i * 6) + 1].GetComponent<Image>().sprite = upgrade1Icon;
                upgradeUIElements[(i * 6) + 2].GetComponent<TMP_Text>().text = title;
                upgradeUIElements[(i * 6) + 3].GetComponent<TMP_Text>().text = description;

                // if a new item then show "new" otherwise show "Level Up"
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
            catch
            {
                // this means that there is only one upgrade left so dont show the empty slots
                upgradeUIElements[(i * 6) + 1].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    /*
     * this is called when an option is selected and will enable the given powerups 
    */ 
    public void buttonPressed(int ID)
    {
        // apply the upgrade
        _level++;
        int i = ID;
        if (items[i].list == 1)
        {
            // new weapon
            weapons.addWeapon(newWeapons[items[i].index]);
            // set the UI for the new options being added
            equippedWeaponSprites[_currentWeapon] = newWeapons[items[i].index];
            weaponSprites[_currentWeapon].sprite = upgradeUIElements[(i * 6) + 1].GetComponent<Image>().sprite;
            _currentWeapon++;

        }
        else if (items[i].list == 2)
        {
            // need to find the position of the wepaon being upgraded
            int index = Array.FindIndex(equippedWeaponSprites, element => element == upgradeWeapons[items[i].index]);
            weaponSprites[index].sprite = upgradeUIElements[(i * 6) + 1].GetComponent<Image>().sprite;
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
            equippedPassivesSprites[_currentPassive] = newPassives[items[i].index];
            passiveSprites[_currentPassive].sprite = upgradeUIElements[(i * 6) + 1].GetComponent<Image>().sprite;
            _currentPassive++;
        }
        else if (items[i].list == 4)
        {
            // passive upgrade
            int index = Array.FindIndex(equippedPassivesSprites, element => element == upgradePassives[items[i].index]);
            passiveSprites[index].sprite = upgradeUIElements[(i * 6) + 1].GetComponent<Image>().sprite;
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
            // add the weapon to the new category if there is not currently 5 weapons equipped
            else if (_currentWeapon < _maxEquips)
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
            // add the passive to the new category if there is not currently 5 passives equipped
            else if (_currentPassive < _maxEquips)
                newPassives.Add(listOfPassives[i]);
        }

        /*
        print("New Weapons Count: " + newWeapons.Count);
        print("Weapon Upgrade Count: " + upgradeWeapons.Count);
        print("New Passives Count: " + newPassives.Count);
        print("Passive Upgrade Count: " + upgradePassives.Count);
        print("\n\n");
        */

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

        // if there are no options then do nothing as there is nothing to do
        if (available.Count == 0)
            return;

        // for each of the items select the upgrade to show
        var upgradeOptions = new int[3];
        // loop through all of the options to select an item for it
        for (int i = 0; i < upgradeOptions.Length; i++)
        {
            // try 10 times, 10 is a max, the idea is to repeat until, 
            // an item is found, but set limit to 10 to stop lag
            for (int J = 0; J < 10; J++)
            {
                // depending on the upgrade selected, find one out of the options,
                // that are available (then if the correct one is found then stop the
                // generation and move to the next item)
                int temp = available[Random.Range(0, available.Count)];
                if (temp == 1)
                {
                    int[] count = Array.FindAll(upgradeOptions, element => element == 1);
                    int ammount = count.Length;
                    if (ammount >= newWeapons.Count)
                        continue;
                    upgradeOptions[i] = temp;
                    break;
                }
                else if (temp == 2)
                {
                    int[] count = Array.FindAll(upgradeOptions, element => element == 2);
                    int ammount = count.Length;
                    if (ammount >= upgradeWeapons.Count)
                        continue;
                    upgradeOptions[i] = temp;
                    break;
                }
                else if (temp == 3)
                {
                    int[] count = Array.FindAll(upgradeOptions, element => element == 3);
                    int ammount = count.Length;
                    if (ammount >= newPassives.Count)
                        continue;
                    upgradeOptions[i] = temp;
                    break;
                }
                else if (temp == 4)
                {
                    int[] count = Array.FindAll(upgradeOptions, element => element == 4);
                    int ammount = count.Length;
                    if (ammount >= upgradePassives.Count)
                        continue;
                    upgradeOptions[i] = temp;
                    break;
                }
            }
        }

        //print("Options Chosen: " + upgradeOptions[0] + ", " + upgradeOptions[1] + ", " + upgradeOptions[2]);

        // for all of the selected upgrades that have been selected
        for (int i = 0; i < upgradeOptions.Length; i++)
        {
            // for each upgrade
            switch (upgradeOptions[i])
            {
                // New Weapon
                case 1:
                    items[i].list = 1;
                    var rand = Random.Range(0, newWeapons.Count);
                    // do this a couple of times to make sure we get an original
                    for (int j = 0; j < 50; j++)
                    {
                        if (items[0].list == 1 && items[0].index == rand ||
                            items[1].list == 1 && items[1].index == rand ||
                            items[2].list == 1 && items[2].index == rand)
                            rand = Random.Range(0, newWeapons.Count);
                        else
                            break;
                    }
                    items[i].index = rand;
                    break;
                // Weapon Upgrade
                case 2:
                    items[i].list = 2;
                    var rand2 = Random.Range(0, upgradeWeapons.Count);
                    // do this a couple of times to make sure we get an orignal
                    for (int j = 0; j < 50; j++)
                    {
                        if (items[0].list == 2 && items[0].index == rand2 ||
                            items[1].list == 2 && items[1].index == rand2 ||
                            items[2].list == 2 && items[2].index == rand2)
                            rand2 = Random.Range(0, upgradeWeapons.Count);
                        else
                            break;
                    }
                    items[i].index = rand2;
                    break;
                // New Passive
                case 3:
                    items[i].list = 3;
                    var rand3 = Random.Range(0, newPassives.Count);
                    // do this a couple of times to make sure we get an orignal
                    for (int j = 0; j < 50; j++)
                    {
                        if (items[0].list == 3 && items[0].index == rand3 ||
                            items[1].list == 3 && items[1].index == rand3 ||
                            items[2].list == 3 && items[2].index == rand3)
                            rand3 = Random.Range(0, newPassives.Count);
                        else
                            break;
                    }
                    items[i].index = rand3;
                    break;
                // Passive Upgrade
                case 4:
                    items[i].list = 4;
                    var rand4 = Random.Range(0, upgradePassives.Count);
                    // do this a couple of times to make sure we get an orignal
                    for (int j = 0; j < 50; j++)
                    {
                        if (items[0].list == 4 && items[0].index == rand4 ||
                            items[1].list == 4 && items[1].index == rand4 ||
                            items[2].list == 4 && items[2].index == rand4)
                            rand4 = Random.Range(0, upgradePassives.Count);
                        else
                            break;
                    }
                    items[i].index = rand4;
                    break;
                default:
                    print("Should be impossible to be here");
                    break;
            }
        }
        /*
        print("Items:");
        print("Item 1: " + items[0].list + ", " + items[0].index);
        print("Item 2: " + items[1].list + ", " + items[1].index);
        print("Item 3: " + items[2].list + ", " + items[2].index);
        */
    }
}
