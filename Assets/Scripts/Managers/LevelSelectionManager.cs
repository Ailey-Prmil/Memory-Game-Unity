using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LevelSelectionManager : MonoBehaviour
    {
        public Button button4x4;
        public Button button6x6;
        public Button button8x8;

        private Button selectedButton;

        void Start()
        {
            button4x4.onClick.AddListener(() => SelectLevel(button4x4));
            button6x6.onClick.AddListener(() => SelectLevel(button6x6));
            button8x8.onClick.AddListener(() => SelectLevel(button8x8));
            SelectLevel(button4x4);
        }

        void SelectLevel(Button button)
        {
            if (selectedButton != null)
            {
                selectedButton.transform.localScale = Vector3.one; 
            }

            selectedButton = button;
            selectedButton.transform.localScale = new Vector3(1.2f, 1.2f, 1); 
        }

    }
}
