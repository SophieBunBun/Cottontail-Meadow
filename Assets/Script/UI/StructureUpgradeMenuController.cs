using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StructureUpgradeMenuController : MonoBehaviour
{
    [Header("Main elements")]
    public RectTransform mainPanel;
    public RectTransform upgradePanel;
    public RectTransform selectionPanel;
    public CanvasGroup canvasGroup;
    public Button closeUIButton;

    [Header("Structure Info")]
    public Image structureIcon;
    public TextMeshProUGUI structureName;

    [Header("Upgrade buttons")]
    public RectTransform content;
    
    [Header("UpgradeProceedWindow")]
    public TextMeshProUGUI upgradeName;
    public Image upgradeIcon;
    public TextMeshProUGUI upgradeDescription;
    public RectTransform itemsRequired;
    public RectTransform itemsOwned;
    public Button proceedButton;

    [Header("ResourceSelectionWindow")]
    public TextMeshProUGUI resourceName;
    public Image resourceIcon;
    public TextMeshProUGUI resourceDescription;
    public RectTransform resources;
    private List<ButtonLayout> resourceButtons = new List<ButtonLayout>();
    public Button chooseResourceButton;

    public FarmBase.StructureInstance structure;


    void Start(){
        
        closeUIButton.onClick.AddListener(delegate { close(); });
    }

    private IEnumerator CloseUI(){

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        GameManager.Instance.changeState("farmRoaming");

        float lerp = 0f;

        while (lerp < 1f){

            mainPanel.transform.localPosition = new Vector2(Mathf.Lerp(-100f, -800f, lerp), 0f);
            upgradePanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, upgradePanel.transform.localPosition.x), -800f, lerp), 25f);
            selectionPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, selectionPanel.transform.localPosition.x), -800f, lerp), 25f);

            if (Camera.main.GetComponent<CameraController>().orthographic){
                Camera.main.transform.localPosition =
                new Vector3(Mathf.Lerp(23f, 25f, lerp),
                        Camera.main.transform.localPosition.y,
                        Mathf.Lerp(-27f, -25f, lerp));
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

            mainPanel.transform.localPosition = new Vector2(Mathf.Lerp(-800f, -100f, lerp), 0f);

            if (Camera.main.GetComponent<CameraController>().orthographic){
                Camera.main.transform.localPosition =
                new Vector3(Mathf.Lerp(25f, 23f, lerp),
                        Camera.main.transform.localPosition.y,
                        Mathf.Lerp(-25f, -27f, lerp));
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

    private IEnumerator OpenUpgradeUI(){

        float lerp = 0f;

        while (lerp < 1f){

            upgradePanel.transform.localPosition = new Vector2(Mathf.Lerp(0f, 800f, lerp), 25f);
            selectionPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, selectionPanel.transform.localPosition.x), -800f, lerp), 25f);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator OpenSelectionUI(){

        float lerp = 0f;

        while (lerp < 1f){

            selectionPanel.transform.localPosition = new Vector2(Mathf.Lerp(0f, 800f, lerp), 25f);
            upgradePanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, upgradePanel.transform.localPosition.x), -800f, lerp), 25f);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    public void close(){

        StartCoroutine(CloseUI());
    }


    public void openForUpgrade(FarmBase.StructureInstance structure){

        this.structure = structure;

        setStructureInfo();
        createUpgradeButtons();
        StartCoroutine(OpenUI());
    }

    private void createUpgradeButtons(){

        foreach (Transform child in content){

            Destroy(child.gameObject);
        }

        foreach (string upgrade in FixedVariables.upgradeCategories["structure:" + structure.structureId]){

            if (!FixedVariables.maxUpgrade.ContainsKey("structure:" + structure.structureId + ":" + upgrade)){

                createSelectionButton("structure:" + structure.structureId + ":" + upgrade);
            }
            else if ((int)structure.structurePropreties[upgrade] < FixedVariables.maxUpgrade["structure:" + structure.structureId + ":" + upgrade]){
                
                createUpgradeButton(
                    "structure:" + structure.structureId + ":" + upgrade + ((int)structure.structurePropreties[upgrade] + 1));
            }
        }

    }

    private void openUpgradeProceedure(string upgradeId){

        setUpgradeInfo(upgradeId);
        presentItemRequirements(upgradeId);
        judgeIfProceedUpgrade(upgradeId);
        StartCoroutine(OpenUpgradeUI());
    }

    private void openSelectionProceedure(string selectionId){

        setSelectionInfo(selectionId);
        createSelectionButtons(selectionId);
        StartCoroutine(OpenSelectionUI());
    }

    private void createSelectionButtons(string selectionId){

        foreach (Transform button in resources){
 
            Destroy(button.gameObject);
        }

        resourceButtons = new List<ButtonLayout>();

        if (FixedVariables.complexResources[selectionId]){
            resources.GetComponent<GridLayoutGroup>().cellSize = new Vector2(375, 175);
            resources.GetComponent<GridLayoutGroup>().spacing = new Vector2(25, 25);
        }
        else{
            resources.GetComponent<GridLayoutGroup>().cellSize = new Vector2(175, 175);
            resources.GetComponent<GridLayoutGroup>().spacing = new Vector2(25, 60);
        }

        foreach (string resource in FixedVariables.resources[selectionId + structure.structurePropreties["tier"]]){

            if (GameManager.Instance.isUnlocked[resource] && (string)structure.structurePropreties["resource"] != resource){
                    
                ButtonLayout resourceButton = null;

                if (FixedVariables.complexResources[selectionId]){

                    resourceButton = Instantiate(
                    (GameObject)GameManager.Instance.getResource("general:ui:resourceSelectionButtonComplex"), resources).GetComponent<ButtonLayout>();

                    RectTransform resourcesList = resourceButton.transform.GetChild(2).GetComponent<RectTransform>();

                    foreach (Item item in FixedVariables.resourceCost[resource]){

                        ItemDisplay itemDisplay1 = Instantiate(
                            (GameObject)GameManager.Instance.getResource("general:ui:itemDisplay"), resourcesList).GetComponent<ItemDisplay>();

                        itemDisplay1.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        itemDisplay1.image.sprite =
                            GameManager.Instance.getSprite("sprites:itemIcon:" + item.itemId);
                        itemDisplay1.text.text = "" + item.itemCount;

                        int playerItemCount = GameManager.Instance.itemInventory.getItemCount(item.itemId);

                        itemDisplay1.text.color = playerItemCount < item.itemCount ? new Color(200/255f, 0f, 0f) : new Color(80/255f, 70/255f, 50/255f);
                    }
                }
                else{

                    resourceButton = Instantiate(
                    (GameObject)GameManager.Instance.getResource("general:ui:resourceSelectionButton"), resources).GetComponent<ButtonLayout>();

                    resourceButton.GetComponentInChildren<ItemDisplay>().text.text = FixedVariables.resourceCost[resource][0].itemCount.ToString();
                    resourceButton.GetComponentInChildren<ItemDisplay>().image.sprite =
                        GameManager.Instance.getSprite("sprites:itemIcon:" + FixedVariables.resourceCost[resource][0].itemId);
                }
                
                resourceButton.text.text = FixedVariables.resourceNames[resource];
                resourceButton.icon.sprite = GameManager.Instance.getSprite("sprites:resourceIcon:" + resource.Split(":")[1]);
                resourceButton.button.onClick.AddListener(delegate {setSelectionButton(resourceButton); setResourceSelection(resource);});

                resourceButtons.Add(resourceButton);
            }
        }
    }

    private void setResourceSelection(string resourceId){

        resourceDescription.text = FixedVariables.resourceDescriptions[resourceId];
        judgeIfProceedResource(resourceId);
    }

    private void judgeIfProceedResource(string resourceId){

         bool upgradable = true;

        foreach (Item item in FixedVariables.resourceCost[resourceId]){

            upgradable &= GameManager.Instance.itemInventory.getItemCount(item.itemId) >= item.itemCount;
        }

        if (upgradable){

            chooseResourceButton.interactable = true;
            chooseResourceButton.onClick.RemoveAllListeners();
            chooseResourceButton.onClick.AddListener(delegate {executeResource(resourceId);});
        }
        else{

            chooseResourceButton.interactable = false;
        }
    }

    private void executeResource(string resourceId){

        foreach (Item item in FixedVariables.resourceCost[resourceId]){

            if (item.itemId.Equals("money"))
             structure.structurePropreties["moneySpent"] = (int)structure.structurePropreties["moneySpent"] + item.itemCount;
            GameManager.Instance.itemInventory.removeItem(item);
        }

        if (resourceId.Split(":")[1] == "empty"){
            structure.structurePropreties["resource"] = null;
        }
        else{
            structure.structurePropreties["resource"] = resourceId;
        }
        structure.structurePropreties["age"] = 0f;
        structure.structurePropreties["stage"] = 0;
        if ((string)structure.structurePropreties["currentInteraction"] == "harvestPlant"){
            structure.structurePropreties["currentInteraction"] = null;
        }
        
        switch (structure.structureId){

            case "farmland" :

                Farm.Instance.getStructureRenderer(structure).GetComponent<FarmlandRenderer>().deepUpdateStructure();

            break;
        }

        UIController.Instance.moneyDisplayHUD.updateMoneyDisplay();
        StartCoroutine(CloseUI());
    }

    private void setSelectionButton(ButtonLayout button){

        foreach (ButtonLayout buttonL in resourceButtons){

            if (buttonL != button && buttonL.button.interactable == false){

                buttonL.button.interactable = true;
                buttonL.transform.GetComponent<CanvasGroup>().alpha = 1;
            }
        }

        button.button.interactable = false;
        button.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void setSelectionInfo(string selectionId){

        resourceIcon.sprite = GameManager.Instance.getSprite(FixedVariables.upgradeIcons[selectionId]);
        resourceName.text = FixedVariables.upgradeNames[selectionId];
        resourceDescription.text = FixedVariables.upgradeDescriptions[selectionId];
    }

    private void judgeIfProceedUpgrade(string upgradeId){

        bool upgradable = true;

        foreach (Item item in FixedVariables.upgradeRequirements[upgradeId]){

            upgradable &= GameManager.Instance.itemInventory.getItemCount(item.itemId) >= item.itemCount;
        }

        if (upgradable){

            proceedButton.interactable = true;
            proceedButton.onClick.RemoveAllListeners();
            proceedButton.onClick.AddListener(delegate {executeUpgrade(upgradeId);});
        }
        else{

            proceedButton.interactable = false;
        }
    }

    private void executeUpgrade(string upgradeId){

        foreach (Item item in FixedVariables.upgradeRequirements[upgradeId]){

            if (item.itemId.Equals("money"))
             structure.structurePropreties["moneySpent"] = (int)structure.structurePropreties["moneySpent"] + item.itemCount;
            GameManager.Instance.itemInventory.removeItem(item);
        }

        if ((string)structure.structurePropreties["currentInteraction"] == "harvestPlant"){
            structure.structurePropreties["currentInteraction"] = null;
        }
        structure.structurePropreties["age"] = 0f;
        structure.structurePropreties["stage"] = 0;
        
        structure.structurePropreties["maintenenceTime"] = (float)FixedVariables.upgradeTimes[upgradeId];
        structure.structurePropreties["currentlyUpgrading"] = upgradeId.Split(":")[2];
        
        switch (structure.structureId){

            case "farmland" :

                Farm.Instance.getStructureRenderer(structure).GetComponent<FarmlandRenderer>().deepUpdateStructure();

            break;
        }

        UIController.Instance.moneyDisplayHUD.updateMoneyDisplay();
        StartCoroutine(CloseUI());
    }
    private void presentItemRequirements(string upgradeId){

        foreach (Transform child in itemsRequired){
            foreach (Transform leaf in child){
                Destroy(leaf.gameObject);
            }
            Destroy(child.gameObject);
        }

        foreach (Transform child in itemsOwned){
            foreach (Transform leaf in child){
                Destroy(leaf.gameObject);
            }
            Destroy(child.gameObject);
        }

        foreach (Item item in FixedVariables.upgradeRequirements[upgradeId]){

            ItemDisplay itemDisplay1 =
                Instantiate(
                (GameObject)GameManager.Instance.getResource("general:ui:itemDisplay"), itemsRequired
                            ).GetComponent<ItemDisplay>();

            itemDisplay1.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            itemDisplay1.image.sprite =
                GameManager.Instance.getSprite("sprites:itemIcon:" + item.itemId);
            itemDisplay1.text.text = "" + item.itemCount;

            int playerItemCount = GameManager.Instance.itemInventory.getItemCount(item.itemId);

            ItemDisplay itemDisplay2 =
                Instantiate(
                (GameObject)GameManager.Instance.getResource("general:ui:itemDisplay"), itemsOwned
                            ).GetComponent<ItemDisplay>();

            itemDisplay2.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            itemDisplay2.image.sprite =
                GameManager.Instance.getSprite("sprites:itemIcon:" + item.itemId);
            itemDisplay2.text.text = "" + playerItemCount;
            itemDisplay2.text.color = playerItemCount < item.itemCount ? new Color(200/255f, 0f, 0f) : new Color(80/255f, 70/255f, 50/255f);
        }
    }

    private void setUpgradeInfo(string upgradeId){

        upgradeIcon.sprite = GameManager.Instance.getSprite(FixedVariables.upgradeIcons[upgradeId]);
        upgradeName.text = FixedVariables.upgradeNames[upgradeId];
        upgradeDescription.text = FixedVariables.upgradeDescriptions[upgradeId];
    }

    private void createSelectionButton(string selectionId){

        ButtonLayout selectionButton =
            Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:upgradeButton"), content.transform
                        ).GetComponent<ButtonLayout>();

        selectionButton.icon.sprite = GameManager.Instance.getSprite(FixedVariables.upgradeIcons[selectionId]);
        selectionButton.text.text = FixedVariables.upgradeNames[selectionId];
        selectionButton.button.onClick.AddListener(delegate {openSelectionProceedure(selectionId);});
    }
    private void createUpgradeButton(string upgradeId){

        ButtonLayout upgradeButton =
            Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:upgradeButton"), content.transform
                        ).GetComponent<ButtonLayout>();

        upgradeButton.icon.sprite = GameManager.Instance.getSprite(FixedVariables.upgradeIcons[upgradeId]);
        upgradeButton.text.text = FixedVariables.upgradeNames[upgradeId];
        upgradeButton.button.onClick.AddListener(delegate {openUpgradeProceedure(upgradeId);});
    }

    private void setStructureInfo(){

        structureName.text = FarmBase.structureNames[structure.structureId];
        structureIcon.sprite = GameManager.Instance.getSprite(string.Format("sprites:structureIcon:{0}", structure.structureId));
    }
}
