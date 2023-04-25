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
        SceneManager.LoadSceneAsync(1);
    }

    public void mapPressed(int ID)
    {
        mapObjects[_previousMapPressed].color = mapColors[_previousMapPressed];
        _previousMapPressed = ID;
        mapObjects[ID].color = Color.white;
    }
}
