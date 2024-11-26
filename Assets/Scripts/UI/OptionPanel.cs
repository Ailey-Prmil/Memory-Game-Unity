using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class OptionPanel : MonoBehaviour
    {
        public Button exitButton; // Reference to the Exit button
        public Button triggerButton;
        public CanvasGroup mainCanvasGroup;
        public GameObject mainPanel;
        private bool isPanelEnabled;

        protected virtual void Awake()
        {
            if (exitButton != null) exitButton.onClick.AddListener(ExitCanvas);
            if (triggerButton != null) triggerButton.onClick.AddListener(OpenCanvas);
        }

        protected virtual void Start()
        {
            mainPanel.SetActive(false);
        }

        protected void ExitCanvas()
        {
            mainPanel.SetActive(false);
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
                mainPanel.SetActive(true);
                mainCanvasGroup.blocksRaycasts = false;
                mainCanvasGroup.interactable = false;
                if (GameManager.Instance != null) GameManager.Instance.OnGamePause(); // Only call OnGamePause if GameManager exists
            }
        }
    }
}