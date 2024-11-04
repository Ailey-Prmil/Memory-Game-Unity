using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public Sprite[] SpriteCollection;
    public List<Card> Cards = new List<Card>();
    public List<Sprite> CardFace = new List<Sprite>();
    
    void Awake()
    {
        SpriteCollection = Resources.LoadAll<Sprite>("Sprites/CardSprites");
    }

    void Start()
    {
        GetButton();
        //AddButtonListener();
    }
    void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < objects.Length; i++)
        {
            Cards.Add(objects[i].GetComponent<Card>());
            Cards[i].SetCard(i, SpriteCollection[4]);
        }
    }
}
