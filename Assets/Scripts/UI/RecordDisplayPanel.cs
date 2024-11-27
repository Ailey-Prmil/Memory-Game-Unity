using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Assets.Scripts.Data;
using Assets.Scripts.Managers;
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
    public TopDataContent[] TopDataContents;


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

        TopDataContents = GetComponentsInChildren<TopDataContent>();
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
        List<GameResult> topGameResults = ResultDataManager.Instance.GetTopResults(3, gridDimension);

        for (int i = 0; i < 3; i++)
        {
            if (i >= topGameResults.Count)
            {
                TopDataContents[i].gameObject.SetActive(false);
                continue;
            }
            TopDataContents[i].gameObject.SetActive(true);
            TopDataContents[i].ScoreText.text = topGameResults[i].Score.ToString();
            TopDataContents[i].DateText.text = topGameResults[i].Date;
            TopDataContents[i].StreakText.text = topGameResults[i].Streak.ToString();
        }
        DataDisplayPanel.SetActive(true);
    }
}
