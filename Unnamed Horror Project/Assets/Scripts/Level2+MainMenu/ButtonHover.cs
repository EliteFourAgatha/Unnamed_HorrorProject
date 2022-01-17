using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseOver = false;
    public Button playButton;
    public Text playButtonText;
    private Color buttonOnColor = new Color(0.25f, 0.25f, 0.25f, 1);
    private Color textOnColor = new Color(0.9f, 0.8f, 0.3f, 1);
    private Color buttonOffColor = new Color(0.25f, 0.25f, 0.25f, 0);
    private Color textOffColor = new Color(0.9f, 0.8f, 0.3f, 0);
    void Update()
    {
        if(mouseOver)
        {
            
            ColorBlock buttonColors = playButton.colors;
            buttonColors.normalColor = buttonOnColor;
            playButtonText.color = textOnColor;
            //playButton.SetActive(true);
        }
        else
        {
            
            ColorBlock buttonColors = playButton.colors;
            buttonColors.normalColor = buttonOffColor;
            playButtonText.color = textOffColor;
            //playButton.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        Debug.Log("Mouse exit");
    }
}
