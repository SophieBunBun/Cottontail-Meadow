using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPrompt : MonoBehaviour
{
    public RectTransform mainPanel;
    public TextMeshProUGUI promptText;
    public Button confirmButton;
    public Button cancelButton;

    public void ToggleElement(bool value){

        mainPanel.gameObject.SetActive(value);
    }
}
