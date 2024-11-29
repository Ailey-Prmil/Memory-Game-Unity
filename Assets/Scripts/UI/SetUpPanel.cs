using Assets.Scripts.Managers;
using Assets.Scripts.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SetUpPanel : MonoBehaviour
    {
        public Button Button4X4;
        public Button Button6X6;
        public Button Button8X8;
        public Button PlayStartButton;

        private void Start()
        {
            // Gán sự kiện cho các nút để chọn kích thước ma trận
            Button4X4.onClick.AddListener(() => SetDimension(4));
            Button6X6.onClick.AddListener(() => SetDimension(6));
            Button8X8.onClick.AddListener(() => SetDimension(8));
            SetDimension(4);

            // Gán sự kiện cho nút PlayStartButton để bắt đầu trò chơi
            PlayStartButton.onClick.AddListener(AppManager.Instance.StartGame);
        }

        // Hàm để thiết lập kích thước ma trận
        private void SetDimension(int dimension)
        {
            CardGrid.SelectedDimension = dimension; // Lưu kích thước ma trận vào CardGri
        }
    }
}
