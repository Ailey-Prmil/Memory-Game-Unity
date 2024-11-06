using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTrack : MonoBehaviour
{
    private int progress;
    private int maxProgress;


    public ProgressBarAnimation ProgressBarAnimation;

    void Awake()
    {
        ProgressBarAnimation = GetComponent<ProgressBarAnimation>();
    }

    public void SetMaxProgress(int Dimension)
    {
        maxProgress = Dimension*Dimension/2;
        ProgressBarAnimation.progressBar.maxValue = maxProgress;

    }
    public void IncrementProgress()
    {
        progress++;
        ProgressBarAnimation.SetProgress(progress);
    }
    
}