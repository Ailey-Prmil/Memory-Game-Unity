using System;

namespace Assets.Scripts.Data
{
    [System.Serializable]
    public class GameResult
    {
        public int Id;
        public string Date;
        public int Score;
        public int Streak;

        public GameResult(DateTime dateTime, int score, int streak)
        {
            Date = dateTime.ToString("dd MMM yyyy");
            Score = score;
            Streak = streak;
        }
    }
}
