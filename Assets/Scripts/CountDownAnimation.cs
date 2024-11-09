using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CountDownAnimation : MonoBehaviour
{
    public PopUpTextAnimation PopUpTextAnimation;
    // Start is called before the first frame update
    void Awake()
    {
        PopUpTextAnimation = GetComponent<PopUpTextAnimation>();
    }

    public void StartCountdown(int countDownTime)
    {
        StartCoroutine(CountdownCoroutine(countDownTime));
    }

    private IEnumerator CountdownCoroutine(int countDownTime)
    {
        PopUpTextAnimation.ShowText("START");
        yield return new WaitForSeconds(1f);
        float currentTime = countDownTime;

        while (currentTime > 0)
        {
            PopUpTextAnimation.ShowText(currentTime.ToString("F0"));
            yield return new WaitForSeconds(1.0f); // Wait for one second
            currentTime -= 1.0f; // Decrease the time by one second
        }
    }

}

