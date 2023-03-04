using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public bool DELETE_ME_I_AM_FOR_TESTING;
    public static bool statsChanged;

    public GameStats stats;

    [Header("XP/Level")]
    public Slider xpSlider;
    public TMP_Text levelText;

    [Header("Coins")]
    public TMP_Text coinsText;

    [Header("Kills")]
    public TMP_Text killsText;

    public void Start()
    {
        // XP/Level
        xpSlider.minValue = (float)(Math.Pow(stats.level, 2) * 100);
        xpSlider.maxValue = (float)(Math.Pow(stats.level + 1, 2) * 100);
        xpSlider.value = stats.XP;
        levelText.text = "Level: " + stats.level;

        // Coins
        coinsText.text = stats.coins + " $";

        // Kills
        killsText.text = "& " + stats.kills;
    }

    public void Update()
    {
        if (statsChanged || DELETE_ME_I_AM_FOR_TESTING) 
        {
            statsChanged = false;
            DELETE_ME_I_AM_FOR_TESTING = false;

            // XP/Level
            if (stats.XP > (float)(Math.Pow(stats.level + 1, 2) * 100))
                stats.level++;
            xpSlider.minValue = (float)(Math.Pow(stats.level, 2) * 100);
            xpSlider.maxValue = (float)(Math.Pow(stats.level + 1, 2) * 100);
            xpSlider.value = stats.XP;
            levelText.text = "Level: " + stats.level;

            // Coins
            coinsText.text = stats.coins + " $";

            // Kills
            killsText.text = stats.kills + " Kills";
        }
    }
}
