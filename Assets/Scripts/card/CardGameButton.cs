using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using UnityEngine.EventSystems;


public class CardGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Color originalColor;
    private Vector3 originalScale;
    private Image buttonImage;
    private Animator animator;
    private String hoveredColor = "#B1C8CD";

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("OnHover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("OnExit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.color = ParseHexColor(hoveredColor); // Change color on click
        // Optionally add a delay before changing scenes to visualize the click effect
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainCardGameScene"); // Replace "NextScene" with the name of your scene
    }

    private Color ParseHexColor (string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
