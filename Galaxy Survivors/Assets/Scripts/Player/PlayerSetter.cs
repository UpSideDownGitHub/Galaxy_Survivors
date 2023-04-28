using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    // public variables
    public Sprite[] players;
    public SpriteRenderer playerSprite;

    // private variables
    private SaveManager _saveManager;

    // called before the first update frame
    void Start()
    {
        // load the save data and get the current player sprite being used
        _saveManager = SaveManager.instance;
        playerSprite.sprite = players[_saveManager.data.currentPlayer];

        // turn off the loading screen as the new player has now been replaced
        GameObject loadingScreen = GameObject.FindGameObjectWithTag("Loading");
        loadingScreen.SetActive(false);
    }
}
