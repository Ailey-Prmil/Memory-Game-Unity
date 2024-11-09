using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpManager : MonoBehaviour
{
    public GameObject setupPanel; // Tham chiếu đến bảng setup (SetupPanel)
    public Button playButton; // Tham chiếu đến nút Play Game

    private void Start()
    {
        // Đảm bảo bảng setup ẩn khi bắt đầu game
        setupPanel.SetActive(false);

        // Gán sự kiện cho nút Play Game
        playButton.onClick.AddListener(ShowSetupPanel);
    }

    // Hàm hiển thị bảng setup cấp độ
    private void ShowSetupPanel()
    {
        setupPanel.SetActive(true);
    }
}
