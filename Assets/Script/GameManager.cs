using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Inventory itemInventory = new Inventory();
    public Dictionary<string, bool> isUnlocked = new Dictionary<string, bool>{

        {"farmland:empty", true},
        {"farmland:carrot", true},
        {"farmland:potato", true},
        {"farmland:wheat", false},
        {"farmland:cabbage", false},
        {"farmland:tomato", true},
        {"farmland:mushroom", false},
        {"farmland:strawberry", false},
        {"farmland:pumpkin", false},

        {"flowerbed:empty", true},
        {"flowerbed:pinkhydra", true},
        {"flowerbed:bluehydra", true},
        {"flowerbed:pinkorchid", true},
        {"flowerbed:blueorchid", true},
        {"flowerbed:whiteorchid", true},
        {"flowerbed:orangeorchid", true},

        {"furnace:copper", true},
        {"furnace:iron", true},
        {"furnace:brick", true},
        {"furnace:gold", true},
        {"furnace:tungsten", true},
        {"furnace:glass", true},
        {"furnace:titanium", true},
        {"furnace:niobium", true},

        {"tile:tile:grass", true},
        {"tile:path:dirt", true},
        {"tile:path:brick", true},
        {"tile:path:water", true},
        {"tile:path:gravel", true},

        {"tree:oak", true},
        {"tree:pine", true},
        {"tree:cherry", true},

        {"fruittree:apple", true},
    };

    void Awake()
    {
        itemInventory.addItem(new Item("money", 1000000));
        itemInventory.addItem(new Item("copperOre", 500));
        itemInventory.addItem(new Item("ironOre", 6));
        itemInventory.addItem(new Item("coal", 5));
        Instance = this;
        loadResources("farmAssets");
    }

    //General managers & controllers

    public CameraController cameraController;
    public PlayerMovement playerMovement;
    public UIController uiController;

    //Input & state management

    public string state = "farmRoaming";
    public string tool = "inspect";
    void Update(){

        switch (state){

            case "farmRoaming":

                if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
                Input.GetButtonDown("Tap")){ Interact.interact(cameraController.raycast()); }
                if(Input.GetAxis("Mouse ScrollWheel") != 0){ cameraController.updateZoom(); }
                if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){ playerMovement.moveCharacter(); }
                else { playerMovement.stopCharacter(); }

            break;

            case "farmPrompt":

                playerMovement.stopCharacter();
                if(Input.GetAxis("Mouse ScrollWheel") != 0){ cameraController.updateZoom(); }

            break;

            case "destroyMode":

                playerMovement.stopCharacter();
                if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()
                && Input.GetButton("Tap")){ uiController.buildMenu.askForDestruction(Interact.getStructure(cameraController.raycast())); }

            break;

            case "preciseBuildPlan":

                playerMovement.stopCharacter();
                if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()
                && Input.GetButton("Tap")){ uiController.buildMenu.updateBuildPlanPos(cameraController.raycast()); }

            break;

            case "freeBuildPlan":

                playerMovement.stopCharacter();
                if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()
                && Input.GetButton("Tap")){ uiController.buildMenu.updateBuildPlanFree(cameraController.raycast()); }

            break;
        }
    }

    public void changeState(string state){

        switch (state){

            case "farmRoaming":

                uiController.farmRoamingMode();

            break;

            case "farmPrompt":

                uiController.farmPromptMode();

            break;

            case "buildPlan":

                uiController.buildPlanMode();

            break;

            case "destroyMode":

                uiController.buildPlanMode();

            break;
        }

        this.state = state;
    }

    //Resource loading

    public string loadedKey = null;
    private static Dictionary<string, string[][]> loadKey = new Dictionary<string, string[][]>
    {
        {"farmAssets", new string[][] 
            {
                new string[] {"Prefabs/General/point", "general:tools:empty"},
                new string[] {"Prefabs/General/cameraLockedSprite", "general:tools:cameraSprite"},
                new string[] {"Prefabs/General/UpDownBobPointer", "general:tools:upDownBobPointer"},
                new string[] {"Prefabs/General/ColoredPlane", "general:tools:plane"},
                new string[] {"Prefabs/General/ItemPickupIcon", "general:tools:itemPickup"},

                new string[] {"Prefabs/General/UI/Button", "general:ui:basicbutton"},
                new string[] {"Prefabs/General/UI/upgradeButton", "general:ui:upgradeButton"},
                new string[] {"Prefabs/General/UI/itemDisplay", "general:ui:itemDisplay"},
                new string[] {"Prefabs/General/UI/itemDisplayButton", "general:ui:itemDisplayButton"},
                new string[] {"Prefabs/General/UI/resourceSelectionButton", "general:ui:resourceSelectionButton"},
                new string[] {"Prefabs/General/UI/resourceSelectionButtonComplex", "general:ui:resourceSelectionButtonComplex"},

                new string[] {"Textures/Sprites/ResourceIcons/emptyIcon", "sprites:resourceIcon:empty"},
                new string[] {"Textures/Sprites/ResourceIcons/carrotCropIcon", "sprites:resourceIcon:carrot"},
                new string[] {"Textures/Sprites/ResourceIcons/potatoCropIcon", "sprites:resourceIcon:potato"},
                new string[] {"Textures/Sprites/ResourceIcons/tomatoCropIcon", "sprites:resourceIcon:tomato"},
                new string[] {"Textures/Sprites/ResourceIcons/carrotCropIcon", "sprites:resourceIcon:honey"},
                new string[] {"Textures/Sprites/ResourceIcons/carrotCropIcon", "sprites:resourceIcon:hydrahoney"},
                new string[] {"Textures/Sprites/ResourceIcons/carrotCropIcon", "sprites:resourceIcon:orchidhoney"},
                new string[] {"Textures/Sprites/ResourceIcons/pinkHydraFlowerIcon", "sprites:resourceIcon:pinkhydra"},
                new string[] {"Textures/Sprites/ResourceIcons/blueHydraFlowerIcon", "sprites:resourceIcon:bluehydra"},
                new string[] {"Textures/Sprites/ResourceIcons/pinkOrchidFlowerIcon", "sprites:resourceIcon:pinkorchid"},
                new string[] {"Textures/Sprites/ResourceIcons/blueOrchidFlowerIcon", "sprites:resourceIcon:blueorchid"},
                new string[] {"Textures/Sprites/ResourceIcons/whiteOrchidFlowerIcon", "sprites:resourceIcon:whiteorchid"},
                new string[] {"Textures/Sprites/ResourceIcons/orangeOrchidFlowerIcon", "sprites:resourceIcon:orangeorchid"},
                new string[] {"Textures/Sprites/ResourceIcons/copperBarResourceIcon", "sprites:resourceIcon:copper"},
                new string[] {"Textures/Sprites/ResourceIcons/ironBarResourceIcon", "sprites:resourceIcon:iron"},
                new string[] {"Textures/Sprites/ResourceIcons/brickResourceIcon", "sprites:resourceIcon:brick"},
                new string[] {"Textures/Sprites/ResourceIcons/carrotCropIcon", "sprites:resourceIcon:wood"},

                new string[] {"Textures/Sprites/StructureIcons/farmland", "sprites:structureIcon:farmland"},
                new string[] {"Textures/Sprites/StructureIcons/flowerbed", "sprites:structureIcon:flowerbed"},
                new string[] {"Textures/Sprites/StructureIcons/furnace", "sprites:structureIcon:furnace"},
                new string[] {"Textures/Sprites/StructureIcons/beehouse", "sprites:structureIcon:beehouse"},

                new string[] {"Textures/Sprites/ItemIcons/moneyItem", "sprites:itemIcon:money"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:carrot"},
                new string[] {"Textures/Sprites/ItemIcons/potatoItem", "sprites:itemIcon:potato"},
                new string[] {"Textures/Sprites/ItemIcons/tomatoItem", "sprites:itemIcon:tomato"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:honey"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:hydrahoney"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:orchidhoney"},
                new string[] {"Textures/Sprites/ItemIcons/pinkHydraItem", "sprites:itemIcon:pinkhydra"},
                new string[] {"Textures/Sprites/ItemIcons/blueHydraItem", "sprites:itemIcon:bluehydra"},
                new string[] {"Textures/Sprites/ItemIcons/pinkOrchidItem", "sprites:itemIcon:pinkorchid"},
                new string[] {"Textures/Sprites/ItemIcons/blueOrchidItem", "sprites:itemIcon:blueorchid"},
                new string[] {"Textures/Sprites/ItemIcons/whiteOrchidItem", "sprites:itemIcon:whiteorchid"},
                new string[] {"Textures/Sprites/ItemIcons/orangeOrchidItem", "sprites:itemIcon:orangeorchid"},
                new string[] {"Textures/Sprites/ItemIcons/coalItem", "sprites:itemIcon:coal"},
                new string[] {"Textures/Sprites/ItemIcons/copperOreItem", "sprites:itemIcon:copperOre"},
                new string[] {"Textures/Sprites/ItemIcons/copperBarItem", "sprites:itemIcon:copper"},
                new string[] {"Textures/Sprites/ItemIcons/ironOreItem", "sprites:itemIcon:ironOre"},
                new string[] {"Textures/Sprites/ItemIcons/ironBarItem", "sprites:itemIcon:iron"},
                new string[] {"Textures/Sprites/ItemIcons/clayItem", "sprites:itemIcon:clay"},
                new string[] {"Textures/Sprites/ItemIcons/brickItem", "sprites:itemIcon:brick"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:wood"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:oakSappling"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:pineSappling"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:cherrySappling"},

                new string[] {"Textures/Sprites/InteractionIcons/defaultInteraction", "sprites:interactionIcon:default"},
                new string[] {"Textures/Sprites/InteractionIcons/maintnenceComplete", "sprites:interactionIcon:maintnenceComplete"},
                new string[] {"Textures/Sprites/InteractionIcons/farmlandCropReady", "sprites:interactionIcon:farmlandCropReady"},
                new string[] {"Textures/Sprites/InteractionIcons/flowerBedFlowerReady", "sprites:interactionIcon:flowerbedFlowerReady"},
                new string[] {"Textures/Sprites/InteractionIcons/furnaceResourceReady", "sprites:interactionIcon:furnaceResourceReady"},

                new string[] {"Textures/Sprites/ButtonIcons/inspect", "sprites:buttonIcons:inspect"},
                new string[] {"Textures/Sprites/ButtonIcons/wateringcan", "sprites:buttonIcons:wateringcan"},
                new string[] {"Textures/Sprites/ButtonIcons/axe", "sprites:buttonIcons:axe"},
                new string[] {"Textures/Sprites/ButtonIcons/pickaxe", "sprites:buttonIcons:pickaxe"},
                new string[] {"Textures/Sprites/ButtonIcons/buildmode", "sprites:buttonIcons:build"},
                new string[] {"Textures/Sprites/ButtonIcons/move", "sprites:buttonIcons:move"},
                new string[] {"Textures/Sprites/ButtonIcons/destroy", "sprites:buttonIcons:destroy"},

                //Upgrade Icons
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandCrop", "sprites:upgradeIcon:farmlandCrop"},
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandQuantity", "sprites:upgradeIcon:farmlandQuantity"},
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandSoilQuality", "sprites:upgradeIcon:farmlandSoilQuality"},
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandSoilRetension", "sprites:upgradeIcon:farmlandSoilRetension"},

                new string[] {"Textures/Sprites/UpgradeIcons/flowerBedFlower", "sprites:upgradeIcon:flowerbedFlower"},
                new string[] {"Textures/Sprites/UpgradeIcons/flowerBedQuantity", "sprites:upgradeIcon:flowerbedQuantity"},
                new string[] {"Textures/Sprites/UpgradeIcons/flowerBedSoilQuality", "sprites:upgradeIcon:flowerbedSoilQuality"},

                new string[] {"Textures/Sprites/UpgradeIcons/furnaceResource", "sprites:upgradeIcon:furnaceResource"},
                new string[] {"Textures/Sprites/UpgradeIcons/furnaceTier", "sprites:upgradeIcon:furnaceTier"},
                new string[] {"Textures/Sprites/UpgradeIcons/furnaceQuantity", "sprites:upgradeIcon:furnaceQuantity"},
                new string[] {"Textures/Sprites/UpgradeIcons/furnaceSpeed", "sprites:upgradeIcon:furnaceSpeed"},

                //Border
                new string[] {"Prefabs/Farm/BuildPlan", "planning:farm:buildplan"},
                new string[] {"Prefabs/Farm/Tiles/tile", "tiles:ground:tile"},
                new string[] {"Prefabs/Farm/Border/BorderStraight", "border:tile:straight"},
                new string[] {"Prefabs/Farm/Border/BorderCorner", "border:tile:corner"},

                //Farmland
                new string[] {"Prefabs/Farm/Farmland/farmLandStruct", "structures:structures:farmland"},
                new string[] {"Prefabs/Farm/Farmland/farmLand", "structures:farmland:farmland1"},
                new string[] {"Prefabs/Farm/Farmland/farmLandConstruction", "structures:farmland:farmland0"},
                new string[] {"Prefabs/Farm/Farmland/maintenenceSign", "structures:farmland:maintnencesign"},
                new string[] {"Prefabs/Farm/Farmland/statisticsSign", "structures:farmland:statisticsSign"},
                new string[] {"Prefabs/Farm/Farmland/farmlandFence", "structures:farmland:fence"},
                new string[] {"Prefabs/Farm/Farmland/Carrot", "structures:farmland:carrot"},
                new string[] {"Prefabs/Farm/Farmland/Potato", "structures:farmland:potato"},
                new string[] {"Prefabs/Farm/Farmland/Tomato", "structures:farmland:tomato"},

                //Flowerbed
                new string[] {"Prefabs/Farm/Structures/Flowerbed/flowerBedStruct", "structures:structures:flowerbed"},
                new string[] {"Prefabs/Farm/Structures/Flowerbed/flowerBed", "structures:flowerbed:flowerbed1"},
                new string[] {"Prefabs/Farm/Structures/Flowerbed/flowerBedConstruction", "structures:flowerbed:flowerbed0"},
                new string[] {"Prefabs/Farm/Structures/Flowerbed/flowerBedFence", "structures:flowerbed:fence"},
                new string[] {"Prefabs/Farm/Flowerbed/PinkHydrangea", "structures:flowerbed:pinkhydra"},
                new string[] {"Prefabs/Farm/Flowerbed/BlueHydrangea", "structures:flowerbed:bluehydra"},
                new string[] {"Prefabs/Farm/FlowerBed/PinkOrchid", "structures:flowerbed:pinkorchid"},
                new string[] {"Prefabs/Farm/FlowerBed/BlueOrchid", "structures:flowerbed:blueorchid"},
                new string[] {"Prefabs/Farm/FlowerBed/OrangeOrchid", "structures:flowerbed:orangeorchid"},
                new string[] {"Prefabs/Farm/FlowerBed/WhiteOrchid", "structures:flowerbed:whiteorchid"},

                //Beehouse
                new string[] {"Prefabs/Farm/Structures/Beehouse/beehouseStruct", "structures:structures:beehouse"},
                new string[] {"Models/Farm/Materials/Beehouse/beehouse0Mat", "structures:beehouse:beehouse0"},
                new string[] {"Models/Farm/Materials/Beehouse/beehouse1Mat", "structures:beehouse:beehouse1"},
                new string[] {"Models/Farm/Materials/Beehouse/beehouse2Mat", "structures:beehouse:beehouse2"},

                //Furnace
                new string[] {"Prefabs/Farm/Structures/Furnace/furnaceStruct", "structures:structures:furnace"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0BuildMat", "structures:furnace:0build"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0Mat", "structures:furnace:0unlit"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0LitMat", "structures:furnace:0lit"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0BuildMat", "structures:furnace:1build"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0Mat", "structures:furnace:1unlit"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0LitMat", "structures:furnace:1lit"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0BuildMat", "structures:furnace:2build"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0Mat", "structures:furnace:2unlit"},
                new string[] {"Models/Farm/Materials/Furnace/furnace0LitMat", "structures:furnace:2lit"},

                //Trees
                new string[] {"Prefabs/Farm/Farmland/TreeStruct", "structures:structures:tree"},

                new string[] {"Textures/Folliage/Trees/OakTreeMat/oakTree1", "structures:tree:oak0"},
                new string[] {"Textures/Folliage/Trees/OakTreeMat/oakTree2", "structures:tree:oak1"},
                new string[] {"Textures/Folliage/Trees/OakTreeMat/oakTree3", "structures:tree:oak2"},
                new string[] {"Textures/Folliage/Trees/OakTreeMat/oakTree1Shaded", "structures:tree:oak0Shaded"},
                new string[] {"Textures/Folliage/Trees/OakTreeMat/oakTree2Shaded", "structures:tree:oak1Shaded"},
                new string[] {"Textures/Folliage/Trees/OakTreeMat/oakTree3Shaded", "structures:tree:oak2Shaded"},

                //Paths & tiles
                new string[] {"Textures/Tiles/Grass/GrassMat", "materials:tile:grass"},
                ////Dirt path
                new string[] {"Textures/Tiles/DirtPath/0ConnectionDirtPathMat", "materials:path:dirt0"},
                new string[] {"Textures/Tiles/DirtPath/1ConnectionDirtPathMat", "materials:path:dirt1"},
                new string[] {"Textures/Tiles/DirtPath/2ConnectionStraightDirtPathMat", "materials:path:dirt2"},
                new string[] {"Textures/Tiles/DirtPath/2ConnectionCurveDirtPathMat", "materials:path:dirt3"},
                new string[] {"Textures/Tiles/DirtPath/3ConnectionDirtPathMat", "materials:path:dirt4"},
                new string[] {"Textures/Tiles/DirtPath/4ConnectionDirtPathMat", "materials:path:dirt5"},
                new string[] {"Textures/Tiles/DirtPath/Corners/1CornerDirtPathMat", "materials:path:dirtcorner0"},
                new string[] {"Textures/Tiles/DirtPath/Corners/2CornerStraightDirtPathMat", "materials:path:dirtcorner1"},
                new string[] {"Textures/Tiles/DirtPath/Corners/2CornerCurveDirtPathMat", "materials:path:dirtcorner2"},
                new string[] {"Textures/Tiles/DirtPath/Corners/3CornerDirtPathMat", "materials:path:dirtcorner3"},
                new string[] {"Textures/Tiles/DirtPath/Corners/4CornerDirtPathMat", "materials:path:dirtcorner4"},
                ////Brick path
                new string[] {"Textures/Tiles/BrickPath/0ConnectionBrickPathMat", "materials:path:brick0"},
                new string[] {"Textures/Tiles/BrickPath/1ConnectionBrickPathMat", "materials:path:brick1"},
                new string[] {"Textures/Tiles/BrickPath/2ConnectionStraightBrickPathMat", "materials:path:brick2"},
                new string[] {"Textures/Tiles/BrickPath/2ConnectionCurveBrickPathMat", "materials:path:brick3"},
                new string[] {"Textures/Tiles/BrickPath/3ConnectionBrickPathMat", "materials:path:brick4"},
                new string[] {"Textures/Tiles/BrickPath/4ConnectionBrickPathMat", "materials:path:brick5"},
                new string[] {"Textures/Tiles/BrickPath/Corners/1CornerBrickPathMat", "materials:path:brickcorner0"},
                new string[] {"Textures/Tiles/BrickPath/Corners/2CornerStraightBrickPathMat", "materials:path:brickcorner1"},
                new string[] {"Textures/Tiles/BrickPath/Corners/2CornerCurveBrickPathMat", "materials:path:brickcorner2"},
                new string[] {"Textures/Tiles/BrickPath/Corners/3CornerBrickPathMat", "materials:path:brickcorner3"},
                new string[] {"Textures/Tiles/BrickPath/Corners/4CornerBrickPathMat", "materials:path:brickcorner4"},
                ////Water tile
                new string[] {"Textures/Tiles/WaterTile1/0ConnectionWaterTile1Mat", "materials:path:water0"},
                new string[] {"Textures/Tiles/WaterTile1/1ConnectionWaterTile1Mat", "materials:path:water1"},
                new string[] {"Textures/Tiles/WaterTile1/2ConnectionStraightWaterTile1Mat", "materials:path:water2"},
                new string[] {"Textures/Tiles/WaterTile1/2ConnectionCurveWaterTile1Mat", "materials:path:water3"},
                new string[] {"Textures/Tiles/WaterTile1/3ConnectionWaterTile1Mat", "materials:path:water4"},
                new string[] {"Textures/Tiles/WaterTile1/4ConnectionWaterTile1Mat", "materials:path:water5"},
                new string[] {"Textures/Tiles/WaterTile1/Corners/1CornerWaterTile1Mat", "materials:path:watercorner0"},
                new string[] {"Textures/Tiles/WaterTile1/Corners/2CornerStraightWaterTile1Mat", "materials:path:watercorner1"},
                new string[] {"Textures/Tiles/WaterTile1/Corners/2CornerCurveWaterTile1Mat", "materials:path:watercorner2"},
                new string[] {"Textures/Tiles/WaterTile1/Corners/3CornerWaterTile1Mat", "materials:path:watercorner3"},
                new string[] {"Textures/Tiles/WaterTile1/Corners/4CornerWaterTile1Mat", "materials:path:watercorner4"},
                ////Gravel tile
                new string[] {"Textures/Tiles/GravelPath/0ConnectionGravelPathMat", "materials:path:gravel0"},
                new string[] {"Textures/Tiles/GravelPath/1ConnectionGravelPathMat", "materials:path:gravel1"},
                new string[] {"Textures/Tiles/GravelPath/2ConnectionStraightGravelPathMat", "materials:path:gravel2"},
                new string[] {"Textures/Tiles/GravelPath/2ConnectionCurveGravelPathMat", "materials:path:gravel3"},
                new string[] {"Textures/Tiles/GravelPath/3ConnectionGravelPathMat", "materials:path:gravel4"},
                new string[] {"Textures/Tiles/GravelPath/4ConnectionGravelPathMat", "materials:path:gravel5"},
                new string[] {"Textures/Tiles/GravelPath/Corners/1CornerGravelPathMat", "materials:path:gravelcorner0"},
                new string[] {"Textures/Tiles/GravelPath/Corners/2CornerStraightGravelPathMat", "materials:path:gravelcorner1"},
                new string[] {"Textures/Tiles/GravelPath/Corners/2CornerCurveGravelPathMat", "materials:path:gravelcorner2"},
                new string[] {"Textures/Tiles/GravelPath/Corners/3CornerGravelPathMat", "materials:path:gravelcorner3"},
                new string[] {"Textures/Tiles/GravelPath/Corners/4CornerGravelPathMat", "materials:path:gravelcorner4"},
            }
        }
    };
    private Dictionary<string, Dictionary<string, Dictionary<string, object>>> loadedResources; 
    public void loadResources(string key){

        loadedResources = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

        foreach (string[] entry in loadKey[key]){

            var resource = Resources.Load(entry[0]);
            string[] split = entry[1].Split(":");

            if (!loadedResources.ContainsKey(split[0])){
                loadedResources.Add(split[0], new Dictionary<string, Dictionary<string, object>>());
            }

            if (!loadedResources[split[0]].ContainsKey(split[1])){
                loadedResources[split[0]].Add(split[1], new Dictionary<string, object>());
            }

            loadedResources[split[0]][split[1]].Add(split[2], resource);
        }

        loadedKey = key;
    }
    public object getResource(string id){
        string[] split = id.Split(":");
        return loadedResources[split[0]][split[1]][split[2]];
    }

    public Sprite getSprite(string id){
        string[] split = id.Split(":");
        return Sprite.Create((Texture2D)loadedResources[split[0]][split[1]][split[2]], new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
    }
}
