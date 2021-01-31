using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{

    public static AmbianceSound Instance;
    public AudioClip clip;
    public float ambianceVol;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialize = false;
        audioSource.volume = ambianceVol;
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