using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelBinder : MonoBehaviour, IPointerClickHandler
{
    public string sceneName;
    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        levelLoader.LoadLevel(sceneName);
    }

    public void updateUiForFinishedLevels(LevelFinishedSerialization state)
    {
        var currentLevel = state.levelFinishedList.FirstOrDefault(item => item.levelName == sceneName);
        var scoreText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()
            .FirstOrDefault(item => item.name == "score");
        var text = "";
        if (currentLevel.levelName != null)
        {
            var validatedImage = GetComponentsInChildren<Image>().FirstOrDefault(item => item.name == "Validated");
            if (validatedImage != null)
            {
                validatedImage.enabled = true;
            }

            text = $"High score\n{currentLevel.score}";
        }
        scoreText.text = text;
    }
}
