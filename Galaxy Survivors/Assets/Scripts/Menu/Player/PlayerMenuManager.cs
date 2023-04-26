using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuManager : MonoBehaviour
{
    public Image currentPlayer;
    public TMP_Text infoText;
    public TMP_Text costText;
    public Image selectedPlayer;

    private int _currentSelected;

    public GameObject buyButton;
    public GameObject equipButton;

    public GameObject spawnUnder;
    public GameObject playerSpawnPrefab;

    public PlayerInfo info;
    private SaveManager _saveManager;

    public TMP_Text goldText;

    public void Start()
    {
        // loop through all of the options and then
        // show the options in the list of options
        _saveManager = SaveManager.instance;
        _saveManager.loadFromJson();
        for (int i = 0; i < _saveManager.data.playerInformation.Length; i++)
        {
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

    public void equipButtonPressed()
    {
        _saveManager.loadFromJson();
        _saveManager.data.currentPlayer = _currentSelected;
        _saveManager.saveIntoJson();
        currentPlayer.sprite = info.playerSprites[_currentSelected];
    }

    public void buttonPressed(int ID)
    {
        // do the thing that happens when the button is pressed
        _saveManager.loadFromJson();

        _currentSelected = ID;

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
        
        infoText.text = info.playerInfo[ID];
        costText.text = info.costs[ID].ToString();
        selectedPlayer.sprite = info.playerSprites[ID];
    }
}
