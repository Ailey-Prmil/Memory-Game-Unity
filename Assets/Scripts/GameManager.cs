using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public Sprite[] SpriteCollection;
    public List<Button> Buttons = new List<Button>();
    public List<Sprite> CardFace = new List<Sprite>();
    
    void Awake()
    {
        SpriteCollection = Resources.LoadAll<Sprite>("Sprites");
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
            Buttons.Add(objects[i].GetComponent<Button>());
        }
    }

    //void AddButtonListener()
    //{
    //    foreach (Button button in Buttons)
    //    {
    //        button.onClick.AddListener(CardClick);
    //    }
    //}

    //public void CardClick()
    //{
    //    Debug.Log("Button Clicked");
    //}
}
