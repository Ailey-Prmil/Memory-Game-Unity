using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardGrid;
    public void AddCard()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardGrid, false);
            card.name = "Card-" + i;
        }
    }
    void Awake()
    {
        AddCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
