using Assets.Scripts.Managers;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PauseSettingPanel : OptionPanel
    {
        public Button RestartButton;
        public Button QuitButton;

        protected override void Awake()
        {
            RestartButton.onClick.AddListener(RestartGame);
            QuitButton.onClick.AddListener(QuitGame);
            base.Awake();
        }

        void Start()
        {
            gameObject.SetActive(false);
        }

        void RestartGame()
        {
            ExitCanvas();
            GameManager.Instance.OnGameStart();
        }

        void QuitGame()
        {
            gameObject.SetActive(false);
            GameManager.Instance.OnGameOver(false);
        }
    }
}
