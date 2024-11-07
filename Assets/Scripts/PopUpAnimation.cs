using UnityEngine;
using TMPro;

public class PopUpTextAnimation : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float displayDuration = 2.0f; // Duration to show the text
    public float fadeOutDuration = 1.0f; // Duration of the fade-out effect
    public float fadeInDuration = 1.0f;

    private void Start()
    {
        // Initially set the text component to be invisible
        textComponent.canvasRenderer.SetAlpha(0.0f);
    }

    public void ShowText(string message)
    {
        textComponent.text = message;
        textComponent.CrossFadeAlpha(1.0f, fadeInDuration, false); // Fade in
        Invoke("HideText", displayDuration);
    }

    private void HideText()
    {
        textComponent.CrossFadeAlpha(0.0f, fadeOutDuration, false); // Fade out
    }
}