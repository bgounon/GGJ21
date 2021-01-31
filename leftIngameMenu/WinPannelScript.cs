using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinPannelScript : MonoBehaviour
{
    
    private GameManager gameManager;

    private TMPro.TextMeshProUGUI winText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        winText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()
            .FirstOrDefault(item => item.name == "WinText");
    }

    public void displayWinPannel(int score)
    {
        gameObject.SetActive(true);
        winText.text = $"You Win\nScore : {score}";
    }

    public void hideDisplayPannel()
    {
        gameObject.SetActive(false);
    }

    public void onMenuPress()
    {
        //load level selection
        gameManager.finishGame();
    }
}
