using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI elements")]
    public StructureUpgradeMenuController upgradeMenu;
    public StructureBuildMenuController buildMenu;
    public StructureStatPanelController statsPanel;

    [Header("Hud elements")]
    public MoneyDisplayHUD moneyDisplayHUD;
    public BuildStructureButtonHUD buildStructureButtonHUD;
    public BuildPlanHUD buildPlanHUD;

    void Awake()
    {
        Instance = this;
    }

    public void OpenBuildMenu(){

        buildMenu.openForBuild();
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
        fadeCanvasGroup(buildStructureButtonHUD.GetComponent<CanvasGroup>());
    }

    public void farmRoamingMode(){

        enableCanvasGroup(moneyDisplayHUD.GetComponent<CanvasGroup>());
        enableCanvasGroup(buildStructureButtonHUD.GetComponent<CanvasGroup>());
    }

    public void buildPlanMode(){

        hideCanvasGroup(moneyDisplayHUD.GetComponent<CanvasGroup>());
        hideCanvasGroup(buildStructureButtonHUD.GetComponent<CanvasGroup>());
    }
}
