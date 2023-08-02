using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBedRenderer : MonoBehaviour
{
    public FarmBase.StructureInstance farmStructure;

    private GameObject flowerBed;
    private GameObject maintnenceSign;
    private GameObject statisticsSign;
    private GameObject nodes;
    private GameObject fence;
    private GameObject interactionAnouncement;
    private List<GameObject> emptyCropNodes;
    private List<GameObject> fullCropNodes;
    private List<GameObject> cropNodes;

    public void deepUpdateStructure(){

        //Setting anchor location
        transform.position = new Vector3((farmStructure.anchorLocation[0] + FarmBase.structureSize[farmStructure.structureId][0] / 2f) * 2f ,0.01f ,
                                    (farmStructure.anchorLocation[1] + FarmBase.structureSize[farmStructure.structureId][1] / 2f) * 2f);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);

        //Destroying previous farm
        Destroy(flowerBed);
        Destroy(maintnenceSign); //Add smoke poof for sign disapearing

        if (nodes != null && (int)farmStructure.structurePropreties["stage"] == 0){
            foreach (Transform child in nodes.transform){
                foreach (Transform leafChild in child){
                    Destroy(leafChild.gameObject);
                }
                Destroy(child.gameObject);
            }
            Destroy(nodes);
            nodes = null;
        }

        if (statisticsSign == null)
        {
            statisticsSign = Instantiate((GameObject)GameManager.Instance.getResource("structures:farmland:statisticsSign"), transform);
            statisticsSign.transform.localPosition = new Vector3(1.4f, 1, 2.3f);
            statisticsSign.GetComponent<StatisticsSign>().structure = this.farmStructure;

        }

        if (fence == null){
            fence = Instantiate((GameObject)GameManager.Instance.getResource("structures:flowerbed:fence"), transform);
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
        if ((string)farmStructure.structurePropreties["currentlyUpgrading"] == "tier0")
        {
            flowerBed = Instantiate((GameObject)GameManager.Instance.getResource("structures:flowerbed:flowerbed0"), transform);
        }
        else
        {
            flowerBed = Instantiate((GameObject)GameManager.Instance.getResource("structures:flowerbed:flowerbed1"), transform);
        }

        //Initiating interactAnouncement
        if (farmStructure.structurePropreties["currentInteraction"] != null){

            anounceInteraction();
        }

        //Initiating construction sign
        if (farmStructure.structurePropreties["currentlyUpgrading"] != null)
        {
            maintnenceSign = Instantiate((GameObject)GameManager.Instance.getResource("structures:farmland:maintnencesign"), transform);
            maintnenceSign.transform.localPosition = new Vector3(-1.175f, 1, 2.3f);
        }
        //Initiating crops
        else if (nodes == null && farmStructure.structurePropreties["resource"] != null)
        {
            nodes = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:empty"), transform);
            nodes.name = "Crops";

            cropNodes = new List<GameObject>();

            emptyCropNodes = new List<GameObject>();
            fullCropNodes = new List<GameObject>();

            createCropLayout();
        }
    }

    private void createCropLayout(){

        if ((int)farmStructure.structurePropreties["quantity"] == 0){

            for (int x = 0; x < 3; x++){
                for (int y = 0; y < 5; y++){

                    if ( 1.25f - (2.3f) * y + (2.3f / 4) * (x % 2) <= 1.25f && 1.25f - (2.3f) * y + (2.3f / 2) * (x % 2) >= -1.25f){
                        insertCrop(1.35f - (1.35f) * x + Random.Range(-0.1f, 0.1f), 1.25f - (2.3f) * y + (2.3f / 2) * (x % 2) + Random.Range(-0.1f, 0.1f));
                    }
                }
            }
        }
        else if ((int)farmStructure.structurePropreties["quantity"] == 1){

            for (int x = 0; x < 5; x++){
                for (int y = 0; y < 7; y++){

                    if ( 1.25f - (2.3f / 2) * y + (2.3f / 4) * (x % 2) <= 1.25f && 1.25f - (2.3f / 2) * y + (2.3f / 4) * (x % 2) >= -1.25f){
                        insertCrop(1.35f - (1.35f) * x + Random.Range(-0.1f, 0.1f), 1.25f - (2.3f / 2) * y + (2.3f / 4) * (x % 2) + Random.Range(-0.1f, 0.1f));
                    }
                }
            }
        }
        else if ((int)farmStructure.structurePropreties["quantity"] == 2){

            for (int x = 0; x < 5; x++){
                for (int y = 0; y < 9; y++){

                    if ( 1.25f - (2.3f / 3) * y + (2.3f / 6) * (x % 2) <= 1.25f && 1.25f - (2.3f / 3) * y + (2.3f / 6) * (x % 2) >= -1.25f){
                        insertCrop(1.35f - (1.35f) * x + Random.Range(-0.1f, 0.1f), 1.25f - (2.3f / 3) * y + (2.3f / 6) * (x % 2) + Random.Range(-0.1f, 0.1f));
                    }
                }
            }
        }
    }

    private void insertCrop(float x, float y){

        GameObject empty = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:empty"), nodes.transform);
        empty.transform.localPosition = new Vector3(x, 0f, y);
        empty.transform.localScale = new Vector3(0.8f, 0.95f, 0.8f);
        Instantiate((GameObject)GameManager.Instance.getResource(
            "structures:flowerbed:" + ((string)farmStructure.structurePropreties["resource"]).Split(":")[1]), empty.transform);
            
        cropNodes.Add(empty);
    }

    public void updateStructure(){

        if (farmStructure.structurePropreties["resource"] != null && (int)(float)farmStructure.structurePropreties["age"] != 0)
        {
            foreach (GameObject node in cropNodes){
            node.transform.GetChild(0).GetChild(
                (int)farmStructure.structurePropreties["stage"]).GetComponent<Animator>().Play(
                "Grow", 0,Mathf.Min(0.99f, (float)farmStructure.structurePropreties["age"]
                /
                FixedVariables.proccessTimes[(string)farmStructure.structurePropreties["resource"] +
                (int)farmStructure.structurePropreties["stage"]]));
            }
        }
    }

    public void destroyStructure(){
        Destroy(maintnenceSign);
        Destroy(statisticsSign);
        Destroy(flowerBed);
        Destroy(fence);
        if (interactionAnouncement != null){

            foreach (Transform child in interactionAnouncement.transform){
                foreach (Transform leafChild in child){
                    Destroy(leafChild.gameObject);
                }
                Destroy(child.gameObject);
            }
            Destroy(interactionAnouncement);
        }
        if (nodes != null){
            foreach (Transform child in nodes.transform){
                foreach (Transform leafChild in child){
                    Destroy(leafChild.gameObject);
                }
                Destroy(child.gameObject);
            }
            Destroy(nodes);
        }

        Destroy(this.gameObject);
    }

    public void anounceInteraction(){

        interactionAnouncement = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:empty"), transform);
        GameObject hudPopUp = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:upDownBobPointer"), interactionAnouncement.transform);
        hudPopUp.GetComponentInChildren<SpriteRenderer>().sprite =
         GameManager.Instance.getSprite(FixedVariables.interactionIcons["flowerbed:" + farmStructure.structurePropreties["currentInteraction"]]);
        hudPopUp.transform.localPosition = new Vector3 (0, 2, 0);
    }

    public void collectCrop(){

        GameObject collectIcon = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:itemPickup"), transform);
        collectIcon.transform.localPosition = new Vector3(0f, 0f, 0f);
        collectIcon.GetComponent<ItemPickupAnim>().spriteRenderer.sprite =
            GameManager.Instance.getSprite("sprites:itemIcon:" + ((string)farmStructure.structurePropreties["resource"]).Split(":")[1]);
    }
}
