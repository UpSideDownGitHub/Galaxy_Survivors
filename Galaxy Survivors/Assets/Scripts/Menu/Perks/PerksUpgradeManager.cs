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

    public void Start()
    {
        // loop through all of the options and then
        // show the options in the list of options
        _saveManager = SaveManager.instance;
        _saveManager.loadFromJson();
        for (int i = 0; i < _saveManager.data.perks.Length; i++)
        {
            int ID = _saveManager.data.perks[i].ID;
            Sprite tempSprite = info.perkSprite[ID];

            GameObject tempObj = Instantiate(playerSpawnPrefab, spawnUnder.transform);
            tempObj.GetComponent<Image>().sprite = tempSprite;
            tempObj.GetComponent<customButton>().setUp(ID, this);
        }
        for (int i = 0; i < extraPerkCostsText.Length; i++)
        {
            extraPerkCostsText[i].text = extraButonCosts[i].ToString();
        }
        goldText.text = _saveManager.data.gold.ToString();
    }

    public void OnEnable()
    {
        if (_saveManager)
            goldText.text = _saveManager.data.gold.ToString();
    }

    public void buyButtonPressed()
    {
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

    public void equipButtonPressed()
    {
        for (int i = 0; i < equippedPerks.Length; i++)
        {
            if (equippedPerks[i] == _currentSelected)
            {
                print("Allready Equipped");
                return;
            }
        }

        _saveManager.loadFromJson();
        if (currentSlotSelected == 0)
            _saveManager.data.currentPerk1 = _currentSelected;
        else if (currentSlotSelected == 1)
            _saveManager.data.currentPerk2 = _currentSelected;
        else if (currentSlotSelected == 2)
            _saveManager.data.currentPerk3 = _currentSelected;
        _saveManager.saveIntoJson();

        //
        perk[currentSlotSelected].sprite = info.perkSprite[_currentSelected];
        equippedPerks[currentSlotSelected] = _currentSelected;
    }

    public void buttonPressed(int ID)
    {
        // do the thing that happens when the button is pressed
        _saveManager.loadFromJson();

        _currentSelected = ID;

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

        infoText.text = info.perkInfo[ID];
        costText.text = info.costs[ID].ToString();
        perkNameText.text = info.names[ID];
        selectedPerk.sprite = info.perkSprite[ID];
    }

    public void buyExtra(int ID)
    {
        _saveManager.loadFromJson();

        if (ID == 0)
        {
            if (_saveManager.data.gold - extraButonCosts[0] >= 0)
            {
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
        else if (ID == 1)
        {
            if (_saveManager.data.gold - extraButonCosts[1] >= 0)
            {
                perk3Button.SetActive(false);
                perk3.SetActive(true);

                _saveManager.data.gold -= extraButonCosts[1];
                goldText.text = _saveManager.data.gold.ToString();
                _saveManager.data.perksUnlocked[1] = true;
                _saveManager.saveIntoJson();
            }
            else
                print("Cant Afford Perk Slot");
        }
    }


    public void changeSlotSelected(int ID)
    {
        currentSlotSelected = ID;

        perk[_previousSlotSelected].color = color;
        _previousSlotSelected = currentSlotSelected;
        perk[currentSlotSelected].color = Color.red;

        if (equippedPerks[currentSlotSelected] >= 0)
        {
            infoText.text = info.perkInfo[equippedPerks[currentSlotSelected]];
            costText.text = info.costs[equippedPerks[currentSlotSelected]].ToString();
            perkNameText.text = info.names[equippedPerks[currentSlotSelected]];
            selectedPerk.sprite = info.perkSprite[equippedPerks[currentSlotSelected]];
        }
    }
}
