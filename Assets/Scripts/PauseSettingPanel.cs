using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingPanel : OptionPanel
{
    public Button RestartButton;

    protected override void Awake()
    {
        RestartButton.onClick.AddListener(RestartGame);
        base.Awake();
    }

    void RestartGame()
    {
        ExitCanvas();
        GameManager.Instance.OnGameStart();
        
    }
}
