using UnityEngine;

namespace Assets.Scripts
{
    public class Score : MonoBehaviour
    {
        private ScoreAnimation scoreAnimation;
        private int Scores;
        private ScoreUnitManager scoreUnitManager;

        // Start is called before the first frame update
        void Start()
        {
            scoreUnitManager = ScoreUnitManager.Instance;
        
            Scores = 0;
        }

        public void ChangeUnitScore()
        {
            scoreUnitManager.IncrementWrongMoves();
        }
    
        public void IncrementScore()
        {
            Scores += scoreUnitManager.ScoreUnit;
            scoreAnimation.SetScore(Scores);
            Debug.Log("Score: " + Scores);
        }

        public void ResetScore()
        {
            Scores = 0;
            if (scoreAnimation == null)
            {
                scoreAnimation = GetComponent<ScoreAnimation>();
            }

            scoreAnimation.SetScore(Scores);
        }


    }
}
