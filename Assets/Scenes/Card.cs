using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;

public class Card : MonoBehaviour
{
    private SpriteRenderer rend;
    [SerializeField]
    private Sprite faceSpriteFruitCard_1, Cardback;
    private bool coroutineAllowed, faceUp;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = Cardback;
        coroutineAllowed = true;
        faceUp = false;
    }

    private void OnMouseDown()
    {
        //if (coroutineAllowed)
        //{
        //    StartCoroutine(RotateCard());
        //}
        Debug.Log("Button clicked!");
    }

    //private IEnumerator RotateCard()
    //{
    //    coroutineAllowed = false;

    //    if (!faceUp)
    //    {
    //        for (float i = 0f; i <= 180f; i += 10f)
    //        {
    //            transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
    //            if (i == 90f)
    //            {
    //                rend.sprite = faceSpriteFruitCard_1;
    //            }
    //            yield return new WaitForSeconds(0.03f);
    //        }
    //    }
    //    else
    //    {
    //        for (float i = 180f; i >= 0f; i -= 10f)
    //        {
    //            transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
    //            if (i == 90f)
    //            {
    //                rend.sprite = Cardback;
    //            }
    //            yield return new WaitForSeconds(0.03f);
    //        }
    //    }

    //    coroutineAllowed = true;
    //    faceUp = !faceUp;
    //}

    void Update()
    {
    }
}
