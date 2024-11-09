using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour, IGridObserver
    {
        public List<Sprite> SpriteCollection;
        public static GameManager Instance { get; private set; }
        private Score score;
        private CardGrid cardGrid;
        private ProgressTrack progressTrack;
        private Streak streak;
        public PopUpTextAnimation PopUpText;
        public ResultPanel ResultPanel;
        public bool IsGameRunning;

        void Awake()
        {
        
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            IsGameRunning = false;
            SpriteCollection = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/CardSprites"));
            Sprite backSprite = SpriteCollection.Find(sprite => sprite.name == "Cardback");
            SpriteCollection.Remove(backSprite);

            score = FindObjectOfType<Score>();
            if (score == null)
            {
                Debug.LogError("Score component not found in the scene!");
            }

            progressTrack = FindObjectOfType<ProgressTrack>();
            if (progressTrack == null)
            {
                Debug.LogError("ProgressTrack component not found in the scene!");
            }


            streak = Streak.CreateInstance();

            cardGrid = FindObjectOfType<CardGrid>();
            if (cardGrid == null)
            {
                Debug.LogError("CardGrid component not found in the scene!");
            }
        }

        void Start()
        {
            OnGameStart();
            cardGrid.EventManager.AddObserver(this); // Be an observer to CardGrid
        }

        public void OnGameStart()
        {
            IsGameRunning = false;
            progressTrack.ResetProgress(cardGrid.Dimension);
            score.ResetScore();
            streak.ResetStreak();
            cardGrid.ResetGrid(() => { IsGameRunning = true;});
            
        }

        public void OnGamePause()
        {
            IsGameRunning = false;
        }

        public void OnGameResume()
        {
            IsGameRunning = true;
        }

        public void OnGameOver(bool isWin)
        {
            IsGameRunning = false;
            ResultPanel.ShowResultPanel(isWin, score.GetScores(), streak.GetMaxStreakCount());
        }

        public void OnMatchedPair()
        {
            score.IncrementScore();
            progressTrack.IncrementProgress();
            streak.IncrementStreak();
            MemeManager.Instance.ShowRandomWinMeme();

        }
        public void OnMismatchedPair()
        {
            score.ChangeUnitScore();
            streak.ResetStreak();
            MemeManager.Instance.ShowRandomFailMeme();
        
        }

        public void OnNotify(MonoBehaviour publisher, object eventType)
        {
            if (eventType is GridEventType.CardMatched) OnMatchedPair();
            else if (eventType is GridEventType.CardFailed) OnMismatchedPair();

            int streakCount = streak.GetStreakCount();
            if (streakCount % 2 == 0 && streakCount > 0)
            {
                PopUpText.ShowText($"Combo {streakCount}");
            }

            if (eventType is GridEventType.AllCardsMatched)
            {
                OnGameOver(true);
            }
        }
    }
}
