using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject[] upgrade1;
    public GameObject[] upgrade2;
    public GameObject[] upgrade3;
    public GameObject mainLevelUpUI;

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


    public void levelUp()
    {
        // stop gameplay and enable the UI
        Time.timeScale = 0f;
        mainLevelUpUI.SetActive(true);

        generate3Upgrades();
        getInfo();

        // add listeners to the buttons to be call buttonPressed which will apply the 
        // selected item
        upgrade1[0].GetComponent<Button>().onClick.AddListener(() => buttonPressed(0));
        upgrade2[0].GetComponent<Button>().onClick.AddListener(() => buttonPressed(1));
        upgrade3[0].GetComponent<Button>().onClick.AddListener(() => buttonPressed(2));
    }

    

    public void getInfo()
    {
        // get all of the info for the 3 selected items
    }

    public void buttonPressed(int ID)
    {
        // apply the upgrade

        // remove listeners from the buttons
        upgrade1[0].GetComponent<Button>().onClick.RemoveAllListeners();
        upgrade2[0].GetComponent<Button>().onClick.RemoveAllListeners();
        upgrade3[0].GetComponent<Button>().onClick.RemoveAllListeners();
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
        var upgradeOptions = new int[] { Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5) };
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
