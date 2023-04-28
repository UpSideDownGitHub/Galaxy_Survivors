#define TESTING
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // variables
    public TMP_Text timerText;
    public float startTime;
    public float time;
    public float min;
    public float sec;

    // Called before the first update frame
    public void Start()
    {
        startTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        // used to control the time of the game, for testing end features
#if TESTING
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale += 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale -= 0.5f;
        }
#endif


        // calculates the time beased upon the current time the game has been running for
        time = Time.time - startTime;
        min = TimeSpan.FromSeconds(time).Minutes;
        sec = TimeSpan.FromSeconds(time).Seconds;
        // sets the time to be in the correct format for the game
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
