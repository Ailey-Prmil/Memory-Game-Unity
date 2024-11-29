using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Animations;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Objects
{
    public class CardGrid : MonoBehaviour, ICardObserver
    {
        public static int SelectedDimension = 4;
        [SerializeField]
        private GameObject _cardPrefab;
        private List<Card> _cards;
        private List<Card> _openCards;
        private List<Card> _matchedCards;
        private List<Sprite> _cardFaces;
        private float _cardSize;
        private float _gridWidth, _xOffset, _yOffset;
        private float _padding;
        private GridLayoutGroup _gridLayoutGroup;

        public EventManager GridEventManager = new EventManager();
        public CountDownAnimation CountDownAnimation;

        void Awake()
        {
            int dimension = SelectedDimension;
            _cards = new List<Card>();
            _cardFaces = new List<Sprite>();
            _openCards = new List<Card>(capacity:2);
            _matchedCards = new List<Card>();

            _gridWidth = GetComponent<RectTransform>().rect.width;
            _padding = 50f;
            _xOffset = _yOffset = 9f;
            _cardSize = (_gridWidth - (_padding * 2) - (_xOffset * (dimension - 1))) / dimension;

            FormCardGrid(dimension);
            InitializeCardFaces(GameManager.Instance.SpriteCollection);
            AddCard(dimension);
        }

        void Start()
        {
            GetCard();
        }

        void FormCardGrid(int dimension)
        {
            // form the grid with the given dimension
            if (_gridLayoutGroup == null)
            {
                _gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            }

            _gridLayoutGroup.spacing = new Vector2(_xOffset, _yOffset);
            _gridLayoutGroup.cellSize = new Vector2(_cardSize, _cardSize);
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = dimension;
            _gridLayoutGroup.padding = new RectOffset((int)_padding, (int)_padding, (int)_padding, (int)_padding);
        }

        void InitializeCardFaces(List<Sprite> spriteCollection)
        {
            spriteCollection = ShuffleCardFaces(spriteCollection); // Shuffle the sprite collection

            for (int times = 0; times < 2; times++) // Add 2 times the same sprite
            {
                for (int i = 0; i < (SelectedDimension * SelectedDimension) / 2; i++)
                {
                    _cardFaces.Add(spriteCollection[i]);
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

        void AddCard(int dimension)
        {
            for (int i = 0; i < dimension * dimension; i++)
            {
                GameObject card = Instantiate(_cardPrefab, GetComponent<RectTransform>(), false);
                card.name = "Card-" + i;
            }
        }

        void GetCard()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < objects.Length; i++)
            {
 
                _cards.Add(objects[i].GetComponent<Card>()); // Add the card to the list
                _cards[i].SetCard(i, _cardFaces[i], _cardSize); // Set the card id, card front and scale
                _cards[i].CardEventManager.AddObserver(this); // Add this Grid as an observer to each Card
            }
        }

        void CheckPair(Card firstCard, Card secondCard)
        {
            if (firstCard == secondCard)
            {
                SoundManager.Instance.PlayCardMatchSound(); // Play the card match sound
                // Change the state of both cards to matched
                firstCard.MatchCard();
                secondCard.MatchCard();

                // Add the matched cards to the list
                _matchedCards.Add(firstCard);
                _matchedCards.Add(secondCard);

                GridEventManager.NotifyObservers(this, GridEventType.CardMatched); // Notify observers that a pair is matched

                if (_matchedCards.Count == _cards.Count)
                {
                    GridEventManager.NotifyObservers(this, GridEventType.AllCardsMatched); // Notify observers that all cards are matched
                }
            }
            else
            {
                // Close both cards if they are not matched
                firstCard.CloseCard();
                secondCard.CloseCard();
                GridEventManager.NotifyObservers(this, GridEventType.CardFailed); // Notify observers that a pair is mismatched
            }

            // Clear the open cards list
            _openCards.Clear(); 
        }

        public void ResetGrid(Action callback)
        {
            ShuffleCardFaces(_cardFaces);
            _matchedCards.Clear();
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].ResetCard();
                _cards[i].SetCard(i, _cardFaces[i], _cardSize);
            }

            StartCoroutine(showAllCards(5, callback));
        }

        public void OnCardFlipped(Card card)
        {
            if (card.State != CardStates.Visible)
            {
                return;
            }

            if (_openCards.Count < 2)
            {
                _openCards.Add(card);
            }
            if (_openCards.Count == 2)
            {
                Card firstCard = _openCards[0];
                Card secondCard = _openCards[1];
                CheckPair(firstCard, secondCard);
            }
        }

        public void OnNotify(MonoBehaviour publisher, object eventType)
        // This Grid is a Card Observer and will be notified by the Card
        {
            Card card = publisher as Card;
            if (card == null)
            {
                Debug.LogError("GridEventManager is not a Card!");
                return;
            }
            OnCardFlipped(card); // Call the OnCardFlipped function to handle the flipping card event
        }

        public IEnumerator showAllCards(float duration, Action callback)
        {
            yield return new WaitForSeconds(1f);

            CountDownAnimation.StartCountdown((int)duration);
            foreach (Card card in _cards)
            {
                card.AutoOpenCard();
            }
            yield return new WaitForSeconds(duration);
            foreach (Card card in _cards)
            {
                card.AutoCloseCard();
            }
            callback?.Invoke();
        }
    }
}