using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SettingManager : OptionPanel
    {
        public static SettingManager Instance { get; private set; }
        public Button musicButton;      
        public Button soundButton;      
        public Image musicIcon;
        public Image soundIcon;         
        public Sprite musicOnIcon;      
        public Sprite musicOffIcon;     
        public Sprite soundOnIcon;      
        public Sprite soundOffIcon;     

        private bool isMusicOn = true;  
        private bool isSoundOn = true;

        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                base.Awake();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected override void Start()
        {
            musicButton.onClick.AddListener(ToggleMusic);
            soundButton.onClick.AddListener(ToggleSound);
            UpdateIcons();
            base.Start();

        }

        void ToggleMusic()
        {
            isMusicOn = !isMusicOn;
            if (isMusicOn)
            {
                SoundManager.Instance.PlayBackgroundMusic();
            }
            else
            {
                SoundManager.Instance.StopBackgroundMusic();
            }

            musicIcon.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
        }

        void ToggleSound()
        {
            isSoundOn = !isSoundOn;
            if (!isSoundOn)
            {
                SoundManager.Instance.DisableSound();
            }
            else
            {
                SoundManager.Instance.EnableSound();
            }

            soundIcon.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
        }

        void UpdateIcons()
        {
            musicIcon.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
            soundIcon.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
        }
    }
}
