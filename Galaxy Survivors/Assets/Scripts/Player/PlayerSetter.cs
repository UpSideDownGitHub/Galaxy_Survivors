using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    private SaveManager _saveManager;
    public Sprite[] players;
    public SpriteRenderer playerSprite;

    void Start()
    {
        _saveManager = SaveManager.instance;
        playerSprite.sprite = players[_saveManager.data.currentPlayer];

        GameObject loadingScreen = GameObject.FindGameObjectWithTag("Loading");
        loadingScreen.SetActive(false);
    }
}
