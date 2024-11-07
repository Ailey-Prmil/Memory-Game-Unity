using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using System.Collections.ObjectModel;

public class CardAnimation : MonoBehaviour
{
    public event Action OnCardFlipped;
    private SpriteRenderer rend;
    private bool coroutineAllowed;
    public bool faceUp { get; set; }
    public Sprite CardBack, CardFront;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = CardBack;
        coroutineAllowed = true;
        faceUp = false;
    }

    private void OnMouseDown() // Detects click directly on the card
    {
        OpenCard();
    }

    public void FlipCard() // Detects click on the button
    {
        if (coroutineAllowed)
        {
            StartCoroutine(RotateCard(() => { faceUp = !faceUp; OnCardFlipped?.Invoke(); }));
        }
    }

    public void AutoFlipCard()
    {
        StartCoroutine(RotateCard(() => { faceUp = !faceUp;}));
    }

    public void OpenCard()
    {
        if (faceUp == false)
        {
            FlipCard();
        }
    }

    private IEnumerator RotateCard(Action callback)
    {
        coroutineAllowed = false;
        if (!faceUp)
        {
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = CardFront;
                }
                yield return new WaitForSeconds(0.03f);
            }
        }
        else
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = CardBack;
                }
                yield return new WaitForSeconds(0.03f);
            }
        }
        coroutineAllowed = true;
        callback?.Invoke();
    }


}
