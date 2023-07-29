using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public static void interact(RaycastHit hit){

        Interactable interactable;

        if (hit.transform != null && hit.transform.TryGetComponent<Interactable>(out interactable)){

            switch (interactable.interactableId){

                case "statistics" :

                    UIController.Instance.statsPanel.openAndSet(hit.transform.GetComponent<StatisticsSign>().structure);

                    break;

                case "farmland" :

                    interactFarmland(hit.transform.GetComponent<FarmlandRenderer>());

                    break;
            }
        }
    }

    public static void interactFarmland(FarmlandRenderer structure){

        if (structure.farmStructure.structurePropreties["currentInteraction"] != null){

            switch (structure.farmStructure.structurePropreties["currentInteraction"]){

                case "finishUpgrade" :

                    switch (structure.farmStructure.structurePropreties["currentlyUpgrading"]){

                        case "baseBuild" :

                            structure.farmStructure.structurePropreties["currentlyUpgrading"] = null;

                            break;

                        default :

                            string upgrade = (string)structure.farmStructure.structurePropreties["currentlyUpgrading"];
                            structure.farmStructure.structurePropreties[upgrade.Remove(upgrade.Length - 1)] =
                                (int)char.GetNumericValue(upgrade[upgrade.Length - 1]);
                            structure.farmStructure.structurePropreties["currentlyUpgrading"] = null;

                            break;
                    }

                    break;

                case "harvestPlant" :

                    structure.farmStructure.structurePropreties["age"] = 0f;
                    structure.collectCrop();

                    break;
            }

            structure.farmStructure.structurePropreties["currentInteraction"] = null;
            structure.deepUpdateStructure();
        }
        else if (structure.farmStructure.structurePropreties["currentlyUpgrading"] == null){

            UIController.Instance.upgradeMenu.openForUpgrade(structure.farmStructure);
        }
    }
}
