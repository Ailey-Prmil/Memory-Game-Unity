namespace Assets.Scripts.Objects
{
    public class Streak
    {
        private int streakCount;
        private int maxStreakCount;

        private Streak()
        {
            this.streakCount = 0;
            this.maxStreakCount = 0;
        }

        public static Streak CreateInstance()
        {
            return new Streak();
        }

        public void IncrementStreak()
        {
            streakCount++;
            if (streakCount > maxStreakCount)
            {
                maxStreakCount = streakCount;
            }
        }

        public void ResetStreak()
        {
            streakCount = 0;
        }
        public void ResetMaxStreak()
        {
            maxStreakCount = 0;
        }

        public int GetStreakCount()
        {
            return streakCount;
        }

        public int GetMaxStreakCount()
        {
            return maxStreakCount;
        }
    }
}