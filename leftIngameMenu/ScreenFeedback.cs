using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScreenFeedback : MonoBehaviour
{
        private Text textStatus;
        private Text textScore;

    private void Start()
    {
        var components = GetComponentsInChildren<Text>();
        textStatus = components.FirstOrDefault(item => item.name == "Status");
        textScore = components.FirstOrDefault(item => item.name == "Score");
    }

    public void updateDisplay(string step)
    {
        if(textStatus != null) {
            textStatus.text = step;
        }
 
    }
    public void updateScore(int score)
    {
             if(textScore != null) {
                    textScore.text = $"Score : {score}";
        }

    }
}
