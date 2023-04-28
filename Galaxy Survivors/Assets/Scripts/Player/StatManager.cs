using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static bool statsChanged;
    public GameStats stats;

    public Upgrade upgradeSystem;

    [Header("XP/Level")]
    public Slider xpSlider;
    public TMP_Text levelText;

    [Header("Coins")]
    public TMP_Text coinsText;

    [Header("Kills")]
    public TMP_Text killsText;

    [Header("Pasue Menu")]
    public GameObject pauseMenu;

    /*
     * used to pause the game 
    */ 
    public void pauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    // called before the first update frame
    public void Start()
    {
        // initilse the stats at the start of the new round
        stats.kills = 0;
        stats.coins = 0;
        stats.XP = 0;
        stats.level = 0;

        // XP/Level
        /*
         * Equation for Levels:
         * 
         *   n^1.9 x 10
         *      
        */
        xpSlider.minValue = (float)((Math.Pow(stats.level, 1.9)) * 10);
        xpSlider.maxValue = (float)((Math.Pow(stats.level + 1, 1.9)) * 10);
        xpSlider.value = stats.XP;
        levelText.text = "Level: " + stats.level;

        // Coins
        coinsText.text = stats.coins + " $";

        // Kills
        killsText.text = "& " + stats.kills;
    }

    // called once per frame
    public void Update()
    {
        // if there has been a change to the stats then update them
        if (statsChanged) 
        {
            // make it so that it is not running every frame
            statsChanged = false;

            // XP/Level
            if (stats.XP > (float)((Math.Pow(stats.level + 1, 1.9)) * 10))
            { 
                stats.level++;
                upgradeSystem.levelUp();
            }
            xpSlider.minValue = (float)((Math.Pow(stats.level, 1.9)) * 10);
            xpSlider.maxValue = (float)((Math.Pow(stats.level + 1, 1.9)) * 10);
            xpSlider.value = stats.XP;
            levelText.text = "Level: " + stats.level;

            // Coins
            coinsText.text = stats.coins + " $";

            // Kills
            killsText.text = stats.kills + " Kills";
        }
    }
}
