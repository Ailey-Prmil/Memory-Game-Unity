using TMPro;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class PopUpTextAnimation : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        public float displayDuration; // Duration to show the text
        public float fadeOutDuration; // Duration of the fade-out effect
        public float fadeInDuration;

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
}