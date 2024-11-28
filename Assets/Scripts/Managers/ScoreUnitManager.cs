using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreUnitManager
    {
        private static ScoreUnitManager instance;

        public int ScoreUnit
        {
            get;
            private set;
        }
        private int wrongMoves;

        ScoreUnitManager()
        {
            ResetScore();
        }

        public static ScoreUnitManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoreUnitManager();
                }

                return instance;
            }
        }
        public void ResetScore()
        {
            ScoreUnit = 20;
            wrongMoves = 0;
        }
        public void IncrementWrongMoves()
        {
            wrongMoves++;
            if (wrongMoves > 5 & wrongMoves < 15) ScoreUnit--;
        }

    }
}
