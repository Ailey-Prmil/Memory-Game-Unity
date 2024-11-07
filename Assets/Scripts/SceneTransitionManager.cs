using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public CloudTransition CloudTransition;
        public Scene NextScene;
        public Button StartButton;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
            StartButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            LoadScene("MainGameScene");
        }

        public void ExitGame()
        {
            Application.Quit();
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
        }
    }
}

