using UnityEngine;

namespace Assets.Scripts
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

        void ResetProgress()
        {
            progress = 0;
            ProgressBarAnimation.SetProgress(progress);
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
}
