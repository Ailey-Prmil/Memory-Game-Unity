using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    public AudioSettingController Instance { get; private set; }
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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       
        musicButton.onClick.AddListener(ToggleMusic);
        soundButton.onClick.AddListener(ToggleSound);

        UpdateIcons();
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
