using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScreenFeedback : MonoBehaviour
{
    private string displayFormat = "{0}\nScore : {1}";
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void updateDisplay(string step, int score)
    {
        text.text = string.Format(displayFormat, step, score);
    }
}
