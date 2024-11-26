using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PauseSettingPanel : OptionPanel
    {
        public static PauseSettingPanel Instance { get; private set; }
        public Button RestartButton;
        public Button QuitButton;
        public GameObject MessagePanelPrefab;
        private MessagePanel messagePanel;

        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                RestartButton.onClick.AddListener(()=> ShowConfirmPanel(FunctionType.Restart));
                QuitButton.onClick.AddListener(()=> ShowConfirmPanel(FunctionType.Quit));
                base.Awake();
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected override void Start()
        {
            ExitCanvas();
        }

        public void RestartGame()
        {
            ExitCanvas();
            GameManager.Instance.OnGameStart();
        }

        public void QuitGame()
        {
            ExitCanvas();
            GameManager.Instance.OnGameOver(false);
        }

        public new void OpenCanvas()
        {
            base.OpenCanvas();
        }

        private void ShowConfirmPanel(FunctionType function)
        {
            messagePanel = GameObject.Instantiate(MessagePanelPrefab, GetComponent<RectTransform>()).GetComponent<MessagePanel>();
            messagePanel.Function = function;
            messagePanel.name = "ConfirmMessagePanel";
            mainPanel.SetActive(false);
        }
    }
}
