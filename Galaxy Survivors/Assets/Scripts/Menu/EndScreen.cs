using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [Header("UI Objets")]
    public GameObject wellDoneText;
    public GameObject diedText;

    [Header("Text UI Obect")]
    public TMP_Text timeText;
    public TMP_Text killsText;
    public TMP_Text xpText;
    public TMP_Text coinsText;

    [Header("Links")]
    public Timer timer;
    public GameStats stats;
    private SaveManager _saveData;

    public bool notDied = false;

    // Start is called before the first frame update
    void Start()
    {
        _saveData = SaveManager.instance;
        Time.timeScale = 0f;
        // need to load all of the player data
        diedText.SetActive(false);
        wellDoneText.SetActive(false);
        if (notDied)
        { 
            wellDoneText.SetActive(true); 
            if (_saveData.data.currentMap + 1 < _saveData.data.mapsUnlocked.Length)
            {
                _saveData.data.mapsUnlocked[_saveData.data.currentMap + 1] = true;
            }
        }
        else
            diedText.SetActive(true);

        // TIME
        timeText.text = string.Format("{0:00}:{1:00}", timer.min, timer.sec);

        // STATS
        killsText.text = stats.kills.ToString();
        xpText.text = stats.XP.ToString();
        coinsText.text = stats.coins.ToString();
        _saveData.data.gold += stats.coins;
        
        
        _saveData.saveIntoJson();
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void mainMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

}
