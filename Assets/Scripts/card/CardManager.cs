using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Sprite> SpriteList = new List<Sprite>();

    [SerializeField] private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] private List<GameObject> hiddenButtonList = new List<GameObject>();

    private List<GameObject> choosenCards = new List<GameObject>();
    private int lastMacthId;
    [SerializeField] private bool choosen;
    [Header("How many pairs you want to play?")]
    public int pairs;
    [Header("Card Prefab Button")]
    public GameObject cardPrefab;
    [Header("The Parent Spacer to sort Cards in")]
    public Transform spacer;
    //particle
    [Header("Basic Score per Match")]
    public int matchScore = 100;

    public int choise1;
    public int choise2;
    private int lastMatchId;

    void Awake()
    {
        instance = this;
    }
    public void AddChoosenCard(GameObject card)
    {
        choosenCards.Add(card); 
    }

    // Start is called before the first frame update
    void Start()
    {
        FillPlayField();
    }
    void FillPlayField()
    {
        for (int i = 0; i < (pairs * 2); i++)
        {
            GameObject newCard = Instantiate(cardPrefab, spacer);
            buttonList.Add(newCard);
            hiddenButtonList.Add(newCard);
        }
        ShuffleCards();
    }
    void ShuffleCards()
    {
        int num = 0;
        int cardPairs = buttonList.Count / 2;
        for (int i = 0; i < cardPairs; i++)
        {
            num++;
            for (int j = 0; j < 2; j++) // count card amount per match
            {
                int cardIndex = Random.Range(0, buttonList.Count);
                Card tempCard = buttonList[cardIndex].GetComponent<Card>();
                tempCard.id = num;
                tempCard.cardFront = SpriteList[num - 1];
                buttonList.Remove(buttonList[cardIndex]);
            }
        }
    }
    public IEnumerator CompareCards()
    {
        if (choise2 == 0 || choosen)
        {
            yield break;
        }
        choosen = true;
        yield return new WaitForSeconds(1.5f);
        //No match;
        if ((choise1 != 0 && choise2 != 0) && (choise1 != choise2))
        {
            //flip back open cards
            FlipAllBack();
            //reset the combo in scoremanager
            ScoreManager.instance.ResetCombo();
        }
        else if ((choise1 != 0 && choise2 != 0) && (choise1 == choise2))
        {
            lastMacthId = choise1;
            // add score
            ScoreManager.instance.AddScore(matchScore);
            //remove the match
            RemoveMatch();
            // clear choosencards
            choosenCards.Clear();
        }
        //reset all choosencards

        choise1 = 0;
        choise2 = 0;
        choosen = false;

        //check if won
    }
    void FlipAllBack()
    {
        foreach (GameObject card in choosenCards)
        {
            card.GetComponent<Card>().CloseCard();
        }
        choosenCards.Clear ();
    }
    void RemoveMatch()
    {
        for (int i = hiddenButtonList.Count - 1; i >= 0; i--)
        {
            Card tempCard = hiddenButtonList[i].GetComponent<Card>();
            if (tempCard.id == lastMatchId)
            {
                //particle fx

                //remove the card
                hiddenButtonList.RemoveAt(i);

            }
        }
    }
    void CheckWin()
    {
        if(hiddenButtonList.Count < 1)
        {
            // stop timer 
            ScoreManager.instance.StopTime();
            // show ui
            // play firework
            //show stars
            Debug.Log("You Won!");
        }
    }
}


    
