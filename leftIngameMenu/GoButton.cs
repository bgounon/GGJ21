using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{
    private GameManager manager;
    public Image imageGo;
    public Image imageNoGo;
    private bool isPlaying = false;

    private Image imageToChange;
    // Start is called before the first frame update
    void Start()
    {
        var images = GetComponentsInChildren<Image>();
        imageToChange = images.FirstOrDefault(item => item.name == "ImageButton");
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.getState() == GameState.CONSTRUCTION)
        {
            imageGo.gameObject.SetActive(true);
            imageNoGo.gameObject.SetActive(false);
        }
        else
        {
            imageGo.gameObject.SetActive(false);
            imageNoGo.gameObject.SetActive(true);
        }
    }

    public void reset()
    {
        isPlaying = false;
    }

    public void onClick()
    {
        if (manager.canStart())
        {
            isPlaying = !isPlaying;
            manager.onEnterPressed();
        } else
        {
            print("You have to put your item down or delete it");
        }
    }
}
