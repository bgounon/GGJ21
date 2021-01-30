using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{
    private GameManager manager;
    private TMPro.TextMeshProUGUI uiButton;
    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        uiButton = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            uiButton.text = "STOP";
        }
        else
        {
            uiButton.text = "GO";
        }
    }

    public void onClick()
    {
        isPlaying = !isPlaying;
        manager.onEnterPressed();
    }
}
