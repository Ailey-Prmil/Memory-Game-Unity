using UnityEngine;

namespace Assets.Scripts
{
    public class Score : MonoBehaviour
    {
        private ScoreAnimation scoreAnimation;
        private int scores;
        private ScoreUnitManager scoreUnitManager;

        // Start is called before the first frame update
        void Start()
        {
            scoreUnitManager = ScoreUnitManager.Instance;
        
            scores = 0;
        }

        public void ChangeUnitScore()
        {
            scoreUnitManager.IncrementWrongMoves();
        }
    
        public void IncrementScore()
        {
            scores += scoreUnitManager.ScoreUnit;
            scoreAnimation.SetScore(scores);
        }

        public void AddBonus(int combo)
        {
            scores += combo * scoreUnitManager.ScoreUnit;
            scoreAnimation.SetScore(scores);
        }

        public void ResetScore()
        {
            scores = 0;
            if (scoreAnimation == null)
            {
                scoreAnimation = GetComponent<ScoreAnimation>();
            }

            scoreAnimation.SetScore(scores);
        }

        public int GetScores()
        {
            return scores;
        }


    }
}
