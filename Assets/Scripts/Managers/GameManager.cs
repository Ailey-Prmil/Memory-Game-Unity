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

        private Score _score;
        private CardGrid _cardGrid;
        private ProgressTrack _progressTrack;
        private Streak _streak;

        public PopUpTextAnimation PopUpText;
        public ResultPanel ResultPanel;
        public bool IsGameRunning;
        public ParticleSystem SparkleEffect;


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

            _score = FindObjectOfType<Score>();
            _progressTrack = FindObjectOfType<ProgressTrack>();
            _streak = Streak.CreateInstance();
            _cardGrid = FindObjectOfType<CardGrid>();
        }

        void Start()
        {
            OnGameStart();
            _cardGrid.GridEventManager.AddObserver(this); // Be an observer to CardGrid
        }

        public void OnGameStart()
        {
            IsGameRunning = false;

            // Reset all game objects
            _progressTrack.ResetProgress(CardGrid.SelectedDimension);
            _score.ResetScore();
            _streak.ResetStreak();
            _streak.ResetMaxStreak();
            _cardGrid.ResetGrid(() => { IsGameRunning = true;});
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

            GameResult result = new GameResult(System.DateTime.Now, _score.GetScores(), _streak.GetMaxStreakCount());
            ResultDataManager.Instance.AddResultData(result, CardGrid.SelectedDimension);
            ResultPanel.ShowResultPanel(isWin, _score.GetScores(), _streak.GetMaxStreakCount());
        }

        public void OnMatchedPair()
        {
            MemeManager.Instance.ShowRandomWinMeme();
            _score.IncrementScore();
            _progressTrack.IncrementProgress();
            _streak.IncrementStreak();
        }
        public void OnMismatchedPair()
        {
            MemeManager.Instance.ShowRandomFailMeme();
            _score.ChangeUnitScore();
            _streak.ResetStreak();
        }

        public void OnNotify(MonoBehaviour publisher, object eventType)
        {
            if (eventType is GridEventType.CardMatched) OnMatchedPair();
            else if (eventType is GridEventType.CardFailed) OnMismatchedPair();

            int streakCount = _streak.GetStreakCount();
            if (streakCount > 1) _score.AddBonus(streakCount);
            if (streakCount % 2 == 0 && streakCount > 0)
            {
                SoundManager.Instance.PlayComboSound();
                PopUpText.ShowText($"Combo {streakCount}");
                SparkleEffect.Play();
            }

            if (eventType is GridEventType.AllCardsMatched)
            {
                OnGameOver(true);
            }
        }
    }
}
