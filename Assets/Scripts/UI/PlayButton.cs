using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PlayButton : MonoBehaviour
    {
        public GameObject setUpCanvas; // Tham chiếu tới SetUpCanvas

        void Start()
        {
            // Kiểm tra xem setup canvas có được tham chiếu chưa
            if (setUpCanvas != null)
            {
                // Đảm bảo setup canvas ẩn ban đầu
                setUpCanvas.SetActive(false);
            }

            // Thêm sự kiện khi nhấn nút Play
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (setUpCanvas != null)
                {
                    setUpCanvas.SetActive(true); // Bật setup canvas khi nhấn nút Play
                }
            });
        }
    }
}

