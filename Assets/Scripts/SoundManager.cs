using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource BackgroundMusic;
    private AudioSource audioSource;
    private List<AudioClip> audioClips;
    private float defaultVolume = 0.05f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            audioSource = GetComponent<AudioSource>();
            audioClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds"));
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddButtonClickSound();
    }
    void Start()
    {
        PlayBackgroundMusic();
        
    }

    public void PlayBackgroundMusic()
    {
        BackgroundMusic.Play();
    }
    public void AddButtonClickSound()
    {
        List<Button> buttons = new List<Button>(FindObjectsOfType<Button>());
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlaySound("button-clicked",1f));
        }
    }


    public void PlaySound(string audioName, float volume)
    {
        AudioClip audioClip = audioClips.Find(clip => clip.name == audioName);
        audioSource.volume = volume;
        audioSource.PlayOneShot(audioClip);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    void OnDestroy()
    { 
      // Unsubscribe from scene loaded event to avoid memory leaks
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

