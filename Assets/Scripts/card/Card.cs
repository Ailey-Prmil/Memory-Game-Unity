using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [HideInInspector] public int id;
    public Sprite cardBack;
    [HideInInspector] public Sprite cardFront;

    private Image image;
    private Button button;

    private bool isFlippingOpen;
    private bool isFlippingClose;
    private bool flipped;//true==cardfront
    private float flipAmount = 1;
    public float flipSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    //Onclick to flip card open
    public void FlipCard()
    {
        if (CardManager.instance.choise1 == 0)
        {
            CardManager.instance.choise1 = id;
            CardManager.instance.AddChoosenCard(this.gameObject);
            isFlippingOpen = true;
            StartCoroutine(FlipOpen());
            button.interactable = false;
        }
        else if (CardManager.instance.choise2 == 0)
        {
            CardManager.instance.choise2 = id;
            CardManager.instance.AddChoosenCard(this.gameObject);
            isFlippingOpen = true;
            StartCoroutine(FlipOpen());
            button.interactable = false;
            StartCoroutine (CardManager.instance.CompareCards());
        }
    }
    // open the card over time
    IEnumerator FlipOpen()
    {
        while (isFlippingOpen && flipAmount > 0)
        {
            flipAmount -= Time.deltaTime * flipSpeed;
            flipAmount = Mathf.Clamp01(flipAmount);
            transform.localScale = new Vector3(flipAmount, transform.localScale.y, transform.localScale.z);
            if (flipAmount <= 0)
            {
                image.sprite = cardFront;
                isFlippingOpen = false;
                isFlippingClose = true;
            }
            yield return null;
        }
        while (isFlippingClose && flipAmount < 1)
        {
            flipAmount += Time.deltaTime * flipSpeed;
            flipAmount = Mathf.Clamp01(flipAmount);
            transform.localScale = new Vector3(flipAmount, transform.localScale.y, transform.localScale.z);
            if (flipAmount >= 1)
            {
                isFlippingClose = false;
            }
            yield return null;
        }
    }

    //closecard
    public void CloseCard()
    {
        isFlippingOpen = true;
        StartCoroutine(FlipClose());
    }
    //close the card over time
    IEnumerator FlipClose()
    {
        while (isFlippingOpen && flipAmount > 0)
        {
            flipAmount -= Time.deltaTime * flipSpeed;
            flipAmount = Mathf.Clamp01(flipAmount);
            transform.localScale = new Vector3(flipAmount, transform.localScale.y, transform.localScale.z);
            if (flipAmount <= 0)
            {
                image.sprite = cardBack;
                isFlippingOpen = false;
                isFlippingClose = true;
            }
            yield return null;
        }
        while (isFlippingClose && flipAmount < 1)
        {
            flipAmount += Time.deltaTime * flipSpeed;
            flipAmount = Mathf.Clamp01(flipAmount);
            transform.localScale = new Vector3(flipAmount, transform.localScale.y, transform.localScale.z);
            if (flipAmount >= 1)
            {
                isFlippingClose = false;
            }
            yield return null;
        }
        button.interactable = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}