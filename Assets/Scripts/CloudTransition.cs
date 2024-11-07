using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CloudTransition : MonoBehaviour
    {
        public RectTransform CloudRectTransform;
        public float Duration;
        public float CloudHeight;

        void Awake()
        {
            CloudRectTransform = GetComponent<RectTransform>();
            Duration = 2f;
            CloudHeight = 1800f;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public IEnumerator CloudInTransition()
        {
            float elapsedTime = 0f;

            while (elapsedTime < Duration)
            {
                CloudRectTransform.sizeDelta = new Vector2(CloudRectTransform.rect.width, Mathf.Lerp(CloudHeight, 0, elapsedTime / Duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public IEnumerator CloudOutTransition()
        {
            float elapsedTime = 0f;

            while (elapsedTime < Duration)
            {
                CloudRectTransform.sizeDelta = new Vector2(CloudRectTransform.rect.width, Mathf.Lerp(0, CloudHeight, elapsedTime / Duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
