using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingPanel : OptionPanel
{
    public Button RestartButton;
    public Button QuitButton;

    protected override void Awake()
    {
        RestartButton.onClick.AddListener(RestartGame);
        QuitButton.onClick.AddListener(QuitGame);
        base.Awake();
    }

    void RestartGame()
    {
        ExitCanvas();
        GameManager.Instance.OnGameStart();
    }

    void QuitGame()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameOver(false);
    }
}
