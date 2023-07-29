using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanningMainPanel : MonoBehaviour
{
    [Header("Planning elements")]
    public ButtonLayout header;
    public RectTransform contents;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;

    [Header("Extra panels")]
    public PlanningSelectionPanel selectionPanel;
    public PlanningConfirmationPanel confirmationPanel;

    void Awake(){
        canvasGroup = this.GetComponent<CanvasGroup>();
        rectTransform = this.GetComponent<RectTransform>();
    }
    public void Open(){
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(UIUtils.movePanel(rectTransform, rectTransform.localPosition, new Vector2(-800f, -100f)));
    }
    public void Close(){
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(UIUtils.movePanel(rectTransform, rectTransform.localPosition, new Vector2(-100f, -800f)));
    }

    public void setHeader(){

    }
    public void createButtons(string[] list, Dictionary<string, bool> type,
        //Basics
        Dictionary<string, string> itemToIcon, Dictionary<string, string> names, Dictionary<string, string> descriptions
        //Selection

        ){
        
        foreach (Transform child in contents){
            Destroy(child.gameObject);
        }

        foreach (string item in list){

            ButtonLayout structureButton =
            Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:upgradeButton"), contents
                        ).GetComponent<ButtonLayout>();

            structureButton.icon.sprite = GameManager.Instance.getSprite(itemToIcon[item]);
            structureButton.text.text = names[item];

            if (type[item]){
                
            }
            else{
                // structureButton.button.onClick.AddListener(delegate {selectionPanel.openAndSet(
                //     names[item], itemToIcon[item], descriptions[item],
                // );});
            }
        }
    }
}
