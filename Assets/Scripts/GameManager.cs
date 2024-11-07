using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour, IGridObserver
{
    public List<Sprite> SpriteCollection;
    public static GameManager Instance { get; private set; }
    private Score score;
    private CardGrid cardGrid;
    private ProgressTrack progressTrack;
    private Streak streak;
    public PopUpTextAnimation PopUpText;

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

        SpriteCollection = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/CardSprites"));

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
        cardGrid.EventManager.AddObserver(this); // Subscribe to the event
        progressTrack.SetMaxProgress(cardGrid.Dimension);
    }

    public void OnMatchedPair()
    {
        score.IncrementScore();
        progressTrack.IncrementProgress();
        streak.IncrementStreak();

    }
    public void OnMismatchedPair()
    {
        score.ChangeUnitScore();
        streak.ResetStreak();
        
    }

    public void OnNotify(MonoBehaviour publisher, object eventType)
    {
        if (eventType is GridEventType.CardMatched) OnMatchedPair();
        else if (eventType is GridEventType.CardFailed) OnMismatchedPair();
        int streakCount = streak.GetStreakCount();
        Debug.Log("Streak: " + streakCount);
        if (streakCount % 2 == 0 && streakCount > 0)
        {
            PopUpText.ShowText($"Combo {streakCount}");
        }
    }
}
