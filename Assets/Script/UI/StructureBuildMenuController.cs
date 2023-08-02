using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StructureBuildMenuController : MonoBehaviour
{
    [Header("Main elements")]
    public RectTransform mainPanel;
    public RectTransform buildPanel;
    public RectTransform decorPanel;
    public CanvasGroup canvasGroup;
    public Button closeUIButton;

    [Header("Main panel")]
    public Image icon;
    public TextMeshProUGUI title;
    public RectTransform content;
    
    [Header("BuildProceedWindow")]
    public TextMeshProUGUI structureName;
    public Image structureIcon;
    public TextMeshProUGUI structureDescription;
    public RectTransform itemsRequired;
    public RectTransform itemsOwned;
    public Button proceedButton;

    [Header("DecorSelectionWindow")]
    public TextMeshProUGUI decorName;
    public Image decorIcon;
    public TextMeshProUGUI decorDescription;
    public RectTransform decorItems;
    private List<ButtonLayout> decorButtons = new List<ButtonLayout>();
    public Button chooseDecorButton;

    private BuildPlan buildPlan;
    private bool selectionLast = false;

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
            buildPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, buildPanel.transform.localPosition.x), -800f, lerp), 25f);
            decorPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, buildPanel.transform.localPosition.x), -800f, lerp), 25f);

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

    private IEnumerator OpenSelectionUI(){

        float lerp = 0f;

        while (lerp < 1f){

            decorPanel.transform.localPosition = new Vector2(Mathf.Lerp(0f, 800f, lerp), 25f);
            buildPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, buildPanel.transform.localPosition.x), -800f, lerp), 25f);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator planUI(bool free){

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float lerp = 0f;

        while (lerp < 1f){

            mainPanel.transform.localPosition = new Vector2(Mathf.Lerp(-100f, -800f, lerp), 0f);
            buildPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, buildPanel.transform.localPosition.x), -800f, lerp), 25f);
            decorPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, buildPanel.transform.localPosition.x), -800f, lerp), 25f);

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

        yield return StartCoroutine(UIController.Instance.buildPlanHUD.OpenElement(free));
    }

    private IEnumerator CancelPlanUI(){

        yield return StartCoroutine(UIController.Instance.buildPlanHUD.CloseElement());

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        float lerp = 0f;

        while (lerp < 1f){

            mainPanel.transform.localPosition = new Vector2(Mathf.Lerp(-800f, -100f, lerp), 0f);
            if (selectionLast) {decorPanel.transform.localPosition = new Vector2(Mathf.Lerp(-800f, 800f, lerp), 25f);}
            else {buildPanel.transform.localPosition = new Vector2(Mathf.Lerp(-800f, 800f, lerp), 25f);}

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

    private IEnumerator OpenBuildUI(){

        float lerp = 0f;

        while (lerp < 1f){

            buildPanel.transform.localPosition = new Vector2(Mathf.Lerp(0f, 800f, lerp), 25f);
            decorPanel.transform.localPosition =
             new Vector2(Mathf.Lerp(Mathf.Min(800f, decorPanel.transform.localPosition.x), -800f, lerp), 25f);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    public void close(){

        StartCoroutine(CloseUI());
    }


    public void openForBuild(){

        createBuildButtons();
        setBuildMenu();
        StartCoroutine(OpenUI());
    }

    public void setBuildMenu(){

        icon.sprite = GameManager.Instance.getSprite("sprites:buttonIcons:build");
        title.text = "Buildings";
    }

    public void openForDestroy(){
        
        GameManager.Instance.changeState("destroyMode");
        StartCoroutine(UIController.Instance.buildPlanHUD.OpenElement(true));
        UIController.Instance.buildPlanHUD.goBack.onClick.RemoveAllListeners();
        UIController.Instance.buildPlanHUD.goBack.onClick.AddListener(delegate {exitDestroyMode();});
    }

    public void exitDestroyMode(){

        GameManager.Instance.changeState("farmRoaming");
        StartCoroutine(UIController.Instance.buildPlanHUD.CloseElement());
    }

    public void askForDestruction(FarmBase.StructureInstance structure){

        if (structure != null){

            StartCoroutine(UIController.Instance.buildPlanHUD.CloseElement());
            UIController.Instance.textPrompt.ToggleElement(true);
            UIController.Instance.textPrompt.promptText.text = "Do you really want to destroy this building?";
            UIController.Instance.textPrompt.cancelButton.onClick.RemoveAllListeners();
            UIController.Instance.textPrompt.cancelButton.onClick.AddListener(delegate {
                UIController.Instance.textPrompt.ToggleElement(false);
                StartCoroutine(UIController.Instance.buildPlanHUD.OpenElement(true));});
            UIController.Instance.textPrompt.confirmButton.onClick.RemoveAllListeners();
            UIController.Instance.textPrompt.confirmButton.onClick.AddListener(delegate {
                UIController.Instance.textPrompt.ToggleElement(false);
                Farm.Instance.destroyStructure(structure);
                GameManager.Instance.changeState("farmRoaming");});
        }
    }

    public void openForMove(){

    }

    private void createBuildButtons(){

        foreach (Transform child in content){

            Destroy(child.gameObject);
        }

        foreach (string structure in FixedVariables.structureBuildList){

            createStructureButton(structure);
        }

        foreach (string decor in FixedVariables.decorBuildList){

            createDecorButton(decor);
        }

    }

    private void openBuildProceedure(string structureId){

        setBuildInfo(structureId);
        presentItemRequirements(structureId);
        judgeIfProceedBuild(structureId);
        StartCoroutine(OpenBuildUI());
    }

    private void openDecorProceedure(string decorId){

        setDecorInfo(decorId);
        createDecorButtons(decorId);
        StartCoroutine(OpenSelectionUI());
    }

    private void createDecorButtons(string decorId){

        foreach (Transform button in decorItems){
 
            Destroy(button.gameObject);
        }

        decorButtons = new List<ButtonLayout>();

         if (FixedVariables.complexResources[decorId]){
            decorItems.GetComponent<GridLayoutGroup>().cellSize = new Vector2(375, 175);
            decorItems.GetComponent<GridLayoutGroup>().spacing = new Vector2(25, 25);
        }
        else{
            decorItems.GetComponent<GridLayoutGroup>().cellSize = new Vector2(175, 175);
            decorItems.GetComponent<GridLayoutGroup>().spacing = new Vector2(25, 60);
        }

        foreach (string decor in FixedVariables.decorations[decorId]){

            if (GameManager.Instance.isUnlocked[decor]){

                ButtonLayout decorButton = null;

                if (FixedVariables.complexResources[decorId]){

                    decorButton = Instantiate(
                    (GameObject)GameManager.Instance.getResource("general:ui:resourceSelectionButtonComplex"), decorItems).GetComponent<ButtonLayout>();

                    RectTransform resourcesList = decorButton.transform.GetChild(2).GetComponent<RectTransform>();

                    foreach (Item item in FixedVariables.decorCost[decor]){

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

                    decorButton = Instantiate(
                    (GameObject)GameManager.Instance.getResource("general:ui:resourceSelectionButton"), decorItems).GetComponent<ButtonLayout>();

                    decorButton.GetComponentInChildren<ItemDisplay>().text.text = FixedVariables.decorCost[decor][0].itemCount.ToString();
                    decorButton.GetComponentInChildren<ItemDisplay>().image.sprite =
                        GameManager.Instance.getSprite("sprites:itemIcon:" + FixedVariables.decorCost[decor][0].itemId);
                }
                
                decorButton.text.text = FixedVariables.decorNames[decor];
                //decorButton.icon.sprite = GameManager.Instance.getSprite("sprites:resourceIcon:" + decor.Split(":")[1]);
                decorButton.button.onClick.AddListener(delegate {setDecorButton(decorButton); setDecorSelection(decor);});

                decorButtons.Add(decorButton);
            }
        }
    }

    private void setDecorSelection(string decorId){

        decorDescription.text = FixedVariables.decorDescriptions[decorId];

        bool upgradable = true;

        foreach (Item item in FixedVariables.decorCost[decorId]){

            upgradable &= GameManager.Instance.itemInventory.getItemCount(item.itemId) >= item.itemCount;
        }

        if (upgradable){

            chooseDecorButton.interactable = true;
            chooseDecorButton.onClick.RemoveAllListeners();
            if (decorId.Split(":")[0] == "tile"){ chooseDecorButton.onClick.AddListener(delegate {selectionLast = true; enterTilePlanMode(decorId);}); }
            else{ chooseDecorButton.onClick.AddListener(delegate {selectionLast = true;
             enterStructurePlanMode(FarmBase.createStructureInstance(decorId.Split(":")[0], new int[] {0, 0}, decorId));}); }
        }
        else{

            chooseDecorButton.interactable = false;
        }
    }

    private void setDecorButton(ButtonLayout button){

        foreach (ButtonLayout buttonL in decorButtons){

            if (buttonL != button && buttonL.button.interactable == false){

                buttonL.button.interactable = true;
                buttonL.transform.GetComponent<CanvasGroup>().alpha = 1;
            }
        }

        button.button.interactable = false;
        button.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void setDecorInfo(string decorId){

        //decorIcon.sprite = GameManager.Instance.getSprite(FixedVariables.decorIcons[decorId]);
        decorName.text = FixedVariables.decorNames[decorId];
        decorDescription.text = FixedVariables.decorDescriptions[decorId];
    }

    private void judgeIfProceedBuild(string structureId){

        bool buildable = true;

        foreach (Item item in FixedVariables.structureBuildRequirements[structureId]){

            buildable &= GameManager.Instance.itemInventory.getItemCount(item.itemId) >= item.itemCount;
        }

        if (buildable){

            proceedButton.interactable = true;
            proceedButton.onClick.AddListener(delegate {selectionLast = false; enterStructurePlanMode(FarmBase.createStructureInstance(structureId, new int[] {0, 0}));});
        }
        else{

            proceedButton.interactable = false;
        }
    }

    private void enterStructurePlanMode(FarmBase.StructureInstance structure){

        buildPlan = Instantiate((GameObject)GameManager.Instance.getResource("planning:farm:buildplan")).GetComponent<BuildPlan>();
        buildPlan.transform.position = new Vector3(-20f, 0, -20f);
        buildPlan.setStructure(structure);
        setPlanMode(false);
    }

    private void enterTilePlanMode(string tileId){

        buildPlan = Instantiate((GameObject)GameManager.Instance.getResource("planning:farm:buildplan")).GetComponent<BuildPlan>();
        buildPlan.transform.position = new Vector3(-20f, 0, -20f);
        buildPlan.setTile(new FarmBase.TileInstance(tileId.Remove(0, 5), new Vector2(0, 0)));
        setPlanMode(true);
    }
    
    private void setPlanMode(bool free){

        StartCoroutine(planUI(free));
        if (free){
            UIController.Instance.buildPlanHUD.goBack.onClick.RemoveAllListeners();
            UIController.Instance.buildPlanHUD.goBack.onClick.AddListener(delegate {exitPlanMode();});
            GameManager.Instance.changeState("freeBuildPlan");
        }
        else{
            UIController.Instance.buildPlanHUD.cancel.onClick.RemoveAllListeners();
            UIController.Instance.buildPlanHUD.cancel.onClick.AddListener(delegate {exitPlanMode();});
            UIController.Instance.buildPlanHUD.confirm.interactable = false;
            UIController.Instance.buildPlanHUD.confirm.onClick.RemoveAllListeners();
            UIController.Instance.buildPlanHUD.confirm.onClick.AddListener(delegate {concludePlan();});
            GameManager.Instance.changeState("preciseBuildPlan");
        }
    }

    private void exitPlanMode(){

        buildPlan.Destroy();
        StartCoroutine(CancelPlanUI());
        GameManager.Instance.changeState("farmPrompt");
    }

    private void concludePlan(){

        foreach (Item item in getBuildRequirements()){

            GameManager.Instance.itemInventory.removeItem(item);
        }
        UIController.Instance.moneyDisplayHUD.updateMoneyDisplay();
        buildThePlan();
        buildPlan.Destroy();
        StartCoroutine(UIController.Instance.buildPlanHUD.CloseElement());
        GameManager.Instance.changeState("farmRoaming");
    }

    public void updateBuildPlanPos(RaycastHit hit){
        
        Vector3 position = hit.point;
        buildPlan.updatePositioning(new Vector2(position.x, position.z));
        if (canFitPlan()){
             UIController.Instance.buildPlanHUD.confirm.interactable = true;
        }
        else{ UIController.Instance.buildPlanHUD.confirm.interactable = false; }
    }

    public void updateBuildPlanFree(RaycastHit hit){
        
        Vector3 position = hit.point;
        buildPlan.updatePositioning(new Vector2(position.x, position.z));
        if (canFitPlan()){
            buildThePlan();
            foreach (Item item in getBuildRequirements()){
            GameManager.Instance.itemInventory.removeItem(item);
            }
            UIController.Instance.moneyDisplayHUD.updateMoneyDisplay();
        }
    }

    private Item[] getBuildRequirements(){

        if (buildPlan.structure != null && !FixedVariables.isDecor[buildPlan.structure.structureId]){

            return FixedVariables.structureBuildRequirements[buildPlan.structure.structureId];
        }
        else if (buildPlan.tile != null){
            return FixedVariables.decorCost[string.Format("tile:{0}", buildPlan.tile.tileId)];
        }
        else {
            return FixedVariables.decorCost[(string)buildPlan.structure.structurePropreties["resource"]];
        }
    }

    private void buildThePlan(){

        if (buildPlan.structure != null){
            Farm.Instance.insertStructure(buildPlan.structure);
        }
        else if (buildPlan.tile != null){
            Farm.Instance.insertTile(buildPlan.tile);
        }
    }

    private bool canFitPlan(){

        if (buildPlan.structure != null){
            return Farm.Instance.farmLayout.canFit(buildPlan.structure);
        }
        else if (buildPlan.tile != null){
            return Farm.Instance.farmLayout.canFitTile(buildPlan.tile);
        }
        return false;
    }

    private void presentItemRequirements(string structureId){

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

        foreach (Item item in FixedVariables.structureBuildRequirements[structureId]){

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

    private void setBuildInfo(string structureId){

        structureIcon.sprite = GameManager.Instance.getSprite(FixedVariables.structureIcons[structureId]);
        structureName.text = FixedVariables.structureNames[structureId];
        structureDescription.text = FixedVariables.structureDescriptions[structureId];
    }

    private void createStructureButton(string structureId){

        ButtonLayout structureButton =
            Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:upgradeButton"), content.transform
                        ).GetComponent<ButtonLayout>();

        structureButton.icon.sprite = GameManager.Instance.getSprite(FixedVariables.structureIcons[structureId]);
        structureButton.text.text = FixedVariables.structureNames[structureId];
        structureButton.button.onClick.AddListener(delegate {openBuildProceedure(structureId);});
    }

    private void createDecorButton(string decorId){

        ButtonLayout selectionButton =
            Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:upgradeButton"), content.transform
                        ).GetComponent<ButtonLayout>();

        //selectionButton.icon.sprite = GameManager.Instance.getSprite(FixedVariables.decorIcons[decorId]);
        selectionButton.text.text = FixedVariables.decorNames[decorId];
        selectionButton.button.onClick.AddListener(delegate {openDecorProceedure(decorId);});
    }
}

