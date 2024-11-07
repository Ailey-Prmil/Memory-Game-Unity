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
            GameManager.Instance.OnGameResume();

        }

        protected void OpenCanvas()
        {
            Debug.Log("OpenCanvas");
            if (GameManager.Instance.IsGameRunning)
            {
                gameObject.SetActive(true);
                mainCanvasGroup.blocksRaycasts = false;
                mainCanvasGroup.interactable = false;
                GameManager.Instance.OnGamePause();
            }
        }
    }
}