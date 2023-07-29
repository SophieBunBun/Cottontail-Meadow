using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceRenderer : MonoBehaviour
{
    public FarmBase.StructureInstance farmStructure;

    public StatisticsSign statisticsSign;
    public GameObject furnaceTexture;

    private GameObject maintnenceSign;
    private GameObject interactionAnouncement;

    public void deepUpdateStructure(){

        statisticsSign.structure = farmStructure;

        //Setting anchor location
        transform.position = new Vector3((farmStructure.anchorLocation[0] + FarmBase.structureSize[farmStructure.structureId][0] / 2f) * 2f ,0.01f ,
                                    (farmStructure.anchorLocation[1] + FarmBase.structureSize[farmStructure.structureId][1] / 2f) * 2f);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);

        //Destroying previous farm
        Destroy(maintnenceSign); //Add smoke poof for sign disapearing

        if (farmStructure.structurePropreties["currentlyUpgrading"] != null)
        {
            maintnenceSign = Instantiate((GameObject)GameManager.Instance.getResource("structures:farmland:maintnencesign"), transform);
            maintnenceSign.transform.localPosition = new Vector3(-1.8f, 1, 2.4f);
        }

        if (interactionAnouncement != null){

            foreach (Transform child in interactionAnouncement.transform){
                foreach (Transform leafChild in child){
                    Destroy(leafChild.gameObject);
                }
                Destroy(child.gameObject);
            }
            Destroy(interactionAnouncement);
        }

        //Initiating farmland
        if ((string)farmStructure.structurePropreties["currentlyUpgrading"] == "tierUpgrade")
        {
            furnaceTexture.GetComponent<MeshRenderer>().material =
                (Material)GameManager.Instance.getResource(string.Format("structures:furnace:{0}build", farmStructure.structurePropreties["tier"].ToString()));
        }
        else if ((string) farmStructure.structurePropreties["resource"] != null || farmStructure.structurePropreties["currentInteraction"] != null)
        {
            furnaceTexture.GetComponent<MeshRenderer>().material =
                (Material)GameManager.Instance.getResource(string.Format("structures:furnace:{0}unlit", farmStructure.structurePropreties["tier"].ToString()));
        }
        else
        {
            furnaceTexture.GetComponent<MeshRenderer>().material =
                (Material)GameManager.Instance.getResource(string.Format("structures:furnace:{0}lit", farmStructure.structurePropreties["tier"].ToString()));
        }

        //Initiating interactAnouncement
        if (farmStructure.structurePropreties["currentInteraction"] != null){

            anounceInteraction();
        }

        //Initiating construction sign
        if (farmStructure.structurePropreties["currentlyUpgrading"] != null)
        {
            maintnenceSign = Instantiate((GameObject)GameManager.Instance.getResource("structures:farmland:maintnencesign"), transform);
            maintnenceSign.transform.localPosition = new Vector3(-2.35f, 1, 4f);
        }
    }

    public void anounceInteraction(){

        interactionAnouncement = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:empty"), transform);
        GameObject hudPopUp = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:upDownBobPointer"), interactionAnouncement.transform);
        hudPopUp.GetComponentInChildren<SpriteRenderer>().sprite =
         GameManager.Instance.getSprite(FixedVariables.interactionIcons["farmland:" + farmStructure.structurePropreties["currentInteraction"]]);
        hudPopUp.transform.localPosition = new Vector3 (0, 2, 0);
    }

    public void collectCrop(){

        GameObject collectIcon = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:itemPickup"), transform);
        collectIcon.transform.localPosition = new Vector3(0f, 0f, 0f);
        collectIcon.GetComponent<ItemPickupAnim>().spriteRenderer.sprite =
            GameManager.Instance.getSprite("sprites:itemIcon:" + ((string)farmStructure.structurePropreties["resource"]).Split(":")[1]);
        
        int harvestAmount = (int)Mathf.Round(FixedVariables.harvestCount["farmland:quantity" + farmStructure.structurePropreties["quantity"]]
                            * (((float)farmStructure.structurePropreties["hydration"] / 2f) + 0.5f));
        Item harvested = new Item(((string)farmStructure.structurePropreties["resource"]).Split(":")[1], harvestAmount);

        ((Inventory)farmStructure.structurePropreties["harvested"]).addItem(harvested);
        GameManager.Instance.itemInventory.addItem(harvested);
    }
}
