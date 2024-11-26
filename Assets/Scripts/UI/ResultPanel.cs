using System.Collections.Generic;
using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ResultPanel : MonoBehaviour
    {
        public TMP_Text resultText;
        public TMP_Text scoreText;
        public TMP_Text streakText;
        public GameObject mainCanvas;
        public CanvasGroup mainCanvasGroup;
        public List<Sprite> FailStatusIconSprites;
        public List<Sprite> WinStatusIconSprites;
        public Button RestartButton;
        public Button QuitButton;
        public Image StatusIconImage;

        void Awake()
        {
            mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
        }
        void Start()
        {
            FailStatusIconSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/FailIcons"));
            WinStatusIconSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/WinIcons"));

            RestartButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                mainCanvasGroup.blocksRaycasts = true;
                mainCanvasGroup.interactable = true;
                GameManager.Instance.OnGameStart();

            });
            QuitButton.onClick.AddListener(() =>
            {
                AppManager.Instance.ExitGame();
            });
            StatusIconImage.GetComponent<Image>();
            gameObject.SetActive(false);
        }

        public void ShowResultPanel(bool isWin, int score, int streak)
        {
            int randomIndex = Random.Range(0, isWin ? WinStatusIconSprites.Count : FailStatusIconSprites.Count);
            StatusIconImage.sprite = isWin ? WinStatusIconSprites[randomIndex] : FailStatusIconSprites[randomIndex];
            resultText.text = isWin ? "SUCCESS" : "FAILED";
            if (isWin)
            {
                SoundManager.Instance.PlaySound("game-success", 1f);
            }
            else
            {
                SoundManager.Instance.PlaySound("game-failed", 1f);
            }
            scoreText.text = score.ToString();
            streakText.text = streak.ToString();
            gameObject.SetActive(true);

        }
    }
}
