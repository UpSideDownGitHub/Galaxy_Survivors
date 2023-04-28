using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("Map Selection")]
    public Button[] mapButtons;
    public Image[] mapObjects;
    public Color[] mapColors;
    private int _previousMapPressed;

    [Header("Loading Screen")]
    public GameObject LoadingScreen;

    [Header("Data")]
    private SaveManager _saveManager;

    [Header("Gold")]
    public TMP_Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        // load all of the information of the map
        _saveManager = SaveManager.instance;
        _saveManager.loadFromJson();
        goldText.text = _saveManager.data.gold.ToString();
        // for all of the maps
        for (int i = 0; i < _saveManager.data.mapsUnlocked.Length; i++)
        {
            // if it is locked then disable the button otherwise enable the button
            if (!_saveManager.data.mapsUnlocked[i])
                mapButtons[i].interactable = false;
            else
                mapButtons[i].interactable = true;
        }
        _saveManager.saveIntoJson();
        _saveManager.loadFromJson();
    }

    // when obejct is enabled and there is savedata then show the gold
    public void OnEnable()
    {
        if (_saveManager)
            goldText.text = _saveManager.data.gold.ToString();
    }

    // when the play button is pressed then show the loading and load the map
    public void playPressed()
    {
        // load the map with the correct color but for now i am just going to load the first map as this feature needs some development yet
        Invoke("dontAsk", 2);
        _saveManager.data.currentMap = _previousMapPressed;
        _saveManager.saveIntoJson();
        _saveManager.loadFromJson();
        LoadingScreen.SetActive(true);
    }

    // used to load the map in a given interval to show the loading screen
    public void dontAsk()
    {
        // this is mearly here so the loading screen can be seen
        SceneManager.LoadSceneAsync(1);
    }

    // when a map is pressed set the current map to be that map and also unselect the previous
    // map 
    public void mapPressed(int ID)
    {
        mapObjects[_previousMapPressed].color = mapColors[_previousMapPressed];
        _previousMapPressed = ID;
        mapObjects[ID].color = Color.white;
    }
}
