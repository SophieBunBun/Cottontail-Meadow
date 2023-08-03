using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeehouseRenderer : MonoBehaviour
{
    public FarmBase.StructureInstance farmStructure;
    public GameObject beehouseTexture;
    private GameObject interactionAnouncement;

    public void deepUpdateStructure(){

        //Setting anchor location
        transform.position = new Vector3((farmStructure.anchorLocation[0] + FarmBase.structureSize[farmStructure.structureId][0] / 2f) * 2f ,0.01f ,
                                    (farmStructure.anchorLocation[1] + FarmBase.structureSize[farmStructure.structureId][1] / 2f) * 2f);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);

        destroyAnouncement();

        beehouseTexture.GetComponent<MeshRenderer>().material =
            (Material)GameManager.Instance.getResource(string.Format("structures:beehouse:beehouse{0}", farmStructure.structurePropreties["stage"].ToString()));

        //Initiating interactAnouncement
        attemptAnouncement();
    }
    
    public void attemptAnouncement(){
        if (farmStructure.structurePropreties["currentInteraction"] != null && Farm.Instance.anouncing){

            anounceInteraction();
        }
    }

    public void destroyAnouncement(){
        if (interactionAnouncement != null){

            foreach (Transform child in interactionAnouncement.transform){
                foreach (Transform leafChild in child){
                    Destroy(leafChild.gameObject);
                }
                Destroy(child.gameObject);
            }
            Destroy(interactionAnouncement);
        }
    }

    public void destroyStructure(){
        Destroy(beehouseTexture);
        destroyAnouncement();
        Destroy(this.gameObject);
    }

    public void anounceInteraction(){

        interactionAnouncement = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:empty"), transform);
        GameObject hudPopUp = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:upDownBobPointer"), interactionAnouncement.transform);
        hudPopUp.GetComponentInChildren<SpriteRenderer>().sprite =
         GameManager.Instance.getSprite(FixedVariables.interactionIcons["beehouse:" + farmStructure.structurePropreties["currentInteraction"]]);
        hudPopUp.transform.localPosition = new Vector3 (0, 3.5f, 0);
    }

    public void collectResource(string resource){

        GameObject collectIcon = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:itemPickup"), transform);
        collectIcon.transform.localPosition = new Vector3(0f, 0f, 0f);
        collectIcon.GetComponent<ItemPickupAnim>().spriteRenderer.sprite =
            GameManager.Instance.getSprite(string.Format("sprites:itemIcon:{0}", resource));
    }
}
