using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class OptionPanel : MonoBehaviour
    {
        public Button triggerButton;
        public Button exitButton; // Reference to the Exit button
        public GameObject mainCanvas;
        public CanvasGroup mainCanvasGroup;
        private bool isPanelEnabled;

        protected virtual void Awake()
        {
            mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
            exitButton.onClick.AddListener(ExitCanvas);
            triggerButton.onClick.AddListener(OpenCanvas);
            gameObject.SetActive(false);
        }

        protected void ExitCanvas()
        {
            gameObject.SetActive(false);
            mainCanvasGroup.blocksRaycasts = true;
            mainCanvasGroup.interactable = true;
            if (GameManager.Instance != null) GameManager.Instance.OnGameResume(); // Only call OnGameResume if GameManager exists

        }

        protected void OpenCanvas()
        {
            if (GameManager.Instance == null) isPanelEnabled = true;
            else isPanelEnabled = GameManager.Instance.IsGameRunning; // Panel is disabled when game is not running (i.e. game is paused)
            if (isPanelEnabled)
            {
                gameObject.SetActive(true);
                mainCanvasGroup.blocksRaycasts = false;
                mainCanvasGroup.interactable = false;
                if (GameManager.Instance != null) GameManager.Instance.OnGamePause(); // Only call OnGamePause if GameManager exists
            }
        }
    }
}