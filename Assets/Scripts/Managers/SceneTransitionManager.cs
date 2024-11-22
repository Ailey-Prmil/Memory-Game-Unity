using System.Collections;
using Assets.Scripts.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance { get; private set; }
        public CloudTransition CloudTransition;
        public Button PlayButton;

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
        }
    }
}

