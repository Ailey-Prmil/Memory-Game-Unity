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
    private RectTransform parentRect;
    private SpriteRenderer spriteRenderer;
    private CardState state;

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
        Debug.Log("Card Flipped");
        Debug.Log("Card ID: " + id);
        Debug.Log("Card Front Sprite: " + cardAnimation.CardFront);
        Debug.Log("====================");
    }

    public void SetCard(int id, Sprite cardFront, float cellSize)
    {
        this.id = id;
        cardAnimation.CardFront = cardFront;
        float scale = cellSize / spriteRenderer.sprite.rect.size.x * spriteRenderer.sprite.pixelsPerUnit;
        parentRect.localScale = new Vector3(scale, scale, 1);
    }

}