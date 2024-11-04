using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public enum CardState
    {
        Hidden,
        Visible,
        Matched
    }

    [HideInInspector] public int id;
    private CardAnimation cardAnimation;
    private RectTransform parentRect;
    private SpriteRenderer spriteRenderer;
    private CardState state;

    public CardState State
    {
        get { return state; }
        private set
        {
            state = value;
            Debug.Log(State);
            NotifyObservers();
        }
    }
    private List<ICardObserver> observers = new List<ICardObserver>();

    public void AddObserver(ICardObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(ICardObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnCardFlipped(this);
        }
    }

    private void Awake()
    {
        cardAnimation = GetComponent<CardAnimation>();
        parentRect = GetComponent<RectTransform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = CardState.Hidden;
        cardAnimation.OnCardFlipped += OnCardFlippedHandler;
    }

    private void OnCardFlippedHandler()
    {
        if (cardAnimation.faceUp)
        {
            State = CardState.Visible;
        }
        else
        {
            State = CardState.Hidden;
        }
    }

    public void CloseCard()
    {
        if (cardAnimation.faceUp)
        {
            cardAnimation.FlipCard();
        }
    }

    public void MatchCard()
    {
        State = CardState.Matched;
    }

    public static bool operator == (Card card1, Card card2)
    {
        if (ReferenceEquals(card1, null) || ReferenceEquals(card2, null)) { return false; }
        return card1.cardAnimation.CardFront == card2.cardAnimation.CardFront;
    }

    public static bool operator !=(Card card1, Card card2)
    {
        return !(card1 == card2);
    }

    public void SetCard(int id, Sprite cardFront, float cellSize)
    {
        this.id = id;
        cardAnimation.CardFront = cardFront;
        float scale = cellSize / spriteRenderer.sprite.rect.size.x * spriteRenderer.sprite.pixelsPerUnit;
        parentRect.localScale = new Vector3(scale, scale, 1);
    }

}