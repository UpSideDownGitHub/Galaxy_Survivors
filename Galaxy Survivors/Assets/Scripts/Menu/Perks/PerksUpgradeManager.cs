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
    private int[] equippedPerks = new int[] {-1,-1,-1};
    public TMP_Text infoText;
    public TMP_Text costText;
    public Image selectedPerk;

    private int[] _currentSelectedPerks;
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
    }

    public void buyButtonPressed()
    {
        _saveManager.loadFromJson();
        if (_saveManager.data.gold - info.costs[_currentSelected] > 0)
        {
            // can afford to buy
            _saveManager.data.gold -= info.costs[_currentSelected];
            _saveManager.data.playerInformation[_currentSelected].unlocked = true;
            _saveManager.saveIntoJson();
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            // do nothing as cannot afford
            return;
        }
    }

    public void equipButtonPressed()
    {
        for (int i = 0; i < equippedPerks.Length; i++)
        {
            if(equippedPerks[i] == _currentSelected)
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
        equippedPerks[currentSlotSelected] = currentSlotSelected;
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
        selectedPerk.sprite = info.perkSprite[ID];
    }

    public void buyExtra(int ID)
    {
        if (ID == 0)
        {
            perk2Button.SetActive(false);
            perk2.SetActive(true);
        }
        else if (ID == 1)
        {
            perk3Button.SetActive(false);
            perk3.SetActive(true);
        }
    }


    public void changeSlotSelected(int ID)
    {
        currentSlotSelected = ID;
    }
}
