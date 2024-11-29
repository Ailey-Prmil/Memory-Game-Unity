using System.Collections.Generic;
using Assets.Scripts.Interfaces;
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
        public bool soundEnabled;
        public bool musicEnabled;

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
            if (musicEnabled)
            {
                PlayBackgroundMusic();
            }
            else
            {
                StopBackgroundMusic();
            }
        }

        private void PlaySound(string audioName, float volume)
        {
            if (soundEnabled)
            {
                AudioClip audioClip = audioClips.Find(clip => clip.name == audioName);
                audioSource.volume = volume;
                audioSource.PlayOneShot(audioClip);
            }
        }

        private void AddButtonClickSound()
        {
            List<Button> buttons = new List<Button>(FindObjectsOfType<Button>());
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(() => PlaySound("button-clicked",1f));
            }
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

        public void PlayWinSound()
        {
            PlaySound("game-success", 1f);
        }

        public void PlayLoseSound()
        {
            PlaySound("game-failed", 1f);
        }

        public void PlayCardFlipSound()
        {
            PlaySound("card-flipped", 0.3f);
        }

        public void PlayCardMatchSound()
        {
            PlaySound("card-matched", 1f);
        }

        public void PlayComboSound()
        {
            PlaySound("combo", 1f);
        }

        public void PlayStartSound()
        {
            PlaySound("game-start", 1f);
        }

        public void PlayCounterSound()
        {
            PlaySound("counter", 1f);
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

