using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text scoreText;
    public TMP_Text streakText;

    public Button RestartButton;
    public Button QuitButton;

    void Start()
    {
        RestartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.OnGameStart();
            gameObject.SetActive(false);
        });
        QuitButton.onClick.AddListener(() =>
        {
            SceneTransitionManager.Instance.ExitGame();
        });
        gameObject.SetActive(false);
    }

    public void ShowResultPanel(bool isWin, int score, int streak)
    {
        resultText.text = isWin ? "SUCCESS" : "FAILED";
        scoreText.text = score.ToString();
        streakText.text = streak.ToString();
        gameObject.SetActive(true);
    }
}
