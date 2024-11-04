using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    private enum CardState { Hidden, Visible, Matched }

    [HideInInspector] public int id;
    private CardAnimation cardAnimation;
    private CardState state;
    [SerializeField] private float cardSize;

    private void Awake()
    {
        cardAnimation = GetComponent<CardAnimation>();
        state = CardState.Hidden;
        cardAnimation.OnCardFlipped += OnCardFlippedHandler;
    }

    private void OnCardFlippedHandler()
    {
        Debug.Log("Card Flipped");
        Debug.Log("Card ID: " + id);
        Debug.Log("Card Front Sprite: " + cardAnimation.CardFront);
        Debug.Log("====================");
    }

    public void SetCard(int id, Sprite cardFront)
    {
        Debug.Log($"set card to id {id}");
        this.id = id;
        cardAnimation.CardFront = cardFront;
    }

}