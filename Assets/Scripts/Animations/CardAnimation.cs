using System;
using System.Collections;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class CardAnimation : MonoBehaviour
    {
        public event Action OnCardFlipped;
        private SpriteRenderer _rend;
        private bool _coroutineAllowed;
        public bool FaceUp { get; set; }
        public Sprite CardBack, CardFront;

        void Awake()
        {
            _rend = GetComponent<SpriteRenderer>();
            _rend.sprite = CardBack;
            _coroutineAllowed = true;
            FaceUp = false;
        }

        public void FlipCard()
        {
            if (_coroutineAllowed)
            {
                StartCoroutine(RotateCard(() => { FaceUp = !FaceUp; OnCardFlipped?.Invoke(); }));
            }
        }

        public void AutoFlipCard()
        // Auto flip card happens when the game is start where there is no need to change the card state
        // Avoid confusing with the Awake function where state is default to be hidden
        {
            StartCoroutine(RotateCard(() => { FaceUp = !FaceUp;}));
        }


        private IEnumerator RotateCard(Action callback) 
        {
            // Rotate card to show the front or back side
            _coroutineAllowed = false;
            SoundManager.Instance.PlayCardFlipSound();
            if (!FaceUp)
            {
                // Open card
                for (float i = 0f; i <= 180f; i += 10f)
                {
                    transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
                    if (i == 90f)
                    {
                        _rend.sprite = CardFront;
                    }
                    yield return new WaitForSeconds(0.03f);
                }
            }
            else
            {
                // Close card
                for (float i = 180f; i >= 0f; i -= 10f)
                {
                    transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
                    if (i == 90f)
                    {
                        _rend.sprite = CardBack;
                    }
                    yield return new WaitForSeconds(0.03f);
                }
            }
            _coroutineAllowed = true;
            callback?.Invoke();
        }


    }
}
