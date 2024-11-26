using System;

namespace Assets.Scripts.Data
{
    [System.Serializable]
    public class GameResult
    {
        public int Id;
        public DateTime DateTime;
        public int Score;
        public int Streak;

        public GameResult(DateTime dateTime, int score, int streak)
        {
            DateTime = dateTime;
            Score = score;
            Streak = streak;
        }
    }
}
