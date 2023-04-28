using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuManager : MonoBehaviour
{
    // public variables
    public Image currentPlayer;
    public TMP_Text infoText;
    public TMP_Text costText;
    public Image selectedPlayer;
    public TMP_Text goldText;
    public GameObject buyButton;
    public GameObject equipButton;
    public GameObject spawnUnder;
    public GameObject playerSpawnPrefab;
    public PlayerInfo info;

    private SaveManager _saveManager;
    private int _currentSelected;

    
    // called before the first update frame
    public void Start()
    {
        // loop through all of the options and then
        // show the options in the list of options
        _saveManager = SaveManager.instance;
        _saveManager.loadFromJson();
        // for the ammount of players in the save data
        for (int i = 0; i < _saveManager.data.playerInformation.Length; i++)
        {
            // load all of the information about the player
            int ID = _saveManager.data.playerInformation[i].ID;
            Sprite tempSprite = info.playerSprites[ID];

            GameObject tempObj = Instantiate(playerSpawnPrefab, spawnUnder.transform);
            tempObj.GetComponent<Image>().sprite = tempSprite;
            tempObj.GetComponent<customButton>().setUp(ID, this);
        }
        goldText.text = _saveManager.data.gold.ToString();

        // LOAD IN THE CURRENT PLAYER
        currentPlayer.sprite = info.playerSprites[_saveManager.data.currentPlayer];
        buttonPressed(0);
    }
    // when the object is enabled
    public void OnEnable()
    {
        // there is a savemanger then update the ammount of gold
        if (_saveManager)
            goldText.text = _saveManager.data.gold.ToString();
    }

    // when the buy button is presed
    public void buyButtonPressed()
    {
        // load the save data
        _saveManager.loadFromJson();
        // if the player has enough money to buy
        if (_saveManager.data.gold - info.costs[_currentSelected] >= 0)
        {
            // can afford to buy
            _saveManager.data.gold -= info.costs[_currentSelected];
            goldText.text = _saveManager.data.gold.ToString();
            _saveManager.data.playerInformation[_currentSelected].unlocked = true;
            _saveManager.saveIntoJson();
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            print("Cant Afford This Item");
            // do nothing as cannot afford
            return;
        }
    }

    // if the equip button is pressed
    public void equipButtonPressed()
    {
        // load the data and set the current player to be the player that was equipped
        _saveManager.loadFromJson();
        _saveManager.data.currentPlayer = _currentSelected;
        _saveManager.saveIntoJson();
        currentPlayer.sprite = info.playerSprites[_currentSelected];
    }

    // if one of the buttons was pressed
    public void buttonPressed(int ID)
    {
        // do the thing that happens when the button is pressed
        _saveManager.loadFromJson();

        // set the selected item to be the item of the button pressed
        _currentSelected = ID;

        // if allready unclocked then set to equip button otherwise set to buy
        if (_saveManager.data.playerInformation[ID].unlocked)
        {
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            equipButton.SetActive(false);
            buyButton.SetActive(true);
        }
        
        // set all of the info for the selected item
        infoText.text = info.playerInfo[ID];
        costText.text = info.costs[ID].ToString();
        selectedPlayer.sprite = info.playerSprites[ID];
    }
}
