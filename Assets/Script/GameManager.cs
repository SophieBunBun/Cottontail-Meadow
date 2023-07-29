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
        Instance = this;
        loadResources("farmAssets");
    }

    //General managers & controllers

    public CameraController cameraController;
    public PlayerMovement playerMovement;
    public UIController uiController;

    //Input & state management

    public string state = "farmRoaming";
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

                new string[] {"Prefabs/General/UI/upgradeButton", "general:ui:upgradeButton"},
                new string[] {"Prefabs/General/UI/itemDisplay", "general:ui:itemDisplay"},
                new string[] {"Prefabs/General/UI/resourceSelectionButton", "general:ui:resourceSelectionButton"},

                new string[] {"Textures/Sprites/ResourceIcons/emptyIcon", "sprites:resourceIcon:empty"},
                new string[] {"Textures/Sprites/ResourceIcons/carrotCropIcon", "sprites:resourceIcon:carrot"},
                new string[] {"Textures/Sprites/ResourceIcons/potatoCropIcon", "sprites:resourceIcon:potato"},
                new string[] {"Textures/Sprites/ResourceIcons/tomatoCropIcon", "sprites:resourceIcon:tomato"},

                new string[] {"Textures/Sprites/StructureIcons/farmland", "sprites:structureIcon:farmland"},

                new string[] {"Textures/Sprites/ItemIcons/moneyItem", "sprites:itemIcon:money"},
                new string[] {"Textures/Sprites/ItemIcons/carrotItem", "sprites:itemIcon:carrot"},
                new string[] {"Textures/Sprites/ItemIcons/potatoItem", "sprites:itemIcon:potato"},
                new string[] {"Textures/Sprites/ItemIcons/tomatoItem", "sprites:itemIcon:tomato"},

                new string[] {"Textures/Sprites/InteractionIcons/defaultInteraction", "sprites:interactionIcon:default"},
                new string[] {"Textures/Sprites/InteractionIcons/maintnenceComplete", "sprites:interactionIcon:maintnenceComplete"},
                new string[] {"Textures/Sprites/InteractionIcons/farmlandCropReady", "sprites:interactionIcon:farmlandCropReady"},

                new string[] {"Textures/Sprites/UpgradeIcons/farmlandCrop", "sprites:upgradeIcon:farmlandCrop"},
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandQuantity", "sprites:upgradeIcon:farmlandQuantity"},
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandSoilQuality", "sprites:upgradeIcon:farmlandSoilQuality"},
                new string[] {"Textures/Sprites/UpgradeIcons/farmlandSoilRetension", "sprites:upgradeIcon:farmlandSoilRetension"},

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
