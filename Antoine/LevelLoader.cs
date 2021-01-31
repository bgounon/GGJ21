using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Debug = UnityEngine.Debug;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private LevelFinishedSerialization levelFinished;
    private string serializedFiledName = "levelFinished.lol";

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream($"{Application.persistentDataPath}/{serializedFiledName}", FileMode.Open,
                FileAccess.Read);

            levelFinished = (LevelFinishedSerialization) formatter.Deserialize(stream);
        }
        catch (Exception e)
        {
            print($"Exception : {e}");
            levelFinished = new LevelFinishedSerialization()
            {
                levelFinishedList = Array.Empty<LevelFinishedDetails>()
            };
        }

        var levelBinders = FindObjectsOfType<LevelBinder>();
        foreach (var levelBinder in levelBinders)
        {
            levelBinder.updateUiForFinishedLevels(levelFinished);
        }
        
        Debug.Log("LOADED");

    }

    public void LoadSceneCreditsFonction()
    {
        StartCoroutine(LoadScene("Credits"));
    }
    
    public void LoadSceneLevelSelectFonction()
    {
        StartCoroutine(LoadScene("LevelSelection"));
    }

    public void SaveAndBackToLevelSelection(int score)
    {
        updateLevelWin(SceneManager.GetActiveScene().name, score);
        LoadSceneLevelSelectFonction();
    }
    
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    public void LoadSceneMenuFonction()
    {
        StartCoroutine(LoadScene("Menu"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        //play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        print("quit game!");
        Application.Quit();
    }

    public void updateLevelWin(string levelName, int score)
    {
        LevelFinishedDetails alreadyFinishedLevel =
            levelFinished.levelFinishedList.FirstOrDefault(item => item.levelName.Equals(levelName));
        if (alreadyFinishedLevel.levelName == null)
        {
            alreadyFinishedLevel.score = score;
            alreadyFinishedLevel.levelName = levelName;
            levelFinished.levelFinishedList = levelFinished.levelFinishedList.Append(alreadyFinishedLevel).ToArray();
        }
        else
        {
            var tmpList = levelFinished.levelFinishedList.ToList();
            tmpList.Remove(alreadyFinishedLevel);
            if (score > alreadyFinishedLevel.score)
            {
                alreadyFinishedLevel.score = score;
            }
            tmpList.Add(alreadyFinishedLevel);
            levelFinished.levelFinishedList = tmpList.ToArray();
        }

        updateSerializedFile();

    }

    private void updateSerializedFile()
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream($"{Application.persistentDataPath}/{serializedFiledName}", FileMode.Create,
                FileAccess.Write);

            formatter.Serialize(stream, levelFinished);
        }
        catch (Exception e)
        {
            print($"Error impossible to serialize data {e}");
        }
    }
}
