using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float time;

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        float min = TimeSpan.FromSeconds(time).Minutes;
        float sec = TimeSpan.FromSeconds(time).Seconds;
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
