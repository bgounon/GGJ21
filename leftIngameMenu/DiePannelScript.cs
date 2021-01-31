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
    }

    public IEnumerator displayDiePannel(string dieCause)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
        dieText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()
            .FirstOrDefault(item => item.name == "DieText");
        dieText.text = $"You loose : {dieCause}";
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
