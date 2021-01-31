using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiePannelScript : MonoBehaviour
{
    private GameManager gameManager;

    private TMPro.TextMeshProUGUI dieText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dieText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()
            .FirstOrDefault(item => item.name == "TMPro.TextMeshProUGUI");
    }

    public void displayDiePannel(string dieCause)
    {
        gameObject.SetActive(true);
        dieText.text = $"You died by : {dieCause}";
    }

    public void hideDisplayPannel()
    {
        gameObject.SetActive(false);
    }

    public void onRetryPressed()
    {
        gameManager.onRetryPressed();
        hideDisplayPannel();
    }
}
