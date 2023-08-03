using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public static FarmBase.StructureInstance getStructure(RaycastHit hit){

        Interactable interactable;

        if (hit.transform != null && hit.transform.TryGetComponent<Interactable>(out interactable)){

            if (interactable.interactable){

                switch (interactable.interactableId){

                    case "flowerbed" :
                        return hit.transform.GetComponent<FlowerBedRenderer>().farmStructure;

                    case "beehouse" :
                        return hit.transform.GetComponent<BeehouseRenderer>().farmStructure;

                    case "farmland" :
                        return hit.transform.GetComponent<FarmlandRenderer>().farmStructure;

                    case "furnace" :
                        return hit.transform.GetComponent<FurnaceRenderer>().farmStructure;

                    case "tree" :
                        return hit.transform.GetComponent<TreeRenderer>().tree;

                    default :
                        return hit.transform.GetComponent<BasicRenderer>().farmStructure;

                }
            }
        }
        return null;
    }

    public static void deepUpdateStructure(FarmBase.StructureInstance structure){

        switch (structure.structureId){

            case "flowerbed" :
                Farm.Instance.structRenderers[structure].GetComponent<FlowerBedRenderer>().deepUpdateStructure();
                break;

            case "beehouse" :
                Farm.Instance.structRenderers[structure].GetComponent<BeehouseRenderer>().deepUpdateStructure();
                break;

            case "farmland" :
                Farm.Instance.structRenderers[structure].GetComponent<FarmlandRenderer>().deepUpdateStructure();
                break;

            case "furnace" :
                Farm.Instance.structRenderers[structure].GetComponent<FurnaceRenderer>().deepUpdateStructure();
                break;

            case "tree" :
                Farm.Instance.structRenderers[structure].GetComponent<TreeRenderer>().deepUpdateStructure();
                break;

            default :
                Farm.Instance.structRenderers[structure].GetComponent<BasicRenderer>().deepUpdateStructure();
                break;

        }
    }
    
    public static void interact(RaycastHit hit){

        Interactable interactable;

        if (hit.transform != null && hit.transform.TryGetComponent<Interactable>(out interactable)){

            if (interactable.interactable){

                switch (GameManager.Instance.tool){

                case "inspect" :

                    switch (interactable.interactableId){

                    case "statistics" :

                        UIController.Instance.statsPanel.openAndSet(hit.transform.GetComponent<StatisticsSign>().structure);
                        break;

                    case "flowerbed" :

                        interactFlowerbed(hit.transform.GetComponent<FlowerBedRenderer>());
                        break;

                    case "beehouse" :

                        interactBeehouse(hit.transform.GetComponent<BeehouseRenderer>());
                        break;

                    case "farmland" :

                        interactFarmland(hit.transform.GetComponent<FarmlandRenderer>());
                        break;

                    case "furnace" :

                        interactFurnace(hit.transform.GetComponent<FurnaceRenderer>());
                        break;
                    }

                    break;

                case "axe" :

                    switch (interactable.interactableId){
                        
                    case "tree" :

                        GameManager.Instance.StartCoroutine(chopTree(hit.transform.GetComponent<TreeRenderer>()));
                        break;
                    }

                    break;
                }
            }
        }
    }

    public static IEnumerator chopTree(TreeRenderer structure){

        GameManager.Instance.changeState("farmPrompt");

        UIController.Instance.interactBar.setState(true);
        UIController.Instance.interactBar.setBarPos();

        float targetTime = FixedVariables.miningTime[structure.tree.structurePropreties["resource"].ToString()];
        float currentTime = 0f;

        while (Input.GetButton("Tap") && currentTime < targetTime){
            
            UIController.Instance.interactBar.setSize(Mathf.Abs((currentTime / targetTime) - 1));
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (currentTime >= targetTime){

            structure.collectWood();

            float harvestCount = structure.getScale();
            Item harvestedWood = new Item("wood", Mathf.RoundToInt((harvestCount > 0.5f ? harvestCount : 0)
             * FixedVariables.harvestCount[structure.tree.structurePropreties["resource"].ToString()]));
            Item harvestedSappling = new Item(string.Format("{0}Sappling", structure.tree.structurePropreties["resource"].ToString().Split(":")[1]),
             Mathf.RoundToInt((harvestCount > 0.5f ? harvestCount : 0) * Random.Range(0,3)));
            GameManager.Instance.itemInventory.addItem(harvestedWood);
            GameManager.Instance.itemInventory.addItem(harvestedSappling);

            Farm.Instance.destroyStructure(structure.tree);
        }

        UIController.Instance.interactBar.setState(false);

        GameManager.Instance.changeState("farmRoaming");
    }

    public static void interactBeehouse(BeehouseRenderer structure){

        if (structure.farmStructure.structurePropreties["currentInteraction"] != null){

            List<string> nearbyFlowers = Farm.Instance.farmLayout.getResourcesInRadius(structure.farmStructure.anchorLocation, 2, "flowerbed");
            string resource;
            if (nearbyFlowers.Count > 0){
                string flower = nearbyFlowers[Random.Range(0, nearbyFlowers.Count)];
                resource = FixedVariables.flowerToHoney[flower];
            }
            else{
                resource = "honey";
            }

            structure.collectResource(resource);
            Item harvested = new Item(resource, FixedVariables.harvestCount["beehouse"]);

            ((Inventory)structure.farmStructure.structurePropreties["harvested"]).addItem(harvested);
            GameManager.Instance.itemInventory.addItem(harvested);

            structure.farmStructure.structurePropreties["age"] = 0f;
            structure.farmStructure.structurePropreties["stage"] = 0;

            structure.farmStructure.structurePropreties["currentInteraction"] = null;
            structure.deepUpdateStructure();
        }
    }

    public static void interactFurnace(FurnaceRenderer structure){

        if (structure.farmStructure.structurePropreties["currentInteraction"] != null){

            switch (structure.farmStructure.structurePropreties["currentInteraction"]){

                case "finishUpgrade" :

                    switch (structure.farmStructure.structurePropreties["currentlyUpgrading"]){

                        case "structure:furnace:tier0" :

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

                case "harvestResource" :

                    structure.collectResource();
                    int harvestAmount = (int)structure.farmStructure.structurePropreties["count"];
                    Item harvested = new Item(((string)structure.farmStructure.structurePropreties["resource"]).Split(":")[1], harvestAmount);

                    ((Inventory)structure.farmStructure.structurePropreties["harvested"]).addItem(harvested);
                    GameManager.Instance.itemInventory.addItem(harvested);

                    structure.farmStructure.structurePropreties["count"] = 0;
                    structure.farmStructure.structurePropreties["age"] = 0f;
                    structure.farmStructure.structurePropreties["resource"] = null;

                    break;
            }

            structure.farmStructure.structurePropreties["currentInteraction"] = null;
            structure.deepUpdateStructure();
        }
        else if (structure.farmStructure.structurePropreties["currentlyUpgrading"] == null){

            UIController.Instance.upgradeMenu.openForUpgrade(structure.farmStructure);
        }
    }

    public static void interactFlowerbed(FlowerBedRenderer structure){

        if (structure.farmStructure.structurePropreties["currentInteraction"] != null){

            switch (structure.farmStructure.structurePropreties["currentInteraction"]){

                case "finishUpgrade" :

                    switch (structure.farmStructure.structurePropreties["currentlyUpgrading"]){

                        case "structure:farmland:tier0" :

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
                    structure.farmStructure.structurePropreties["stage"] = FixedVariables.stageToResetTo[(string)structure.farmStructure.structurePropreties["resource"]];
                    int harvestAmount = (int)Mathf.Round(FixedVariables.harvestCount["flowerbed:quantity" + structure.farmStructure.structurePropreties["quantity"]]
                            * (((float)structure.farmStructure.structurePropreties["hydration"] / 2f) + 0.5f));
                    Item harvested = new Item(((string)structure.farmStructure.structurePropreties["resource"]).Split(":")[1], harvestAmount);

                    ((Inventory)structure.farmStructure.structurePropreties["harvested"]).addItem(harvested);
                    GameManager.Instance.itemInventory.addItem(harvested);
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

    public static void interactFarmland(FarmlandRenderer structure){

        if (structure.farmStructure.structurePropreties["currentInteraction"] != null){

            switch (structure.farmStructure.structurePropreties["currentInteraction"]){

                case "finishUpgrade" :

                    switch (structure.farmStructure.structurePropreties["currentlyUpgrading"]){

                        case "structure:farmland:tier0" :

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
                    int harvestAmount = (int)Mathf.Round(FixedVariables.harvestCount["farmland:quantity" + structure.farmStructure.structurePropreties["quantity"]]
                            * (((float)structure.farmStructure.structurePropreties["hydration"] / 2f) + 0.5f));
                    Item harvested = new Item(((string)structure.farmStructure.structurePropreties["resource"]).Split(":")[1], harvestAmount);

                    ((Inventory)structure.farmStructure.structurePropreties["harvested"]).addItem(harvested);
                    GameManager.Instance.itemInventory.addItem(harvested);
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
