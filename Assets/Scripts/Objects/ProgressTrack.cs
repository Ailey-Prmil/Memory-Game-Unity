using Assets.Scripts.Animations;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ProgressTrack : MonoBehaviour
    {
        private int progress;
        private int maxProgress;


        public ProgressBarAnimation ProgressBarAnimation;

        void Awake()
        {
            ProgressBarAnimation = GetComponent<ProgressBarAnimation>();
        }

        public void ResetProgress(int dimension)
        {
            progress = 0;
            SetMaxProgress(dimension);
            ProgressBarAnimation.SetProgress(progress);
        }

        public void SetMaxProgress(int dimension)
        {
            maxProgress = dimension*dimension/2;
            ProgressBarAnimation.progressBar.maxValue = maxProgress;

        }
        public void IncrementProgress()
        {
            progress++;
            ProgressBarAnimation.SetProgress(progress);
        }
    
    }
}
