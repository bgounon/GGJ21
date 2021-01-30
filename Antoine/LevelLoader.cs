using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadSceneCreditsFonction()
    {
        StartCoroutine(LoadCredits());
    }
    IEnumerator LoadCredits()
    {
        //play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene("Credits");
    }

    public void LoadSceneGameFonction()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        //play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene("Game");
    }

    public void LoadSceneMenuFonction()
    {
        print("JAI CLIQUE PT1");
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu()
    {
        //play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        print("quit game!");
        Application.Quit();
    }
}
