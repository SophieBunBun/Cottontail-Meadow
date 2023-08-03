using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI elements")]
    public InventoryMenuController inventoryMenu;
    public StructureUpgradeMenuController upgradeMenu;
    public StructureBuildMenuController buildMenu;
    public StructureStatPanelController statsPanel;

    [Header("Hud elements")]
    public MoneyDisplayHUD moneyDisplayHUD;
    public CanvasGroup toolHud;
    public Button openInventoryButtonHUD;
    public BuildPlanHUD buildPlanHUD;
    public TextPrompt textPrompt;
    public InteractionProgressBar interactBar;

    void Awake()
    {
        Instance = this;
        openInventoryButtonHUD.onClick.AddListener(delegate {openInventory();});
    }

    public void OpenBuildMenu(){

        buildMenu.openForBuild();
    }

    public void OpenMoveMenu(){

        buildMenu.openForMove();
    }

    public void OpenDestroyMenu(){

        buildMenu.openForDestroy();
    }

    public void openInventory(){

        inventoryMenu.openInventory();
    }

    private void enableCanvasGroup(CanvasGroup cv){

        cv.alpha = 1;
        cv.interactable = true;
        cv.blocksRaycasts = true;
    }

    private void fadeCanvasGroup(CanvasGroup cv){

        cv.alpha = 0.5f;
        cv.interactable = false;
        cv.blocksRaycasts = false;
    }

    private void hideCanvasGroup(CanvasGroup cv){

        cv.alpha = 0f;
        cv.interactable = false;
        cv.blocksRaycasts = false;
    }

    public void farmPromptMode(){

        enableCanvasGroup(moneyDisplayHUD.GetComponent<CanvasGroup>());
        fadeCanvasGroup(toolHud);
        fadeCanvasGroup(openInventoryButtonHUD.GetComponent<CanvasGroup>());
    }

    public void farmRoamingMode(){

        enableCanvasGroup(moneyDisplayHUD.GetComponent<CanvasGroup>());
        enableCanvasGroup(toolHud);
        enableCanvasGroup(openInventoryButtonHUD.GetComponent<CanvasGroup>());
    }

    public void buildPlanMode(){

        hideCanvasGroup(moneyDisplayHUD.GetComponent<CanvasGroup>());
        hideCanvasGroup(toolHud);
        hideCanvasGroup(openInventoryButtonHUD.GetComponent<CanvasGroup>());
    }
}
