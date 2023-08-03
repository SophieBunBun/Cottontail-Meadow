using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public GameObject structures;
    public GameObject tiles;
    public GameObject borderTiles;
    public GameObject borderFolliage;

    public string[] treeTypes;

    public float timeScale;
    public int[] farmSize;

    public static Farm Instance;
    public bool anouncing = true;

    public FarmBase.FarmLayout farmLayout;
    public Dictionary<FarmBase.TileInstance, TileRenderer> tileRenderers = new Dictionary<FarmBase.TileInstance, TileRenderer>();
    public Dictionary<FarmBase.StructureInstance, GameObject> structRenderers = new Dictionary<FarmBase.StructureInstance, GameObject>();

    void Start()
    {
        Instance = this;
        farmLayout = new FarmBase.FarmLayout(farmSize);
        buildFarm();
        insertStructure(FarmBase.createStructureInstance("basichouse", new int[]{9, 9}));
    }

    void Update()
    {
        updateStructures();
    }

    private void buildFarm(){

        buildTiles();
        buildBorder();
    }

    private void buildTiles(){

        for (int x = 0; x < farmLayout.farmSize[0]; x++){
            for (int y = 0; y < farmLayout.farmSize[1]; y++){
                
                createTile(new FarmBase.TileInstance("tile:grass", new Vector2 (x, y)));
            }
        }
    }

    public void insertTile(FarmBase.TileInstance tile){

        farmLayout.tiles[(int)tile.tileLocation.x, (int)tile.tileLocation.y].tileId = tile.tileId;
        tileRenderers[farmLayout.tiles[(int)tile.tileLocation.x, (int)tile.tileLocation.y]].updateRenderer(farmLayout);
        
        for (int x = (int)tile.tileLocation.x - 1; x <= tile.tileLocation.x + 1; x++){
            for (int y = (int)tile.tileLocation.y - 1; y <= tile.tileLocation.y + 1; y++){
                if (x >= 0 && x < farmSize[0] && y >= 0 && y < farmSize[1] && tileRenderers[farmLayout.tiles[x,y]].tile != tile){
                    tileRenderers[farmLayout.tiles[x,y]].GetComponent<TileRenderer>().updateRenderer(farmLayout);
                }
            }
        }
    }

    private void buildBorder(){

        //Creating tiles
        for (int x = -1; x < farmSize[0] + 1; x++){
            for (int y = -1; y < farmSize[0] + 1; y++){

                if (x < 0 || x >= farmSize[0] || y < 0 || y >= farmSize[1]){

                    Vector3 rotation = new Vector3(0f, -90f, 0f);
                    if (y < 0) {rotation = new Vector3(0f, 180f, 0f);}
                    if (x >= farmSize[0]) {rotation = new Vector3(0f, 90f, 0f);}
                    if (y >= farmSize[1]) {rotation = new Vector3(0f, 0f, 0f);}
                    if (x < 0 && y >= farmSize[1]) {rotation = new Vector3(0f, -90f, 0f);}

                    GameObject tile;
                    if ((x < 0 && y < 0) || (x < 0 && y >= farmSize[1]) ||
                     (x >= farmSize[0] && y >= farmSize[1]) || (x >= farmSize[0] && y < 0)){
                        tile = Instantiate((GameObject)GameManager.Instance.getResource("border:tile:corner"), borderTiles.transform);}
                    else{
                        tile = Instantiate((GameObject)GameManager.Instance.getResource("border:tile:straight"), borderTiles.transform);}
                
                    tile.transform.localPosition = new Vector3(-1f + (x + 1) * 2f, 0f, -1f + (y + 1) * 2f);
                    tile.transform.localEulerAngles = rotation;
                }
            }
        }

        //Creating trees
        populateRingOfTrees(-2, farmSize[0] + 4, -4, farmSize[1] + 2, 1.25f);
        populateRingOfTrees(-4, farmSize[0] + 6, -6, farmSize[1] + 4, 1.25f);
        populateRingOfTrees(-6, farmSize[0] + 8, -8, farmSize[1] + 6, 1.25f);
        populateRingOfTrees(-8, farmSize[0] + 10, -10, farmSize[1] + 8, 1.25f);
        populateRingOfTrees(-10, farmSize[0] + 12, -12, farmSize[1] + 10, 1.25f);
    }

    private void populateRingOfTrees(float minX, float maxX, float minY, float maxY, float jump){

        minX /= jump;
        maxX /= jump;
        minY /= jump;
        maxY /= jump;

        for (float x = minX; x < maxX; x += jump){
            instantiateTree(x, minY, jump * 2);
        }
        for (float y = minY; y < maxY; y += jump){
            instantiateTree(maxX, y, jump * 2);
        }
        for (float x = maxX; x > minX; x -= jump){
            instantiateTree(x, maxY, jump * 2);
        }
        for (float y = maxY; y > minY; y -= jump){
            instantiateTree(minX, y, jump * 2);
        }
    }

    private void instantiateTree(float x, float y, float scale){

        Vector3 randomPos = new Vector3(x + Random.Range(-2f / scale, 2f / scale), 0f, y + Random.Range(-2f / scale, 2f / scale));
        TreeRenderer tree = Instantiate(
            (GameObject)GameManager.Instance.getResource("structures:structures:tree"), borderTiles.transform
            ).GetComponent<TreeRenderer>();

        tree.tree = FarmBase.createStructureInstance("tree", new int[] {0, 0});
        tree.tree.structurePropreties["resource"] = treeTypes[Random.Range(0, treeTypes.Length)];
        int stage = Random.Range(1, 3);
        tree.tree.structurePropreties["stage"] = stage;
        float growthTime = 1 + FarmBase.growthTimes
        [tree.tree.structurePropreties["resource"].ToString() + tree.tree.structurePropreties["stage"].ToString()];
        tree.tree.structurePropreties["age"] = Random.Range(stage == 1 ? growthTime / 2 : 0, growthTime );
        tree.tree.structurePropreties["shaded"] = true;

        tree.updateGraphics();
        tree.updateStructure();
        tree.transform.localPosition = randomPos * scale;
    }

    public GameObject getStructureRenderer(FarmBase.StructureInstance structure){

        return structRenderers[structure];
    }

    public void toggleAllAnnouncements(bool value){
        
        anouncing = value;

        if (value){
            foreach (FarmBase.StructureInstance structure in structRenderers.Keys){

                switch (structure.structureId){

                    case "flowerbed" :
                        Farm.Instance.structRenderers[structure].GetComponent<FlowerBedRenderer>().attemptAnouncement();
                        break;

                    case "beehouse" :
                        Farm.Instance.structRenderers[structure].GetComponent<BeehouseRenderer>().attemptAnouncement();
                        break;

                    case "farmland" :
                        Farm.Instance.structRenderers[structure].GetComponent<FarmlandRenderer>().attemptAnouncement();
                        break;

                    case "furnace" :
                        Farm.Instance.structRenderers[structure].GetComponent<FurnaceRenderer>().attemptAnouncement();
                        break;

                }
            }
        }
        else{
            foreach (FarmBase.StructureInstance structure in structRenderers.Keys){

                switch (structure.structureId){

                    case "flowerbed" :
                        Farm.Instance.structRenderers[structure].GetComponent<FlowerBedRenderer>().destroyAnouncement();
                        break;

                    case "beehouse" :
                        Farm.Instance.structRenderers[structure].GetComponent<BeehouseRenderer>().destroyAnouncement();
                        break;

                    case "farmland" :
                        Farm.Instance.structRenderers[structure].GetComponent<FarmlandRenderer>().destroyAnouncement();
                        break;

                    case "furnace" :
                        Farm.Instance.structRenderers[structure].GetComponent<FurnaceRenderer>().destroyAnouncement();
                        break;

                }
            }
        }
    }

    public void updateStructures(){

        foreach (FarmBase.StructureInstance structure in structRenderers.Keys){

            if (structRenderers[structure].activeSelf) switch (structure.structureId){

                case "furnace" :

                    if (structure.structurePropreties["currentInteraction"] == null){

                        if (structure.structurePropreties["currentlyUpgrading"] != null){

                            structure.structurePropreties["maintenenceTime"] = 
                            (float)structure.structurePropreties["maintenenceTime"] - Time.deltaTime * timeScale;

                            if ((float)structure.structurePropreties["maintenenceTime"] <= 0f){

                                structure.structurePropreties["currentInteraction"] = "finishUpgrade";
                                structRenderers[structure].GetComponent<FurnaceRenderer>().attemptAnouncement();
                            }
                        }
                        else if (structure.structurePropreties["resource"] != null){

                            structure.structurePropreties["age"] = 
                            (float)structure.structurePropreties["age"] + Time.deltaTime * timeScale 
                            * FixedVariables.harvestSpeed[string.Format("furnace:speed{0}", structure.structurePropreties["speed"])];

                            if ((float)structure.structurePropreties["age"] 
                            > 
                            FixedVariables.proccessTimes[(string)structure.structurePropreties["resource"]] * (int)structure.structurePropreties["count"]){

                                structRenderers[structure].GetComponent<Interactable>().interactable = true;
                                structure.structurePropreties["currentInteraction"] = "harvestResource";
                                structRenderers[structure].GetComponent<FurnaceRenderer>().deepUpdateStructure();
                            }
                        }
                    }

                    break;

                case "tree" :

                    if ((float)structure.structurePropreties["age"] 
                    > 
                    FarmBase.growthTimes[(string)structure.structurePropreties["resource"] 
                    + (int)structure.structurePropreties["stage"]]){

                        if (FixedVariables.resourceFinalStage[(string)structure.structurePropreties["resource"]]
                        != (int)structure.structurePropreties["stage"]) {

                            structure.structurePropreties["stage"] = (int)structure.structurePropreties["stage"] + 1;
                            structure.structurePropreties["age"] = 0f;
                            structRenderers[structure].GetComponent<TreeRenderer>().playLeafParticles();
                            structRenderers[structure].GetComponent<TreeRenderer>().deepUpdateStructure();
                        }
                    }
                    else{
                        structure.structurePropreties["age"] = (float)structure.structurePropreties["age"] + Time.deltaTime * timeScale;
                        structRenderers[structure].GetComponent<TreeRenderer>().updateStructure();
                    }

                    break;

                case "beehouse" :

                    if (structure.structurePropreties["currentInteraction"] == null){

                        structure.structurePropreties["age"] = (float)structure.structurePropreties["age"] + Time.deltaTime * timeScale;

                        if ((float)structure.structurePropreties["age"] 
                        > 
                        FixedVariables.proccessTimes[string.Format("beehouse{0}", structure.structurePropreties["stage"].ToString())]){

                            if (FixedVariables.resourceFinalStage["beehouse"] == (int)structure.structurePropreties["stage"]){
                                structure.structurePropreties["currentInteraction"] = "harvestResource";
                                structRenderers[structure].GetComponent<BeehouseRenderer>().attemptAnouncement();
                            }
                            else {

                                structure.structurePropreties["stage"] = (int)structure.structurePropreties["stage"] + 1;
                                structure.structurePropreties["age"] = 0f;
                                structRenderers[structure].GetComponent<BeehouseRenderer>().deepUpdateStructure();
                            }
                        }
                    }

                    break;

                case "flowerbed" :

                    if (structure.structurePropreties["currentInteraction"] == null){

                        if (structure.structurePropreties["currentlyUpgrading"] != null){

                            structure.structurePropreties["maintenenceTime"] = 
                            (float)structure.structurePropreties["maintenenceTime"] - Time.deltaTime * timeScale;

                            if ((float)structure.structurePropreties["maintenenceTime"] <= 0f){

                                structure.structurePropreties["currentInteraction"] = "finishUpgrade";
                                structRenderers[structure].GetComponent<FlowerBedRenderer>().attemptAnouncement();
                            }
                        }
                        else if (structure.structurePropreties["resource"] != null){

                            structure.structurePropreties["age"] = (float)structure.structurePropreties["age"] + Time.deltaTime * timeScale;
                            structRenderers[structure].GetComponent<FlowerBedRenderer>().updateStructure();

                            if ((float)structure.structurePropreties["age"] 
                            > 
                            FixedVariables.proccessTimes[(string)structure.structurePropreties["resource"] 
                            + (int)structure.structurePropreties["stage"]]){

                                if (FixedVariables.resourceFinalStage[(string)structure.structurePropreties["resource"]]
                                == (int)structure.structurePropreties["stage"]){
                                    structure.structurePropreties["currentInteraction"] = "harvestPlant";
                                    structRenderers[structure].GetComponent<FlowerBedRenderer>().attemptAnouncement();
                                }
                                else {

                                    structure.structurePropreties["stage"] = (int)structure.structurePropreties["stage"] + 1;
                                    structure.structurePropreties["age"] = 0f;
                                }
                            }
                        }
                    }

                    break;

                case "farmland" :

                    if (structure.structurePropreties["currentInteraction"] == null){

                        if (structure.structurePropreties["currentlyUpgrading"] != null){

                            structure.structurePropreties["maintenenceTime"] = 
                            (float)structure.structurePropreties["maintenenceTime"] - Time.deltaTime * timeScale;

                            if ((float)structure.structurePropreties["maintenenceTime"] <= 0f){

                                structure.structurePropreties["currentInteraction"] = "finishUpgrade";
                                structRenderers[structure].GetComponent<FarmlandRenderer>().attemptAnouncement();
                            }
                        }
                        else if (structure.structurePropreties["resource"] != null){

                            structure.structurePropreties["age"] = (float)structure.structurePropreties["age"] + Time.deltaTime * timeScale;
                            structRenderers[structure].GetComponent<FarmlandRenderer>().updateStructure();

                            if ((float)structure.structurePropreties["age"] 
                            > 
                            FarmBase.growthTimes[(string)structure.structurePropreties["resource"] 
                            + (int)structure.structurePropreties["stage"]]){

                                if (FixedVariables.resourceFinalStage[(string)structure.structurePropreties["resource"]]
                                == (int)structure.structurePropreties["stage"]){
                                    structure.structurePropreties["currentInteraction"] = "harvestPlant";
                                    structRenderers[structure].GetComponent<FarmlandRenderer>().attemptAnouncement();
                                }
                                else {

                                    structure.structurePropreties["stage"] = (int)structure.structurePropreties["stage"] + 1;
                                    structure.structurePropreties["age"] = 0f;
                                }
                            }
                        }
                    }

                    break;
            }
        }
    }

    public void createTile(FarmBase.TileInstance tile){

        farmLayout.insertTile(tile);
        GameObject tileObj = Instantiate((GameObject)GameManager.Instance.getResource("tiles:ground:tile"), tiles.transform);
        tileObj.GetComponent<TileRenderer>().tile = tile;
        tileObj.GetComponent<TileRenderer>().updatePlacement();
        tileRenderers.Add(tile, tileObj.GetComponent<TileRenderer>());
    }

    public void insertStructure(FarmBase.StructureInstance structure){

        if (farmLayout.canFit(structure)){

            farmLayout.insertStructure(structure);
            structRenderers.Add(structure, createStructure(structure));
        }
    }

    public GameObject createStructure(FarmBase.StructureInstance structure){

        switch (structure.structureId){

            case "furnace" :

                GameObject furnace = Instantiate((GameObject)GameManager.Instance.getResource("structures:structures:furnace"), structures.transform);
                furnace.GetComponent<FurnaceRenderer>().farmStructure = structure;
                furnace.GetComponent<FurnaceRenderer>().deepUpdateStructure();
                return furnace;

            case "flowerbed" :

                GameObject flowerbed = Instantiate((GameObject)GameManager.Instance.getResource("structures:structures:flowerbed"), structures.transform);
                flowerbed.GetComponent<FlowerBedRenderer>().farmStructure = structure;
                flowerbed.GetComponent<FlowerBedRenderer>().deepUpdateStructure();
                return flowerbed;

            case "beehouse" :

                GameObject beehouse = Instantiate((GameObject)GameManager.Instance.getResource("structures:structures:beehouse"), structures.transform);
                beehouse.GetComponent<BeehouseRenderer>().farmStructure = structure;
                beehouse.GetComponent<BeehouseRenderer>().deepUpdateStructure();
                return beehouse;

            case "farmland" :

                GameObject farmLand = Instantiate((GameObject)GameManager.Instance.getResource("structures:structures:farmland"), structures.transform);
                farmLand.GetComponent<FarmlandRenderer>().farmStructure = structure;
                farmLand.GetComponent<FarmlandRenderer>().deepUpdateStructure();
                return farmLand;

            case "tree" :

                GameObject tree = Instantiate((GameObject)GameManager.Instance.getResource("structures:structures:tree"), structures.transform);
                tree.GetComponent<TreeRenderer>().tree = structure;
                tree.GetComponent<TreeRenderer>().deepUpdateStructure();
                return tree;

            default :
                
                GameObject basic = Instantiate((GameObject)GameManager.Instance.getResource("structures:structures:" + structure.structureId), structures.transform);
                basic.GetComponent<BasicRenderer>().farmStructure = structure;
                basic.GetComponent<BasicRenderer>().deepUpdateStructure();
                return basic;
        }
    }

    public void destroyStructure(FarmBase.StructureInstance structure){

        switch (structure.structureId){

            case "furnace" :

                structRenderers[structure].GetComponent<FurnaceRenderer>().destroyStructure();
                break;

            case "flowerbed" :

                structRenderers[structure].GetComponent<FlowerBedRenderer>().destroyStructure();
                break;

            case "beehouse" :

                structRenderers[structure].GetComponent<BeehouseRenderer>().destroyStructure();
                break;

            case "farmland" :

                structRenderers[structure].GetComponent<FarmlandRenderer>().destroyStructure();
                break;

            case "tree" :

                structRenderers[structure].GetComponent<TreeRenderer>().destroyStructure();
                break;

            default :

                structRenderers[structure].GetComponent<BasicRenderer>().destroyStructure();
                break;
        }

        structRenderers.Remove(structure);
        farmLayout.removeStructure(structure);
    }
}
