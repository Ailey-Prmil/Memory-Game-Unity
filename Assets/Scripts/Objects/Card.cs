using Assets.Scripts.Animations;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class Card : MonoBehaviour
    {
        private int _id;
        private CardAnimation _cardAnimation;
        private RectTransform _parentRect;
        private SpriteRenderer _spriteRenderer;
        private CardStates _state;

        public EventManager CardEventManager = new();
        public CardStates State
        {
            get => _state;
            private set
            {
                _state = value;
                CardEventManager.NotifyObservers(this, CardEventTypes.CardFlipped);
            }
        }
        
        private void Awake()
        {
            // Get components and set default state
            _cardAnimation = GetComponent<CardAnimation>();
            _parentRect = GetComponent<RectTransform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _state = CardStates.Hidden;
            _cardAnimation.OnCardFlipped += OnCardFlippedHandler; // Subscribe to card flip event (in CardAnimation)
        }

        private void OnCardFlippedHandler()
        {
            // Change card state based on card animation
            if (_cardAnimation.FaceUp)
            {
                State = CardStates.Visible;
            }
            else
            {
                State = CardStates.Hidden;
            }
        }

        private void OnMouseDown()
        {
            // Detect mouse click on certain cards
            if (GameManager.Instance.IsGameRunning) OpenCard(); // Only allow open card when game is running
        }


        public void SetCard(int id, Sprite cardFront, float cellSize)
        {
            // Set card id, card front and scale
            this._id = id;
            _cardAnimation.CardFront = cardFront;
            float scale = cellSize / _spriteRenderer.sprite.rect.size.x * _spriteRenderer.sprite.pixelsPerUnit;
            _parentRect.localScale = new Vector3(scale, scale, 1);
        }

        public void ResetCard()
        {
            // Reset card state
            // Do not state of card when reset (using AutoFlipCard instead of FlipCard)
            _state = CardStates.Hidden;
            AutoCloseCard();
        }

        public void OpenCard()
        {
            if (!_cardAnimation.FaceUp)
            {
                _cardAnimation.FlipCard();
            }
        }

        public void AutoOpenCard()
        {
            if (!_cardAnimation.FaceUp)
            {
                _cardAnimation.AutoFlipCard();
            }
        }

        public void CloseCard()
        {
            if (_cardAnimation.FaceUp)
            {
                _cardAnimation.FlipCard();
            }
        }

        public void AutoCloseCard()
        {
            if (_cardAnimation.FaceUp)
            {
                _cardAnimation.AutoFlipCard();
            }
        }

        public void MatchCard()
        {
            State = CardStates.Matched;
        }
        

        // Compare 2 cards
        public static bool operator == (Card card1, Card card2)
        {
            if (ReferenceEquals(card1, null) || ReferenceEquals(card2, null)) { return false; }
            return card1._cardAnimation.CardFront == card2._cardAnimation.CardFront;
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

    }
}