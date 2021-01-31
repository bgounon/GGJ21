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
    }

    public IEnumerator displayWinPannel(int score)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
        winText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()
            .FirstOrDefault(item => item.name == "WinText");
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
