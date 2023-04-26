using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public Gradient color;
    public TMP_Text text;

    float _val;
    public float increaseRate;
    bool increaseing = true;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (increaseing)
        {
            _val += increaseRate;
            if (_val >= 1)
                increaseing = false;
        }
        else
        {
            _val -= increaseRate;
            if (_val <= 0)
                increaseing = false;
        }
    }
    private void LateUpdate()
    {
        text.color = color.Evaluate(_val);
    }
}
