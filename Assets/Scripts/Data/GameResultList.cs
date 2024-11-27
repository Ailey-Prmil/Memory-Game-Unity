using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    [System.Serializable]
    public class GameResultList
    {
        public List<GameResult> Results4x4;
        public List<GameResult> Results6x6;
        public List<GameResult> Results8x8;
    }
}