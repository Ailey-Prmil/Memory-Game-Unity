using UnityEngine;
using UnityEngine.UI;

public class SettingPanelExitButtonScript : MonoBehaviour
{
    public Button settingButton;
    public Button exitButton; // Reference to the Exit button
    public CanvasGroup mainCanvas;

    void Start()
    {
        // Attach the ExitCanvas method to the button's onClick event
        exitButton.onClick.AddListener(ExitCanvas);
        settingButton.onClick.AddListener(OpenCanvas);
        gameObject.SetActive(false);
    }

    // Method to deactivate the Settings Canvas
    void ExitCanvas()
    {
        mainCanvas.blocksRaycasts = true;
        mainCanvas.interactable = true;
        gameObject.SetActive(false);
    }

    void OpenCanvas()
    {
        gameObject.SetActive(true);
        mainCanvas.blocksRaycasts = false;
        mainCanvas.interactable = false;
    }
}