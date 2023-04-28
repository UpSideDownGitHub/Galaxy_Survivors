using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    // public variables
    public Gradient color;
    public TMP_Text text;
    public float increaseRate;

    // private variables
    float _val;
    bool increaseing = true;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // if the values are increasing 
        if (increaseing)
        {
            // increase the value until reaches the max
            _val += increaseRate;
            if (_val >= 1)
                increaseing = false;
        }
        else
        {
            // deacrease the number untill min reached
            _val -= increaseRate;
            if (_val <= 0)
                increaseing = false;
        }
    }

    // called after all updates have been run
    private void LateUpdate()
    {
        // set the color of the text to the current value
        text.color = color.Evaluate(_val);
    }
}
