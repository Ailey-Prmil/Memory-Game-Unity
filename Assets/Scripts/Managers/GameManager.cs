using System.Collections.Generic;
using Assets.Scripts.Animations;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
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
        public ParticleSystem sparkleEffect;


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

            progressTrack = FindObjectOfType<ProgressTrack>();


            streak = Streak.CreateInstance();

            cardGrid = FindObjectOfType<CardGrid>();
        }

        void Start()
        {
            OnGameStart();
            cardGrid.EventManager.AddObserver(this); // Be an observer to CardGrid
        }

        public void OnGameStart()
        {
            IsGameRunning = false;
            progressTrack.ResetProgress(CardGrid.SelectedDimension);
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
            GameResult result = new GameResult(System.DateTime.Now, score.GetScores(), streak.GetMaxStreakCount());
            ResultDataManager.Instance.AddResultData(result, CardGrid.SelectedDimension);
            ResultPanel.ShowResultPanel(isWin, score.GetScores(), streak.GetMaxStreakCount());
        }

        public void OnMatchedPair()
        {
            MemeManager.Instance.ShowRandomWinMeme();
            score.IncrementScore();
            progressTrack.IncrementProgress();
            streak.IncrementStreak();
        }
        public void OnMismatchedPair()
        {
            MemeManager.Instance.ShowRandomFailMeme();
            score.ChangeUnitScore();
            streak.ResetStreak();
        }

        public void OnNotify(MonoBehaviour publisher, object eventType)
        {
            if (eventType is GridEventType.CardMatched) OnMatchedPair();
            else if (eventType is GridEventType.CardFailed) OnMismatchedPair();

            int streakCount = streak.GetStreakCount();
            if (streakCount % 2 == 0 && streakCount > 0)
            {
                SoundManager.Instance.PlaySound("combo", 1f);
                PopUpText.ShowText($"Combo {streakCount}");
                score.AddBonus(streakCount);

                sparkleEffect.Play();
            }

            if (eventType is GridEventType.AllCardsMatched)
            {
                OnGameOver(true);
            }
        }
    }
}
