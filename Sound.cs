using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound Instance;
    public AudioClip hole, boat, death, step, drown, finish, obstacleHit, button, delete, enemy, coin;
    public float soundVol = 1;
    private AudioSource audioSource;

    void Awake()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialize = false;
        audioSource.volume = soundVol;

        if (Instance != null)
        {
            Debug.LogError("Multiple instances of Sound!");
        }
        Instance = this;
    }

    public void holeSound()
    {
        MakeSound(hole);
    }
    public void boatSound()
    {
        MakeSound(boat);
    }
    public void deathSound()
    {
        MakeSound(death);
    }
    public void stepSound()
    {
        MakeSound(step);
    }
    public void drownSound()
    {
        MakeSound(drown);
    }
    public void finishSound()
    {
        MakeSound(finish);
    }
    public void obstacleHitSound()
    {
        MakeSound(obstacleHit);
    }
    public void buttonSound()
    {
        MakeSound(button);
    }
    public void deleteSound()
    {
        MakeSound(delete);
    }
    public void enemySound()
    {
        MakeSound(enemy);
    }
    public void coinSound()
    {
        MakeSound(coin);
    }


    private void MakeSound(AudioClip originalClip)
    {
        audioSource.PlayOneShot(originalClip);
    }


}