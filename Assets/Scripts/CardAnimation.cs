using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CardAnimation : MonoBehaviour
    {
        public event Action OnCardFlipped;
        private SpriteRenderer rend;
        private bool coroutineAllowed;
        public bool faceUp { get; set; }
        public Sprite CardBack, CardFront;

        void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
            rend.sprite = CardBack;
            coroutineAllowed = true;
            faceUp = false;
        }

        private void OnMouseDown() // Detects click directly on the card
        {
            if (GameManager.Instance.IsGameRunning) OpenCard(); // only allow open card when game is running
        }

        public void FlipCard() // Detects click on the button
        {
            if (coroutineAllowed)
            {
                StartCoroutine(RotateCard(() => { faceUp = !faceUp; OnCardFlipped?.Invoke(); }));
            }
        }

        public void AutoFlipCard()
        {
            StartCoroutine(RotateCard(() => { faceUp = !faceUp;}));
        }

        public void OpenCard()
        {
            if (faceUp == false)
            {
                FlipCard();
            }
        }

        private IEnumerator RotateCard(Action callback)
        {
            Debug.Log("RotateCard start");
            coroutineAllowed = false;
            if (!faceUp)
            {
                // Open card
                for (float i = 0f; i <= 180f; i += 10f)
                {
                    transform.rotation = UnityEngine.Quaternion.Euler(0f, i, 0f);
                    if (i == 90f)
                    {
                        rend.sprite = CardFront;
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
                        rend.sprite = CardBack;
                    }
                    yield return new WaitForSeconds(0.03f);
                }
            }
            Debug.Log("RotateCard end");
            coroutineAllowed = true;
            callback?.Invoke();
            Debug.Log($"FaceUp = {faceUp}");
        }


    }
}
