using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets.Scripts;
using Assets.Scripts.Enums;
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
            EventManager.NotifyObservers(this, CardEventTypes.CardFlipped);
        }
    }
    public Publisher EventManager = new Publisher();

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

    public void AutoFlipCard()
    {
        cardAnimation.AutoFlipCard();
    }

    public void OpenCard()
    {
        if (!cardAnimation.faceUp)
        {
            cardAnimation.FlipCard();
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
        // implement the disappearance of the card

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