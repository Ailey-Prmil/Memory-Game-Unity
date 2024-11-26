using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        public AudioSource BackgroundMusic;
        private AudioSource audioSource;
        private List<AudioClip> audioClips;
        private bool soundEnabled;
        private bool musicEnabled;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                audioSource = GetComponent<AudioSource>();
                audioClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds"));
                SceneManager.sceneLoaded += OnSceneLoaded;
                soundEnabled = PlayerPrefs.GetInt("soundEnabled", 1) == 1;
                musicEnabled = PlayerPrefs.GetInt("musicEnabled", 1) == 1;


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
            if (musicEnabled) PlayBackgroundMusic();
        }

        public void PlayBackgroundMusic()
        {
            BackgroundMusic.Play();
            musicEnabled = true;
        }

        public void StopBackgroundMusic()
        {
            BackgroundMusic.Stop();
            musicEnabled = false;
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
            if (soundEnabled)
            {
                AudioClip audioClip = audioClips.Find(clip => clip.name == audioName);
                audioSource.volume = volume;
                audioSource.PlayOneShot(audioClip);
            }
        }

        public void DisableSound()
        {
            soundEnabled = false;
        }
        public void EnableSound()
        {
            soundEnabled = true;
        }

        void OnDestroy()
        { 
            // Unsubscribe from scene loaded event to avoid memory leaks
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PlayerPrefs.SetInt("soundEnabled", soundEnabled ? 1 : 0);
            PlayerPrefs.SetInt("musicEnabled", musicEnabled ? 1 : 0);
        }
    }
}

