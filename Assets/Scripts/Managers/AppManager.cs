using System.Collections;
using Assets.Scripts.Data;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class AppManager : MonoBehaviour
    // App manager manages the transitions between scenes (game menu and main game) and whether to quit the application
    {
        public static AppManager Instance { get; private set; }
        public CloudTransition CloudTransition;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else 
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "GameMenuScene")
                {
                    ResultDataManager.Instance.SaveResultData(); // Save data before quitting
                    Application.Quit();
                }
                else
                {
                    PauseSettingPanel.Instance.OpenCanvas();
                }
            }
        }
        public void StartGame()
        {
            LoadScene("MainGameScene");
        }

        public void ExitGame()
        {
            LoadScene("GameMenuScene");
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneTransition(sceneName));
        }

        private IEnumerator LoadSceneTransition(string sceneName)
        {
            yield return StartCoroutine(CloudTransition.CloudInTransition());
            SceneManager.LoadScene(sceneName);
            yield return StartCoroutine(CloudTransition.CloudOutTransition());
            SettingManager.Instance.mainCanvasGroup = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<CanvasGroup>();
        }
    }
}

