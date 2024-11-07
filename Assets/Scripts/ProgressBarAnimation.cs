using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarAnimation : MonoBehaviour
{
    public Slider progressBar;
    public float transitionDuration = 0.5f; // Duration of the transition
    void Awake()
    {
        progressBar = GetComponent<Slider>();
    }
    void Start()
    {
        progressBar.value = 0;
        progressBar.minValue = 0;
    }

    public void SetProgress(float newStreak)
    {
        StartCoroutine(SmoothTransition(newStreak));
    }
    public void SetMaxStreak(float maxStreak)
    {
        progressBar.maxValue = maxStreak;
    }

    private IEnumerator SmoothTransition(float newValue)
    {
        float startValue = progressBar.value;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Lerp(startValue, newValue, elapsedTime / transitionDuration);
            yield return null;
        }

        progressBar.value = newValue;
    }
}