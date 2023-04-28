using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerksUpgradeManager : MonoBehaviour
{
    [Header("Player Slots")]
    public int currentSlotSelected;

    [Header("UI Changing ")]
    public Image[] perk;
    private int[] equippedPerks = new int[] { -1, -1, -1 };
    public TMP_Text infoText;
    public TMP_Text costText;
    public TMP_Text perkNameText;
    public Image selectedPerk;
    public Color color;

    private int _previousSlotSelected;
    private int _currentSelected;

    [Header("Buttons")]
    public GameObject buyButton;
    public GameObject equipButton;

    [Header("Spawning")]
    public GameObject spawnUnder;
    public GameObject playerSpawnPrefab;

    [Header("Data")]
    public PerkInfoMenu info;
    private SaveManager _saveManager;

    [Header("Extra Buttons")]
    public GameObject perk2Button;
    public GameObject perk3Button;
    public GameObject perk2;
    public GameObject perk3;
    public int[] extraButonCosts;
    public TMP_Text[] extraPerkCostsText;

    [Header("Gold")]
    public TMP_Text goldText;

    // called before the first update frame
    public void Start()
    {
        // loop through all of the options and then
        // show the options in the list of options
        _saveManager = SaveManager.instance;
        _saveManager.loadFromJson();

        // for the ammount of perks
        for (int i = 0; i < _saveManager.data.perks.Length; i++)
        {
            // load all of the information for each perk
            int ID = _saveManager.data.perks[i].ID;
            Sprite tempSprite = info.perkSprite[ID];

            GameObject tempObj = Instantiate(playerSpawnPrefab, spawnUnder.transform);
            tempObj.GetComponent<Image>().sprite = tempSprite;
            tempObj.GetComponent<customButton>().setUp(ID, this);
        }
        // for the costs of the perks
        for (int i = 0; i < extraPerkCostsText.Length; i++)
        {
            // set all of the costs
            extraPerkCostsText[i].text = extraButonCosts[i].ToString();
        }
        goldText.text = _saveManager.data.gold.ToString();

        // LOAD IN ALL THE DATA FROM THE SAVE DATA
        if (_saveManager.data.currentPerk1 > -1 )
            perk[0].sprite = info.perkSprite[_saveManager.data.currentPerk1];
        if (_saveManager.data.perksUnlocked[0])
        {
            perk2Button.SetActive(false);
            perk2.SetActive(true);
            if (_saveManager.data.currentPerk2 > -1)
                perk[1].sprite = info.perkSprite[_saveManager.data.currentPerk2];
        }
        if (_saveManager.data.perksUnlocked[1])
        {
            perk3Button.SetActive(false);
            perk3.SetActive(true);
            if (_saveManager.data.currentPerk3 > -1)
                perk[2].sprite = info.perkSprite[_saveManager.data.currentPerk3];
        }
        buttonPressed(0);
    }

    // when the object is enabled
    public void OnEnable()
    {
        // if there is a save manager load the gold
        if (_saveManager)
            goldText.text = _saveManager.data.gold.ToString();
    }

    // if the buy button is pressed
    public void buyButtonPressed()
    {
        // if the player has enough gold the buy and take of the gold then save
        _saveManager.loadFromJson();
        if (_saveManager.data.gold - info.costs[_currentSelected] >= 0)
        {
            // can afford to buy
            _saveManager.data.gold -= info.costs[_currentSelected];
            goldText.text = _saveManager.data.gold.ToString();
            _saveManager.data.perks[_currentSelected].unlocked = true;
            _saveManager.saveIntoJson();
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            // do nothing as cannot afford
            print("Cant Afford This Item");
            return;
        }
    }

    // if the equip button is pressed then equip the perk
    public void equipButtonPressed()
    {
        // for the list of equpped perks 
        for (int i = 0; i < equippedPerks.Length; i++)
        {
            // if allready equipped this perk then dont add
            if (equippedPerks[i] == _currentSelected)
            {
                print("Allready Equipped");
                return;
            }
        }

        // load the save data and apply the perk to the correct perk position
        _saveManager.loadFromJson();
        if (currentSlotSelected == 0)
            _saveManager.data.currentPerk1 = _currentSelected;
        else if (currentSlotSelected == 1)
            _saveManager.data.currentPerk2 = _currentSelected;
        else if (currentSlotSelected == 2)
            _saveManager.data.currentPerk3 = _currentSelected;
        _saveManager.saveIntoJson();

        // equip the given perk
        perk[currentSlotSelected].sprite = info.perkSprite[_currentSelected];
        equippedPerks[currentSlotSelected] = _currentSelected;
    }

    // perk button pressed
    public void buttonPressed(int ID)
    {
        // do the thing that happens when the button is pressed
        _saveManager.loadFromJson();

        _currentSelected = ID;

        // if already brough show equp otherwise show buy
        if (_saveManager.data.perks[ID].unlocked)
        {
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            equipButton.SetActive(false);
            buyButton.SetActive(true);
        }

        // show all of the perk info
        infoText.text = info.perkInfo[ID];
        costText.text = info.costs[ID].ToString();
        perkNameText.text = info.names[ID];
        selectedPerk.sprite = info.perkSprite[ID];
    }

    // when the player tries to buy an extra perk slot
    public void buyExtra(int ID)
    {
        _saveManager.loadFromJson();

        // if try the first slot
        if (ID == 0)
        {
            // if have enough gold
            if (_saveManager.data.gold - extraButonCosts[0] >= 0)
            {
                // buy the perk and enable it
                perk2Button.SetActive(false);
                perk2.SetActive(true);

                _saveManager.data.gold -= extraButonCosts[0];
                goldText.text = _saveManager.data.gold.ToString();
                _saveManager.data.perksUnlocked[0] = true;
                _saveManager.saveIntoJson();
            }
            else
                print("Cant Afford Perk Slot");
        }
        // 2nd perk
        else if (ID == 1)
        {
            // if have enough gold
            if (_saveManager.data.gold - extraButonCosts[1] >= 0)
            {
                // enable the 3rd perk options
                perk3Button.SetActive(false);
                perk3.SetActive(true);

                // udpate the save data
                _saveManager.data.gold -= extraButonCosts[1];
                goldText.text = _saveManager.data.gold.ToString();
                _saveManager.data.perksUnlocked[1] = true;
                _saveManager.saveIntoJson();
            }
            else
                print("Cant Afford Perk Slot");
        }
    }


    // when the slot selected is changed
    public void changeSlotSelected(int ID)
    {
        // change the current slot
        currentSlotSelected = ID;

        // update the previous slot
        perk[_previousSlotSelected].color = color;
        _previousSlotSelected = currentSlotSelected;
        perk[currentSlotSelected].color = Color.red;

        // show info of perk if there is a perk in that slot
        if (equippedPerks[currentSlotSelected] >= 0)
        {
            infoText.text = info.perkInfo[equippedPerks[currentSlotSelected]];
            costText.text = info.costs[equippedPerks[currentSlotSelected]].ToString();
            perkNameText.text = info.names[equippedPerks[currentSlotSelected]];
            selectedPerk.sprite = info.perkSprite[equippedPerks[currentSlotSelected]];
        }
    }
}
