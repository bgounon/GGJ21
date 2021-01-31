using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public static Music Instance;
    public AudioClip clip;
    public float musicVol;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialize = false;
        audioSource.volume = musicVol;
        audioSource.loop = true;
        audioSource.clip = clip;

        if (Instance != null)
        {
            Debug.LogError("Multiple instances of MusicController!");
        }
        Instance = this;

        audioSource.Play();

    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

}
