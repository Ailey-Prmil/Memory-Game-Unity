using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Button settingButton;
    public Button exitButton; // Reference to the Exit button
    public GameObject mainCanvas;
    public CanvasGroup mainCanvasGroup;

    void Awake()
    {
        mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
    }
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
        gameObject.SetActive(false);
        mainCanvasGroup.blocksRaycasts = true;
        mainCanvasGroup.interactable = true;
        
    }

    void OpenCanvas()
    {
        gameObject.SetActive(true);
        mainCanvasGroup.blocksRaycasts = false;
        mainCanvasGroup.interactable = false;
    }
}