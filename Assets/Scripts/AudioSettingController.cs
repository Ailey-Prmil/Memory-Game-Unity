using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
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

    void Start()
    {
       
        musicButton.onClick.AddListener(ToggleMusic);
        soundButton.onClick.AddListener(ToggleSound);

        UpdateIcons();
    }

    void ToggleMusic()
    {
        
        isMusicOn = !isMusicOn;

        musicIcon.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        soundIcon.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }

    void UpdateIcons()
    {
        
        musicIcon.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
        soundIcon.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }
}
