using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public GameObject InfoPanel;

    public Button ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseDown);
        ExitButton.onClick.AddListener(ExitPanel);
        InfoPanel.SetActive(false);

    }

    void OnMouseDown()
    {
        InfoPanel.SetActive(true);
        
    }
    void ExitPanel()
    {
        InfoPanel.SetActive(false);
    }

}
