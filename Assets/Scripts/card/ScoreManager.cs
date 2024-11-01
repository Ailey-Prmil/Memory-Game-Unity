using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int currentScore;
    private int currentComboAmount;
    private int currentTurn;
    public int playtime;
    private int seconds;
    private int minutes;
    [Header("Text Connections")]
    public Text timeText;
    public Text scoreText;
    public Text comboText;
    public Text turnText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreText();
        StartCoroutine("Playtime");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sử dụng đúng tên hàm
    }

    public void AddScore(int scoreAmount)
    {
        currentComboAmount++;
        currentTurn++;
        currentScore += scoreAmount * currentComboAmount;
        UpdateScoreText();
    }

    public void ResetCombo()
    {
        currentComboAmount = 0;
        currentTurn++;
        UpdateScoreText();
    }

    public void UpdateCurrentTurn()
    {
        currentTurn++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString("N");
        comboText.text = "Combo: " + currentComboAmount;
        turnText.text = "Turn: " + currentTurn.ToString(); // Chuyển thành chuỗi
    }

    IEnumerator Playtime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playtime++;
            seconds = (playtime % 60);
            minutes = (playtime / 60) % 60;
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        timeText.text = "Time: " + minutes.ToString("D2") + ":" + seconds.ToString("D2"); // Định dạng thời gian
    }

    public void StopTime()
    {
        StopCoroutine("Playtime"); // Dừng đúng coroutine
    }
}
