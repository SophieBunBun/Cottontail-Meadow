using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenuController : MonoBehaviour
{
    [Header("Main elements")]
    public RectTransform mainPanel;
    public RectTransform inspectionPanel;
    public CanvasGroup canvasGroup;
    public Button closeUIButton;

    [Header("Item buttons")]
    public RectTransform content;
    
    [Header("UpgradeProceedWindow")]
    public TextMeshProUGUI itemName;
    public Image itemIcon;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI ownedCount;

    void Start(){
        
        closeUIButton.onClick.AddListener(delegate { close(); });
    }

    private IEnumerator CloseUI(){

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        GameManager.Instance.changeState("farmRoaming");

        float lerp = 0f;

        while (lerp < 1f){

            mainPanel.transform.localPosition = new Vector2(Mathf.Lerp(1750f, 2550f, lerp), 0f);
            inspectionPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Max(1200f, inspectionPanel.transform.localPosition.x), 2550f, lerp), -40f);

            if (Camera.main.GetComponent<CameraController>().orthographic){
                Camera.main.transform.localPosition =
                new Vector3(Mathf.Lerp(27f, 25f, lerp),
                        Camera.main.transform.localPosition.y,
                        Mathf.Lerp(-23f, -25f, lerp));
            }
            else{
                Camera.main.transform.localPosition =
                new Vector3(Mathf.Lerp(-2f, 0f, lerp),
                        Camera.main.transform.localPosition.y,
                        Camera.main.transform.localPosition.z);
            }

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }


    }

    private IEnumerator OpenUI(){

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GameManager.Instance.changeState("farmPrompt");

        float lerp = 0f;

        while (lerp < 1f){

            mainPanel.transform.localPosition = new Vector2(Mathf.Lerp(2550f, 1750f, lerp), 0f);

            if (Camera.main.GetComponent<CameraController>().orthographic){
                Camera.main.transform.localPosition =
                new Vector3(Mathf.Lerp(25f, 27f, lerp),
                        Camera.main.transform.localPosition.y,
                        Mathf.Lerp(-25f, -23f, lerp));
            }
            else{
                Camera.main.transform.localPosition =
                new Vector3(Mathf.Lerp(0f, -2f, lerp),
                        Camera.main.transform.localPosition.y,
                        Camera.main.transform.localPosition.z);
            }

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator OpenInspectionUI(){

        float lerp = 0f;

        while (lerp < 1f){

            inspectionPanel.transform.localPosition = new Vector2(Mathf.Lerp(1750f, 1200f, lerp), -40f);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    public void close(){

        StartCoroutine(CloseUI());
    }


    public void openInventory(){

        createItemButtons();
        StartCoroutine(OpenUI());
    }

    private void createItemButtons(){

        foreach (Transform child in content){

            Destroy(child.gameObject);
        }

        foreach (Item item in GameManager.Instance.itemInventory.items.Values){

            ButtonLayout itemButton = Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:itemDisplayButton"), content.transform).GetComponent<ButtonLayout>();

            itemButton.icon.sprite = GameManager.Instance.getSprite(string.Format("sprites:itemIcon:{0}", item.itemId));
            itemButton.text.text = item.itemCount.ToString();
            itemButton.button.onClick.AddListener(delegate {openInspector(item);});
        }

    }

    private void openInspector(Item item){

        itemName.text = FixedVariables.itemNames[item.itemId];
        itemDescription.text = FixedVariables.itemDescription[item.itemId];
        ownedCount.text = item.itemCount.ToString();
        itemIcon.sprite = GameManager.Instance.getSprite(string.Format("sprites:itemIcon:{0}", item.itemId));
        StartCoroutine(OpenInspectionUI());
    }
}
