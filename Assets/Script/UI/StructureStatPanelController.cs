using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StructureStatPanelController : MonoBehaviour
{
    [Header("Main elements")]
    public RectTransform mainPanel;
    public CanvasGroup canvasGroup;
    public Button closeUIButton;

    [Header("Structure Info")]
    public Image structureIcon;
    public TextMeshProUGUI structureName;

    [Header("Time bar")]
    public TextMeshProUGUI timeLabel;
    public TimeleftBar timeBar;
    public TextMeshProUGUI timeText;

    [Header("Other statistics")]
    public ItemDisplay moneySpent;
    public ItemDisplayContainer resourcesProduced;

    public FarmBase.StructureInstance structure;
    private Coroutine updater;

    void Start(){
        
        closeUIButton.onClick.AddListener(delegate { close(); });
    }

    private void setStructureInfo(){

        structureName.text = FarmBase.structureNames[structure.structureId];
        structureIcon.sprite = GameManager.Instance.getSprite(string.Format("sprites:structureIcon:{0}", structure.structureId));
    }

    private void openUI(){

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GameManager.Instance.changeState("farmPrompt");

        StartCoroutine(UIUtils.movePanel(mainPanel, mainPanel.localPosition, new Vector2(0, -25)));
    }

    private void closeUI(){

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        GameManager.Instance.changeState("farmRoaming");

        StartCoroutine(UIUtils.movePanel(mainPanel, mainPanel.localPosition, new Vector2(0, -1200)));
    }

    public void openAndSet(FarmBase.StructureInstance structure){

        this.structure = structure;
        setStructureInfo();
        setTimeStats();
        setOtherStats();
        openUI();
    }

    public void close(){

        if (updater != null) StopCoroutine(updater);
        closeUI();
    }

    public IEnumerator updateCycleUpgrade(){

        while (true){

            timeBar.setPercentage(1f - ((float)structure.structurePropreties["maintenenceTime"] /
            FixedVariables.upgradeTimes[string.Format("structure:{0}:{1}",structure.structureId, structure.structurePropreties["currentlyUpgrading"])]));

            timeText.text = TimeleftBar.toTime(Mathf.Max(0,Mathf.RoundToInt((float)structure.structurePropreties["maintenenceTime"])));

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator updateCycleHarvest(){

        while (true){

            int currentStage = (int)structure.structurePropreties["stage"];

            while(currentStage == (int)structure.structurePropreties["stage"]){

                float age = (float)structure.structurePropreties["age"];
                float maxAge = FixedVariables.resourceProductionTimes[string.Format("{0}{1}",
                structure.structurePropreties["resource"],structure.structurePropreties["stage"])];
                string speedUpgradeId = FixedVariables.speedUpgradeId[string.Format("structure:{0}",structure.structureId)];

                timeBar.setPercentage(age / maxAge);

                timeText.text = TimeleftBar.toTime(Mathf.Max(0, Mathf.RoundToInt((maxAge - age) /
                FixedVariables.harvestSpeed[string.Format("{0}:{1}{2}",structure.structureId,speedUpgradeId,structure.structurePropreties[speedUpgradeId])])));

                yield return new WaitForEndOfFrame();
            }

            setTimeStats();
        }
    }

    private void setTimeStats(){

        if (structure.structurePropreties["currentlyUpgrading"] == null){
            if (structure.structurePropreties["resource"] != null){
                if (FixedVariables.resourceFinalStage[(string)structure.structurePropreties["resource"]] == (int)structure.structurePropreties["stage"]){
                    timeLabel.text = "Time until next harvest:";}
                else{ timeLabel.text = "Time until next stage:";}
                timeBar.setColor(new Color(0.67f, 0.88f, 0.42f));
                updater = StartCoroutine(updateCycleHarvest());
            }
            else{
                timeLabel.text = "No crops planted";
                timeBar.setColor(new Color(0.67f, 0.88f, 0.42f));
                timeBar.setPercentage(0f);
                timeText.text = "-";
            }
        }
        else{
            timeLabel.text = "Time until upgrade finish:";
            timeBar.setColor(new Color(0.42f, 0.82f, 0.88f));
            updater = StartCoroutine(updateCycleUpgrade());
        }
    }

    private void setOtherStats(){

        moneySpent.text.text = structure.structurePropreties["moneySpent"].ToString();
        resourcesProduced.setItems(((Inventory)structure.structurePropreties["harvested"]).items.Values);
    }
}
