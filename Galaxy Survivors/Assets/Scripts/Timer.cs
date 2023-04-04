using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float time;
    public float min;
    public float sec;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale += 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale -= 0.5f;
        }


        time = Time.time;
        min = TimeSpan.FromSeconds(time).Minutes;
        sec = TimeSpan.FromSeconds(time).Seconds;
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
