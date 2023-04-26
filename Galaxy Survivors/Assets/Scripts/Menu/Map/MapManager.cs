using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("Map Selection")]
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
        _saveManager = SaveManager.instance;
        goldText.text = _saveManager.data.gold.ToString();
    }

    public void OnEnable()
    {
        if (_saveManager)
            goldText.text = _saveManager.data.gold.ToString();
    }

    public void playPressed()
    {
        // load the map with the correct color but for now i am just going to load the first map as this feature needs some development yet
        Invoke("dontAsk", 2);
        LoadingScreen.SetActive(true);
    }

    public void dontAsk()
    {
        // this is mearly here so the loading screen can be seen
        SceneManager.LoadSceneAsync(1);
    }

    public void mapPressed(int ID)
    {
        mapObjects[_previousMapPressed].color = mapColors[_previousMapPressed];
        _previousMapPressed = ID;
        mapObjects[ID].color = Color.white;
    }
}
