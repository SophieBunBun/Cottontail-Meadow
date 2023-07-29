using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanningSelectionPanel : MonoBehaviour
{
    [Header("Selection window elements")]
    public TextMeshProUGUI selectionName;
    public Image selectionIcon;
    public TextMeshProUGUI selectionDescription;
    public RectTransform contents;
    private List<ButtonLayout> selectionButtons = new List<ButtonLayout>();
    public Button proceedButton;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;

    public delegate void callbackMethod(string item);

    void Awake(){
        canvasGroup = this.GetComponent<CanvasGroup>();
        rectTransform = this.GetComponent<RectTransform>();
    }
    public void Open(){
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(UIUtils.movePanel(rectTransform, rectTransform.localPosition, new Vector2(-800f, 800f)));
    }
    public void Close(){
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(UIUtils.movePanel(rectTransform, rectTransform.localPosition, new Vector2(800f, -800f)));
    }

    public void openAndSet(string name, string spriteId, string description,
                           string[] items, string exception,
                           Dictionary<string, string> names,
                           Dictionary<string, string> descriptions,
                           Dictionary<string, int> costs,
                           Dictionary<string, string> itemToIcon,
                           callbackMethod callback){
        
        Open();
        setSelectionInfo(name, spriteId, description);
        createSelectionButtons(items, exception, names, descriptions, costs, itemToIcon, callback);
    }

    private void setSelectionInfo(string name, string spriteId, string description){

        selectionIcon.sprite = GameManager.Instance.getSprite(spriteId);
        selectionName.text = name;
        selectionDescription.text = description;
    }

    private void createSelectionButtons(string[] items, string exception,
                                        Dictionary<string, string> names,
                                        Dictionary<string, string> descriptions,
                                        Dictionary<string, int> costs,
                                        Dictionary<string, string> itemToIcon,
                                        callbackMethod callback){

        foreach (ButtonLayout button in selectionButtons){
 
            Destroy(button.gameObject);
        }

        selectionButtons = new List<ButtonLayout>();

        foreach (string item in items){

            if (GameManager.Instance.isUnlocked[item] && exception != item){

                ButtonLayout selectionButton =
                Instantiate(
                (GameObject)GameManager.Instance.getResource("general:ui:resourceSelectionButton"), contents
                            ).GetComponent<ButtonLayout>();
                
                selectionButton.text.text = names[item];
                selectionButton.GetComponentInChildren<ItemDisplay>().text.text = costs[item].ToString();
                selectionButton.GetComponentInChildren<ItemDisplay>().image.sprite = GameManager.Instance.getSprite("sprites:itemIcon:money");
                selectionButton.icon.sprite = GameManager.Instance.getSprite(itemToIcon[item]);
                selectionButton.button.onClick.AddListener(delegate {setSelectionButton(selectionButton); setSelection(item, costs, descriptions, callback);});

                selectionButtons.Add(selectionButton);
            }
        }
    }

    private void setSelectionButton(ButtonLayout button){

        foreach (ButtonLayout buttonL in selectionButtons){

            if (buttonL != button && buttonL.button.interactable == false){

                buttonL.button.interactable = true;
                buttonL.transform.GetComponent<CanvasGroup>().alpha = 1;
            }
        }

        button.button.interactable = false;
        button.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void setSelection(string item, Dictionary<string, int> costs, Dictionary<string, string> description, callbackMethod method){

        selectionDescription.text = description[item];

        if (GameManager.Instance.itemInventory.getItemCount("money") >= costs[item]){

            proceedButton.interactable = true;
            proceedButton.onClick.RemoveAllListeners();
            proceedButton.onClick.AddListener(delegate {method(item);});
        }
        else{

            proceedButton.interactable = false;
        }
    }
}
