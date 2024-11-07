using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreAnimation : MonoBehaviour
{
    public TMP_Text scoreText;
    public float transitionDuration = 1.5f; // Duration of the number transition

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void SetScore(int newScore)
    {
        StartCoroutine(ScoreTransition(newScore));
    }

    private IEnumerator ScoreTransition(int newScore)
    {
        int currentNumber = int.Parse(scoreText.text);
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            int displayNumber = Mathf.RoundToInt(Mathf.Lerp(currentNumber, newScore, t));
            scoreText.text = displayNumber.ToString();
            yield return null;
        }

        scoreText.text = newScore.ToString();
    }
}