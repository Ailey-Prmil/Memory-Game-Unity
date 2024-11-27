using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordDisplayPanel : OptionPanel
{
    private Dictionary<string, int> buttonNameToIntMap;

    public GameObject DataDisplayPanel;
    public Button backButton;
    public List<Button> GridDimensionButtons;
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    public TMP_Text StreakText;

    protected override void Awake()
    {
        buttonNameToIntMap = new Dictionary<string, int>
        {
            {"4x4Button", 4},
            {"6x6Button", 6},
            {"8x8Button", 8}
        };
        base.Awake();
        if (backButton != null) backButton.onClick.AddListener(BackToOptionPanel);
        foreach(var button in GridDimensionButtons)
        {
            if (buttonNameToIntMap.TryGetValue(button.name, out int gridDim))
            {
                button.onClick.AddListener(() => OpenDataDisplayPanel(gridDim));
            }
            else
            {
                Debug.LogError("Button name not found in buttonNameToIntMap");
            }
            
        }
    }

    protected override void Start()
    {
        base.Start();
        DataDisplayPanel.SetActive(false);
    }

    void BackToOptionPanel()
    {
        DataDisplayPanel.SetActive(false);
    }

    void OpenDataDisplayPanel(int gridDimension)
    {
        DataDisplayPanel.SetActive(true);
    }
}
