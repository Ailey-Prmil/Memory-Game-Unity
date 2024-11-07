using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts
{
    public class CardGrid : MonoBehaviour, ICardObserver
    {
        public int Dimension;
        public GameObject CardPrefab;
        private List<Card> Cards = new List<Card>();
        public List<Card> OpenCards = new List<Card>(2);
        private List<Card> matchedCards = new List<Card>();
        private List<Sprite> CardFaces = new List<Sprite>();
        public Publisher EventManager = new Publisher();
        private GridLayoutGroup gridLayoutGroup;
        public CountDownAnimation CountDownAnimation;
        private float CardSize;
        private float GridWidth, XOffset, YOffset;
        private float Padding;

        void Awake()
        {
            GridWidth = GetComponent<RectTransform>().rect.width;
            Padding = 50f;
            XOffset = YOffset = 9f;
            CardSize = (GridWidth - (Padding * 2) - (XOffset * (Dimension - 1))) / Dimension;
            FormCardGrid();
            InitializeCardFaces(GameManager.Instance.SpriteCollection);
            AddCard();
        }
        void Start()
        {
            GetCard();
        }

        public void ResetGrid(Action callback)
        {
            ShuffleCardFaces(CardFaces);
            matchedCards.Clear();
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].ResetCard();
                Cards[i].SetCard(i, CardFaces[i], CardSize);
            }
            
            StartCoroutine(showAllCards(5, callback));

        }

        void FormCardGrid()
        {
            if (gridLayoutGroup == null)
            {
                gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            }

            gridLayoutGroup.spacing = new Vector2(XOffset, YOffset);
            gridLayoutGroup.cellSize = new Vector2(CardSize, CardSize);
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = Dimension;
            gridLayoutGroup.padding = new RectOffset((int)Padding, (int)Padding, (int)Padding, (int)Padding);
        }

        void InitializeCardFaces(List<Sprite> spriteCollection)
        {
            spriteCollection = ShuffleCardFaces(spriteCollection);
            for (int times = 0; times < 2; times++) // add 2 times the same sprite
            {
                for (int i = 0; i < (Dimension * Dimension) / 2; i++)
                {
                    CardFaces.Add(spriteCollection[i]);
                }
            }
        }

        List<Sprite> ShuffleCardFaces(List<Sprite> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Sprite temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }

            return list;
        }

        void AddCard()
        {
            for (int i = 0; i < Dimension * Dimension; i++)
            {
                GameObject card = Instantiate(CardPrefab, GetComponent<RectTransform>(), false);
                card.name = "Card-" + i;
            }
        }

        void GetCard()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < objects.Length; i++)
            {
                Debug.Log("Card-" + i);
                Cards.Add(objects[i].GetComponent<Card>());
                Cards[i].SetCard(i, CardFaces[i], CardSize);
                Cards[i].EventManager.AddObserver(this);
            }
        }

        public IEnumerator showAllCards(float duration, Action callback)
        {
            // faceUp must be false at first
            yield return new WaitForSeconds(1f); // wait for load transition
            
            CountDownAnimation.StartCountdown((int)duration);
            foreach (Card card in Cards)
            {
                card.AutoOpenCard(); // flip all cards open
            }
            yield return new WaitForSeconds(duration);
            foreach (Card card in Cards)
            {
                card.AutoCloseCard(); // flip all cards closed
            }
            Debug.Log("showAllCards end");
            callback?.Invoke();
        }

        void CheckPair(Card firstCard, Card secondCard)
        {
            if (firstCard == secondCard)
            {
                firstCard.MatchCard();
                secondCard.MatchCard();
                matchedCards.Add(firstCard);
                matchedCards.Add(secondCard);
                EventManager.NotifyObservers(this, GridEventType.CardMatched);
                if (matchedCards.Count == Cards.Count)
                {
                    EventManager.NotifyObservers(this, GridEventType.AllCardsMatched);
                }
            }
            else
            {
                firstCard.CloseCard();
                secondCard.CloseCard();
                EventManager.NotifyObservers(this, GridEventType.CardFailed);
            }
            OpenCards.Clear();
        }

        public void OnCardFlipped(Card card)
        {
            if (card.State != Card.CardState.Visible)
            {
                return;
            }
            // Only run the card is flipped open
            if (OpenCards.Count < 2)
            {
                OpenCards.Add(card);
            }
            if (OpenCards.Count == 2)
            {
                Card firstCard = OpenCards[0];
                Card secondCard = OpenCards[1];
                CheckPair(firstCard, secondCard);
            }
        }
        public void OnNotify(MonoBehaviour publisher, object eventType)
        {
            Card card = publisher as Card;
            if (card == null)
            {
                Debug.LogError("Publisher is not a Card!");
                return;
            }
            if (card.State == Card.CardState.Visible)
            {
                OnCardFlipped(card);
            }
        }


    }
}
