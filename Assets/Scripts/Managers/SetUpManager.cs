using Assets.Scripts.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SetUpManager : MonoBehaviour
    {
        public Button button4x4;
        public Button button6x6;
        public Button button8x8;
        public Button playStartButton;

        private void Start()
        {
            // Gán sự kiện cho các nút để chọn kích thước ma trận
            button4x4.onClick.AddListener(() => SetDimension(4));
            button6x6.onClick.AddListener(() => SetDimension(6));
            button8x8.onClick.AddListener(() => SetDimension(8));

            // Gán sự kiện cho nút PlayStartButton để bắt đầu trò chơi
            playStartButton.onClick.AddListener(StartGame);
        }

        // Hàm để thiết lập kích thước ma trận
        private void SetDimension(int dimension)
        {
            CardGrid.SelectedDimension = dimension; // Lưu kích thước ma trận vào CardGrid
            Debug.Log("Selected Dimension: " + dimension);
        }

        // Hàm chuyển cảnh sang MainGame khi nhấn PlayStartButton
        private void StartGame()
        {
            SceneManager.LoadScene("MainGameScene"); // Chuyển sang cảnh MainGame
        }
    }
}
