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

    public void Start()
    {
        stats.kills = 0;
        stats.coins = 0;
        stats.XP = 0;
        stats.level = 0;

        // XP/Level
        /*
         * Equation for Levels:
         * 
         *   n^1.5
         *  ------ x 10
         *     2  
        */
        xpSlider.minValue = (float)((Math.Pow(stats.level, 1.5)/2) * 10);
        xpSlider.maxValue = (float)((Math.Pow(stats.level + 1, 1.5)/2) * 10);
        xpSlider.value = stats.XP;
        levelText.text = "Level: " + stats.level;

        // Coins
        coinsText.text = stats.coins + " $";

        // Kills
        killsText.text = "& " + stats.kills;
    }

    public void Update()
    {
        if (statsChanged) 
        {
            statsChanged = false;

            // XP/Level
            if (stats.XP > (float)((Math.Pow(stats.level + 1, 1.5) / 2) * 10))
            { 
                stats.level++;
                upgradeSystem.levelUp();
            }
            xpSlider.minValue = (float)((Math.Pow(stats.level, 1.5) / 2) * 10);
            xpSlider.maxValue = (float)((Math.Pow(stats.level + 1, 1.5) / 2) * 10);
            xpSlider.value = stats.XP;
            levelText.text = "Level: " + stats.level;

            // Coins
            coinsText.text = stats.coins + " $";

            // Kills
            killsText.text = stats.kills + " Kills";
        }
    }
}
