using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedVariables : MonoBehaviour
{
    public enum TileLinkRelation{
    Hi,
    Mid,
    Low
}

    //Resource nodes
    public static Dictionary<string, int> nodeVariantCount = new Dictionary<string, int>{

        {"mudcave:rock", 1},
        {"mudcave:coal", 1},
        {"mudcave:copper", 1},
        {"mudcave:iron", 1},
    };

    public static Dictionary<string, Dictionary<string, float>> dungeonResources = new Dictionary<string, Dictionary<string, float>>{

        {"mudcave", new Dictionary<string, float>{
            {"mudcave:coal", 0.4f},
            {"mudcave:copper", 0.4f},
            {"mudcave:iron", 0.2f},
        }},
    };

    public static Dictionary<string, Dictionary<string, float>> dungeonFillers = new Dictionary<string, Dictionary<string, float>>{

        {"mudcave", new Dictionary<string, float>{
            {"mudcave:rock", 1f},
        }},
    };

    //Tool lists
    public static string[] tools = new string[] {"inspect", "wateringcan", "axe", "pickaxe"};
    public static Dictionary<string, string[]> toolOptions = new Dictionary<string, string[]>{

        {"inspect", new string[] {"build","move","destroy"}},
        {"wateringcan", new string[] {}},
        {"axe", new string[] {}},
        {"pickaxe", new string[] {}},
    };

    public static Dictionary<string, string> toolIcons = new Dictionary<string, string>{

        {"inspect", "sprites:buttonIcons:inspect"},
        {"wateringcan", "sprites:buttonIcons:wateringcan"},
        {"axe", "sprites:buttonIcons:axe"},
        {"pickaxe", "sprites:buttonIcons:pickaxe"},
        {"build", "sprites:buttonIcons:build"},
        {"move", "sprites:buttonIcons:move"},
        {"destroy", "sprites:buttonIcons:destroy"},
    };

    //Tool use
    public static Dictionary<string, float> miningTime = new Dictionary<string, float>
    {
        {"tree:oak", 5f},
        {"tree:pine", 5f},
        {"tree:cherry", 5f},
    };

    //Items
    public static Dictionary<string, string> itemNames = new Dictionary<string, string>{

        {"money", "Nibbles"},

        {"carrot", "Carrot"},
        {"potato", "Potato"},
        {"tomato", "Tomato"},

        {"pinkhydra", "Pink hydrangea"},
        {"bluehydra", "Blue hydrangea"},
        {"pinkorchid", "Pink orchid"},
        {"blueorchid", "Blue orchid"},
        {"whiteorchid", "White orchid"},
        {"orangeorchid", "Orange orchid"},

        {"honey", "Honey"},
        {"hydrahoney", "Hydrangea honey"},
        {"orchidhoney", "Orchid honey"},

        {"coal", "Coal"},
        {"copperOre", "Copper Ore"},
        {"copper", "Copper Bar"},
        {"ironOre", "Iron Ore"},
        {"iron", "Iron Bar"},
        {"clay", "Clay"},
        {"brick", "Brick"},

        {"wood", "Wood"},
        {"oakSappling", "Acorn"},
        {"pineSappling", "Pinecone"},
        {"cherrySappling", "Cherry sappling"},
    };

    public static Dictionary<string, string> itemDescription = new Dictionary<string, string>{

        {"money", "The accepted currency on Cottontail meadows! Can be used for purchases and investments."},

        {"carrot", "A carrot! Very good for your eyesight."},
        {"potato", "Potatoes are a very good source of energy."},
        {"tomato", "Some like it in their salads."},

        {"pinkhydra", "TODO"},
        {"bluehydra", "TODO"},
        {"pinkorchid", "TODO"},
        {"blueorchid", "TODO"},
        {"whiteorchid", "TODO"},
        {"orangeorchid", "TODO"},

        {"honey", "TODO"},
        {"hydrahoney", "TODO"},
        {"orchidhoney", "TODO"},

        {"coal", "A very energetic source of power."},
        {"copperOre", "Unrefined copper, smelt it to make a bar."},
        {"copper", "Smelted copper, a very good conductive material."},
        {"ironOre", "Unrefined iron, smelt it to make a bar."},
        {"iron", "Smelted iron, a relatively common and strong material."},
        {"clay", "Maleable clay that can be used for many purposes."},
        {"brick", "Hardened clay that is mainly used for construction."},

        {"wood", "TODO"},
        {"oakSappling", "TODO"},
        {"pineSappling", "TODO"},
        {"cherrySappling", "TODO"},
    };


    //Tile interactions
    public static Dictionary<string, Dictionary<string, TileLinkRelation>> tileLinkRelationship =
     new Dictionary<string, Dictionary<string, TileLinkRelation>>{

        {"path:dirt", new Dictionary<string, TileLinkRelation>{
            {"path:dirt", TileLinkRelation.Mid},
            {"path:gravel", TileLinkRelation.Hi},
            {"path:brick", TileLinkRelation.Hi},
        }},
        {"path:brick", new Dictionary<string, TileLinkRelation>{
            {"path:dirt", TileLinkRelation.Low},
            {"path:gravel", TileLinkRelation.Low},
            {"path:brick", TileLinkRelation.Mid},
        }},
        {"path:gravel", new Dictionary<string, TileLinkRelation>{
            {"path:dirt", TileLinkRelation.Low},
            {"path:gravel", TileLinkRelation.Mid},
            {"path:brick", TileLinkRelation.Hi},
        }},
        {"path:water", new Dictionary<string, TileLinkRelation>{
            {"path:water", TileLinkRelation.Mid},
        }}

     };

     public static Dictionary<string, List<string>> tilesOfSignificance = new Dictionary<string, List<string>>{

        {"path:dirt", new List<string>{"path:dirt", "path:gravel", "path:brick"}},
        {"path:brick", new List<string>{"path:dirt", "path:gravel", "path:brick"}},
        {"path:gravel", new List<string>{"path:dirt", "path:gravel", "path:brick"}},
        {"path:water", new List<string>{"path:water"}},
     };

     public static Dictionary<string, List<string>> tileRestrictions = new Dictionary<string, List<string>>{
        
        {"farmland", new List<string>{"tile:grass", "path:dirt"}},
        {"flowerbed", new List<string>{"tile:grass", "path:dirt"}},
     };

    public static Dictionary<string, string> interactionIcons = new Dictionary<string, string>{

        {"farmland:finishUpgrade", "sprites:interactionIcon:maintnenceComplete"},
        {"farmland:harvestPlant", "sprites:interactionIcon:farmlandCropReady"},

        {"flowerbed:finishUpgrade", "sprites:interactionIcon:maintnenceComplete"},
        {"flowerbed:harvestPlant", "sprites:interactionIcon:flowerbedFlowerReady"},

        {"beehouse:harvestResource", "sprites:interactionIcon:beehouseHoneyReady"},

        {"furnace:finishUpgrade", "sprites:interactionIcon:maintnenceComplete"},
        {"furnace:harvestResource", "sprites:interactionIcon:furnaceResourceReady"},
    };

    public static Dictionary<string, string> decorNames = new Dictionary<string, string>{

        {"tile", "Tiles"},
        {"tile:tile:grass", "Grass"},
        {"tile:path:dirt", "Dirt path"},
        {"tile:path:brick", "Brick path"},
        {"tile:path:water", "Water tile"},
        {"tile:path:gravel", "Gravel path"},

        {"tree", "Trees"},
        {"tree:oak", "Oak tree"},
        {"tree:pine", "Pine tree"},
        {"tree:cherry", "Cherry tree"},

        {"fruittree", "Fruit trees"},
        {"fruittree:apple", "Apple tree"}
    };

    public static Dictionary<string, string[]> decorations = new Dictionary<string, string[]>{

        {"tile", new string[] {
            "tile:tile:grass",
            "tile:path:dirt",
            "tile:path:gravel",
            "tile:path:brick",
            "tile:path:water"
        }},
        {"tree", new string[] {
            "tree:oak",
            "tree:pine",
        //    "tree:cherry"
        }},
        {"fruittree", new string[] {
            "fruittree:apple"
        }},
    };

    public static Dictionary<string, Item[]> decorCost = new Dictionary<string, Item[]>{

        {"tile:tile:grass", new Item[] {new Item("money", 0)}},
        {"tile:path:dirt", new Item[] {new Item("money", 50)}},
        {"tile:path:brick", new Item[] {new Item("money", 100)}},
        {"tile:path:water", new Item[] {new Item("money", 100)}},
        {"tile:path:gravel", new Item[] {new Item("money", 50)}},

        {"tree:oak", new Item[] {new Item("oakSappling", 1)}},
        {"tree:pine", new Item[] {new Item("pineSappling", 1)}},
        {"tree:cherry", new Item[] {new Item("money", 250)}},

        {"fruittree:apple", new Item[] {new Item("money", 2000)}},
    };

    public static Dictionary<string, string> decorDescriptions = new Dictionary<string, string>{

        {"tile", "Spice up your pathing and groundwork!"},
        {"tile:tile:grass", "Some nice and pleasent looking grass."},    
        {"tile:path:dirt", "A trotten dirt path for a easier walk."},    
        {"tile:path:brick", "A carefully built brick path for your main roads."},
        {"tile:path:water", "Make small lakes or still rivers with water."},
        {"tile:path:gravel", "Aninhas."},

        {"tree", "Trees you can grow for wood or decoration."},
        {"tree:oak", "An oak tree."},
        {"tree:pine", "A pine tree."},
        {"tree:cherry", "A cute cherry tree."},

        {"fruittree", "Fruit trees you can grow for fruit."},
        {"fruittree:apple", "A delicious apple tree."}
    };

    public static Dictionary<string, string> decorIcons = new Dictionary<string, string>{

        {"tile:tile:grass", "sprites:decorIcon:grass"},
        {"tile:path:dirt", "sprites:decorIcon:dirtPath"},
        {"tile:path:brick", "sprites:decorIcon:brickPath"},
        {"tile:path:gravel", "sprites:decorIcon:gravelPath"},
        {"tile:path:water", "sprites:decorIcon:water"},

        {"tree:oak", "sprites:decorIcon:oakTree"},
        {"tree:pine", "sprites:decorIcon:pineTree"},
    };

    public static Dictionary<string, string> structureNames = new Dictionary<string, string>{

        {"farmland", "Farmland"},
        {"flowerbed", "Flower bed"},
        {"furnace", "Furnace"},
        {"beehouse", "Bee house"},
    };

    public static Dictionary<string, string> structureResourceName = new Dictionary<string, string>{

        {"farmland", "Crop"},
        {"flowerbed", "Flower"},
        {"furnace", "Resource"},
        {"beehouse", "Honey"},
    };

    //Production proprieties
    public static Dictionary<string, float> resourceProductionTimes = new Dictionary<string, float>
    {
        {"farmland:carrot0", 600f},
        {"farmland:potato0", 900f},
        {"farmland:tomato0", 1500f},
        {"farmland:tomato1", 1000f}
    };

    public static Dictionary<string, int> resourceFinalStage = new Dictionary<string, int>
    {
        {"farmland:carrot", 0},
        {"farmland:potato", 0},
        {"farmland:tomato", 1},

        {"flowerbed:pinkhydra", 1},
        {"flowerbed:bluehydra", 1},
        {"flowerbed:pinkorchid", 1},
        {"flowerbed:blueorchid", 1},
        {"flowerbed:whiteorchid", 1},
        {"flowerbed:orangeorchid", 1},

        {"beehouse", 2},

        {"tree:oak", 2},
        {"tree:pine", 0},
        
    };

    public static Dictionary<string, int> stageToResetTo = new Dictionary<string, int>
    {
        {"farmland:carrot", 0},
        {"farmland:potato", 0},
        {"farmland:tomato", 1},

        {"flowerbed:pinkhydra", 1},
        {"flowerbed:bluehydra", 1},
        {"flowerbed:pinkorchid", 0},
        {"flowerbed:blueorchid", 0},
        {"flowerbed:whiteorchid", 0},
        {"flowerbed:orangeorchid", 0},

        {"beehouse", 0},
    };

    //Build
    public static Dictionary<string, string> structureDescriptions = new Dictionary<string, string>{

        {"farmland", "A patch of farmland that can be used to grow vegetables and other crops."},
        {"flowerbed", "A flower bed to grow your flower on."},
        {"furnace", "A large furnace to smelt your minerals."},
        {"beehouse", "A bee house full of bees that will produce honey based on the flower near them."}
    };

    public static Dictionary<string, Item[]> structureBuildRequirements = new Dictionary<string, Item[]>{

        {"farmland", new Item[] {
            new Item("money", 1000)
        }},
        {"flowerbed", new Item[] {
            new Item("money", 1000)
        }},
        {"furnace", new Item[] {
            new Item("money", 1000)
        }},
        {"beehouse", new Item[] {
            new Item("money", 1000)
        }},
        {"basichouse", new Item[] {
            new Item("money", 0)
        }},
    };

    public static Dictionary<string, bool> isDecor = new Dictionary<string, bool>{
        
        {"farmland", false},
        {"flowerbed", false},
        {"furnace", false},
        {"beehouse", false},
        {"basichouse", false},

        {"tree", true},
        {"fruittree", true}
    };

    public static Dictionary<string, string> structureIcons = new Dictionary<string, string>{

        {"farmland", "sprites:structureIcon:farmland"},
        {"flowerbed", "sprites:structureIcon:flowerbed"},
        {"furnace", "sprites:structureIcon:furnace"},
        {"beehouse", "sprites:structureIcon:beehouse"},
        {"tile", "sprites:structureIcon:tile"},
        {"tree", "sprites:structureIcon:tree"},
    };

    public static string[] structureBuildList = new string[] {"farmland", "flowerbed", "furnace", "beehouse"};
    public static string[] decorBuildList = new string[] {"tile", "tree"}; //Add fruittree back

    //UpgradeEffects
    public static Dictionary<string, int> harvestCount = new Dictionary<string, int>{

        {"farmland:quantity0", 10},
        {"farmland:quantity1", 20},
        {"farmland:quantity2", 50},

        {"flowerbed:quantity0", 5},
        {"flowerbed:quantity1", 10},
        {"flowerbed:quantity2", 25},

        {"beehouse", 5},

        {"furnace:quantity0", 2},
        {"furnace:quantity1", 5},
        {"furnace:quantity2", 10},
        {"furnace:quantity3", 20},

        {"tree:oak", 20},
        {"tree:pine", 20},
        {"tree:cherry", 20},
    };

    public static Dictionary<string, float> harvestSpeed = new Dictionary<string, float>{

        {"farmland:soilQuality0", 1f},
        {"farmland:soilQuality1", 1.5f},
        {"farmland:soilQuality2", 2f},
        {"farmland:soilQuality3", 3f},

        {"flowerbed:soilQuality0", 1f},
        {"flowerbed:soilQuality1", 1.5f},
        {"flowerbed:soilQuality2", 2f},
        {"flowerbed:soilQuality3", 3f},

        {"furnace:speed0", 1f},
        {"furnace:speed1", 1.5f},
        {"furnace:speed2", 2f},
        {"furnace:speed3", 3f},
    };

    //Upgrades
    public static Dictionary<string, string> speedUpgradeId = new Dictionary<string, string>{

        {"structure:farmland", "soilQuality"},
        {"structure:flowerbed", "soilQuality"},
        {"structure:furnace", "speed"}
    };

    public static Dictionary<string, Item[]> resourceCost = new Dictionary<string, Item[]>{

        {"farmland:empty", new Item[] {new Item("money", 0)}},
        {"farmland:carrot", new Item[] {new Item("money", 250)}},
        {"farmland:potato", new Item[] {new Item("money", 1000)}},
        {"farmland:wheat", new Item[] {new Item("money", 0)}},
        {"farmland:cabbage", new Item[] {new Item("money", 0)}},
        {"farmland:tomato", new Item[] {new Item("money", 5000)}},
        {"farmland:mushroom", new Item[] {new Item("money", 0)}},
        {"farmland:strawberry", new Item[] {new Item("money", 0)}},
        {"farmland:pumpkin", new Item[] {new Item("money", 0)}},

        {"flowerbed:empty", new Item[] {new Item("money", 0)}},
        {"flowerbed:pinkhydra", new Item[] {new Item("money", 250)}},
        {"flowerbed:bluehydra", new Item[] {new Item("money", 250)}},
        {"flowerbed:pinkorchid", new Item[] {new Item("money", 250)}},
        {"flowerbed:blueorchid", new Item[] {new Item("money", 250)}},
        {"flowerbed:whiteorchid", new Item[] {new Item("money", 250)}},
        {"flowerbed:orangeorchid", new Item[] {new Item("money", 250)}},

        {"furnace:copper", new Item[] {new Item("copperOre", 2), new Item("coal", 1)}},
        {"furnace:iron", new Item[] {new Item("ironOre", 2), new Item("coal", 1)}},
        {"furnace:brick", new Item[] {new Item("clay", 5), new Item("coal", 1)}},
        {"furnace:gold", new Item[] {new Item("goldOre", 2), new Item("coal", 1)}},
        {"furnace:tungsten", new Item[] {new Item("tungstenOre", 2), new Item("coal", 1)}},
        {"furnace:glass", new Item[] {new Item("silica", 5), new Item("coal", 1)}},
        {"furnace:titanium", new Item[] {new Item("titaniumOre", 2), new Item("coal", 1)}},
        {"furnace:niobium", new Item[] {new Item("niobiumOre", 2), new Item("coal", 1)}},
    };

    public static Dictionary<string, string> flowerToHoney = new Dictionary<string, string>{

        {"flowerbed:pinkhydra", "hydrahoney"},
        {"flowerbed:bluehydra", "hydrahoney"},
        {"flowerbed:pinkorchid", "orchidhoney"},
        {"flowerbed:blueorchid", "orchidhoney"},
        {"flowerbed:whiteorchid", "orchidhoney"},
        {"flowerbed:orangeorchid", "orchidhoney"},

    };

    public static Dictionary<string, string> resourceDescriptions = new Dictionary<string, string>{

        {"farmland:empty", "Clear the farmland of any crops."},
        {"farmland:carrot", "The most basic of all crops, the carrot, great for eyesigh and for bunny happyness."},
        {"farmland:potato", "The energetic potato, grows a flower on its last stage."},
        {"farmland:wheat", "TODO"},
        {"farmland:cabbage", "TODO"},
        {"farmland:tomato", "Red, shiny and succulent."},
        {"farmland:mushroom", "TODO"},
        {"farmland:strawberry", "TODO"},
        {"farmland:pumpkin", "TODO"},    

        {"flowerbed:empty", "Clear the flower bed of any flowers."},
        {"flowerbed:pinkhydra", "TODO"},
        {"flowerbed:bluehydra", "TODO"},
        {"flowerbed:pinkorchid", "TODO"},
        {"flowerbed:blueorchid", "TODO"},
        {"flowerbed:whiteorchid", "TODO"},
        {"flowerbed:orangeorchid", "TODO"},

        {"furnace:copper", "A bar of copper, a cheap and conductive material."},
        {"furnace:iron", "A bar of iron, a common and strong material."},
        {"furnace:brick", "Very versatile and common building material."},
        {"furnace:gold", "TODO"},
        {"furnace:tungsten", "TODO"},
        {"furnace:glass", "TODO"},
        {"furnace:titanium", "TODO"},
        {"furnace:niobium", "TODO"},
    };
    public static Dictionary<string, string> resourceNames = new Dictionary<string, string>{

        {"farmland:empty", "Clear"},
        {"farmland:carrot", "Carrot"},
        {"farmland:potato", "Potato"},
        {"farmland:wheat", "Wheat"},
        {"farmland:cabbage", "Cabbage"},
        {"farmland:tomato", "Tomato"},
        {"farmland:mushroom", "Mushroom"},
        {"farmland:strawberry", "Strawberry"},
        {"farmland:pumpkin", "Pumpkin"},

        {"flowerbed:empty", "Clear"},
        {"flowerbed:pinkhydra", "Pink hydrangea"},
        {"flowerbed:bluehydra", "Blue hydrangea"},
        {"flowerbed:pinkorchid", "Pink orchid"},
        {"flowerbed:blueorchid", "Blue orchid"},
        {"flowerbed:whiteorchid", "White orchid"},
        {"flowerbed:orangeorchid", "Orange orchid"},

        {"furnace:copper", "Copper bar"},
        {"furnace:iron", "Iron bar"},
        {"furnace:brick", "Hardened clay"},
        {"furnace:gold", "Gold bar"},
        {"furnace:tungsten", "Tungsten bar"},
        {"furnace:glass", "Glass"},
        {"furnace:titanium", "Titanium bar"},
        {"furnace:niobium", "Niobium bar"},
    }; 

    public static Dictionary<string, bool> complexResources = new Dictionary<string, bool>{

        {"structure:farmland:resource", false},
        {"structure:flowerbed:resource", false},
        {"structure:furnace:resource", true},

        {"tile", false},
        {"tree", false},
        {"fruittree", false},

    };

     public static Dictionary<string, bool> multipleQueue = new Dictionary<string, bool>{

        {"structure:farmland:resource", false},
        {"structure:flowerbed:resource", false},
        {"structure:furnace:resource", true},

    };

    public static Dictionary<string, float> proccessTimes = new Dictionary<string, float>
    {
        {"farmland:carrot0", 600f},
        {"farmland:potato0", 900f},
        {"farmland:tomato0", 1500f},
        {"farmland:tomato1", 1000f},

        {"flowerbed:pinkhydra0", 1000f},
        {"flowerbed:pinkhydra1", 500f},
        {"flowerbed:bluehydra0", 1000f},
        {"flowerbed:bluehydra1", 500f},
        {"flowerbed:pinkorchid0", 400f},
        {"flowerbed:pinkorchid1", 400f},
        {"flowerbed:blueorchid0", 400f},
        {"flowerbed:blueorchid1", 400f},
        {"flowerbed:whiteorchid0", 400f},
        {"flowerbed:whiteorchid1", 400f},
        {"flowerbed:orangeorchid0", 400f},
        {"flowerbed:orangeorchid1", 400f},

        {"beehouse0", 500f},
        {"beehouse1", 500f},
        {"beehouse2", 0f},

        {"furnace:copper", 200f},
        {"furnace:iron", 400f},
        {"furnace:brick", 200f},
        {"furnace:gold", 600f},
        {"furnace:tungsten", 1000f},
        {"furnace:glass", 600f},
        {"furnace:titanium", 1500f},
        {"furnace:niobium", 3000f},

        {"tree:oak0", 200f},
        {"tree:oak1", 200f},
        {"tree:oak2", 200f},
        {"tree:pine0", 600f},
        {"tree:cherry0", 200f},
        {"tree:cherry1", 200f},
        {"tree:cherry2", 200f},
    };

    public static Dictionary<string, string[]> resources = new Dictionary<string, string[]>{

        {"structure:farmland:resource0", new string[] {
            "farmland:empty",
            "farmland:carrot",
            "farmland:potato",
            "farmland:wheat",
            "farmland:cabbage",
            "farmland:tomato",
            "farmland:mushroom",
            "farmland:strawberry",
            "farmland:pumpkin"
        }},

        {"structure:flowerbed:resource0", new string[] {
            "flowerbed:empty",
            "flowerbed:pinkhydra",
            "flowerbed:bluehydra",
            "flowerbed:pinkorchid",
            "flowerbed:blueorchid",
            "flowerbed:whiteorchid",
            "flowerbed:orangeorchid",
        }},

        {"structure:furnace:resource0", new string[] {
            "furnace:copper",
            "furnace:iron",
            "furnace:brick",
        }},
        {"structure:furnace:resource1", new string[] {
            "furnace:copper",
            "furnace:iron",
            "furnace:brick",

            "furnace:gold",
            "furnace:tungsten",
            "furnace:glass",
        }},
        {"structure:furnace:resource2", new string[] {
            "furnace:copper",
            "furnace:iron",
            "furnace:brick",

            "furnace:gold",
            "furnace:tungsten",
            "furnace:glass",

            "furnace:titanium",
            "furnace:niobium",
        }},
    };
    public static Dictionary<string, int> upgradeTimes = new Dictionary<string, int>{

        {"structure:farmland:tier0", 600},
        {"structure:farmland:quantity1", 600},
        {"structure:farmland:quantity2", 7200},
        {"structure:farmland:soilQuality1", 600},
        {"structure:farmland:soilQuality2", 7200},
        {"structure:farmland:soilQuality3", 43200},
        {"structure:farmland:soilRetension1", 600},
        {"structure:farmland:soilRetension2", 7200},
        {"structure:farmland:soilRetension3", 43200},

        {"structure:flowerbed:tier0", 600},
        {"structure:flowerbed:quantity1", 600},
        {"structure:flowerbed:quantity2", 7200},
        {"structure:flowerbed:soilQuality1", 600},
        {"structure:flowerbed:soilQuality2", 7200},
        {"structure:flowerbed:soilQuality3", 43200},
        {"structure:flowerbed:soilRetension1", 600},
        {"structure:flowerbed:soilRetension2", 7200},
        {"structure:flowerbed:soilRetension3", 43200},

        {"structure:furnace:tier0", 600},
        {"structure:furnace:tier1", 2400},
        {"structure:furnace:tier2", 7200},
        {"structure:furnace:quantity1", 600},
        {"structure:furnace:quantity2", 7200},
        {"structure:furnace:quantity3", 43200},
        {"structure:furnace:speed1", 600},
        {"structure:furnace:speed2", 7200},
        {"structure:furnace:speed3", 43200},
    };
    public static Dictionary<string, string> upgradeDescriptions = new Dictionary<string, string>{

        {"structure:farmland:resource", "Choose a crop you wish to sow on this farmland."},
        {"structure:farmland:quantity1", "Increases the amount of crops being grown on the farmland."},
        {"structure:farmland:quantity2", "Increases the amount of crops being grown on the farmland."},
        {"structure:farmland:soilQuality1", "Increases the rate at which crops are grown on the farmland."},
        {"structure:farmland:soilQuality2", "Increases the rate at which crops are grown on the farmland."},
        {"structure:farmland:soilQuality3", "Increases the rate at which crops are grown on the farmland."},
        {"structure:farmland:soilRetension1", "Increases the soil retension of the farmland, retaining water for longer."},
        {"structure:farmland:soilRetension2", "Increases the soil retension of the farmland, retaining water for longer."},
        {"structure:farmland:soilRetension3", "Increases the soil retension of the farmland, retaining water for longer."},

        {"structure:flowerbed:resource", "Choose a flower you wish to plant on the flower bed."},
        {"structure:flowerbed:quantity1", "Increases the amount of flowers being grown on the flower bed."},
        {"structure:flowerbed:quantity2", "Increases the amount of flowers being grown on the flower bed."},
        {"structure:flowerbed:soilQuality1", "Increases the rate at which flowers are grown on the flower bed."},
        {"structure:flowerbed:soilQuality2", "Increases the rate at which flowers are grown on the flower bed."},
        {"structure:flowerbed:soilQuality3", "Increases the rate at which flowers are grown on the flower bed."},
        {"structure:flowerbed:soilRetension1", "Increases the soil retension of the flower bed, retaining water for longer."},
        {"structure:flowerbed:soilRetension2", "Increases the soil retension of the flower bed, retaining water for longer."},
        {"structure:flowerbed:soilRetension3", "Increases the soil retension of the flower bed, retaining water for longer."},

        {"structure:furnace:resource", "Choose a resource to cook up and refine."},
        {"structure:furnace:tier1", "Increase the furnace tier, unlocking new refinement possibilities."},
        {"structure:furnace:tier2", "Inscrease the furnace tier, unlocking new refinement possibilities."},
        {"structure:furnace:quantity1", "Increase the amount of resources to process at once."},
        {"structure:furnace:quantity2", "Increase the amount of resources to process at once."},
        {"structure:furnace:quantity3", "Increase the amount of resources to process at once."},
        {"structure:furnace:speed1", "Increase the speed of the processing time."},
        {"structure:furnace:speed2", "Increase the speed of the processing time."},
        {"structure:furnace:speed3", "Increase the speed of the processing time."},
    };
    public static Dictionary<string, string> upgradeNames = new Dictionary<string, string>{

        {"structure:farmland:resource", "Plant Crop"},
        {"structure:farmland:quantity1", "Quantity 1"},
        {"structure:farmland:quantity2", "Quantity 2"},
        {"structure:farmland:soilQuality1", "Soil Quality 1"},
        {"structure:farmland:soilQuality2", "Soil Quality 2"},
        {"structure:farmland:soilQuality3", "Soil Quality 3"},
        {"structure:farmland:soilRetension1", "Soil Retension 1"},
        {"structure:farmland:soilRetension2", "Soil Retension 2"},
        {"structure:farmland:soilRetension3", "Soil Retension 3"},

        {"structure:flowerbed:resource", "Plant Flower"},
        {"structure:flowerbed:quantity1", "Quantity 1"},
        {"structure:flowerbed:quantity2", "Quantity 2"},
        {"structure:flowerbed:soilQuality1", "Soil Quality 1"},
        {"structure:flowerbed:soilQuality2", "Soil Quality 2"},
        {"structure:flowerbed:soilQuality3", "Soil Quality 3"},
        {"structure:flowerbed:soilRetension1", "Soil Retension 1"},
        {"structure:flowerbed:soilRetension2", "Soil Retension 2"},
        {"structure:flowerbed:soilRetension3", "Soil Retension 3"},

        {"structure:furnace:resource", "Refine"},
        {"structure:furnace:tier1", "Tier 2"},
        {"structure:furnace:tier2", "Tier 3"},
        {"structure:furnace:quantity1", "Quanitity 1"},
        {"structure:furnace:quantity2", "Quanitity 2"},
        {"structure:furnace:quantity3", "Quanitity 3"},
        {"structure:furnace:speed1", "Speed 1"},
        {"structure:furnace:speed2", "Speed 2"},
        {"structure:furnace:speed3", "Speed 3"},
    };
    public static Dictionary<string, string> upgradeIcons = new Dictionary<string, string>{

        {"structure:farmland:resource", "sprites:upgradeIcon:farmlandCrop"},
        {"structure:farmland:quantity1", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:farmland:quantity2", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:farmland:soilQuality1", "sprites:upgradeIcon:farmlandSoilQuality"},
        {"structure:farmland:soilQuality2", "sprites:upgradeIcon:farmlandSoilQuality"},
        {"structure:farmland:soilQuality3", "sprites:upgradeIcon:farmlandSoilQuality"},
        {"structure:farmland:soilRetension1", "sprites:upgradeIcon:farmlandSoilRetension"},
        {"structure:farmland:soilRetension2", "sprites:upgradeIcon:farmlandSoilRetension"},
        {"structure:farmland:soilRetension3", "sprites:upgradeIcon:farmlandSoilRetension"},

        {"structure:flowerbed:resource", "sprites:upgradeIcon:flowerbedFlower"},
        {"structure:flowerbed:quantity1", "sprites:upgradeIcon:flowerbedQuantity"},
        {"structure:flowerbed:quantity2", "sprites:upgradeIcon:flowerbedQuantity"},
        {"structure:flowerbed:soilQuality1", "sprites:upgradeIcon:flowerbedSoilQuality"},
        {"structure:flowerbed:soilQuality2", "sprites:upgradeIcon:flowerbedSoilQuality"},
        {"structure:flowerbed:soilQuality3", "sprites:upgradeIcon:flowerbedSoilQuality"},
        {"structure:flowerbed:soilRetension1", "sprites:upgradeIcon:farmlandSoilRetension"},
        {"structure:flowerbed:soilRetension2", "sprites:upgradeIcon:farmlandSoilRetension"},
        {"structure:flowerbed:soilRetension3", "sprites:upgradeIcon:farmlandSoilRetension"},

        {"structure:furnace:resource", "sprites:upgradeIcon:furnaceResource"},
        {"structure:furnace:tier1", "sprites:upgradeIcon:furnaceTier"},
        {"structure:furnace:tier2", "sprites:upgradeIcon:furnaceTier"},
        {"structure:furnace:quantity1", "sprites:upgradeIcon:furnaceQuantity"},
        {"structure:furnace:quantity2", "sprites:upgradeIcon:furnaceQuantity"},
        {"structure:furnace:quantity3", "sprites:upgradeIcon:furnaceQuantity"},
        {"structure:furnace:speed1", "sprites:upgradeIcon:furnaceSpeed"},
        {"structure:furnace:speed2", "sprites:upgradeIcon:furnaceSpeed"},
        {"structure:furnace:speed3", "sprites:upgradeIcon:furnaceSpeed"},
    };
    public static Dictionary<string, Item[]> upgradeRequirements = new Dictionary<string, Item[]>{

        {"structure:farmland:quantity1", new Item[] {
            new Item("money", 1000)
        }},
        {"structure:farmland:quantity2", new Item[] {
            new Item("money", 50000)
        }},
        {"structure:farmland:soilQuality1", new Item[] {
            new Item("money", 500)
        }},
        {"structure:farmland:soilQuality2", new Item[] {
            new Item("money", 10000)
        }},
        {"structure:farmland:soilQuality3", new Item[] {
            new Item("money", 500000)
        }},
        {"structure:farmland:soilRetension1", new Item[] {
            new Item("money", 250)
        }},
        {"structure:farmland:soilRetension2", new Item[] {
            new Item("money", 5000)
        }},
        {"structure:farmland:soilRetension3", new Item[] {
            new Item("money", 250000)
        }},


        {"structure:flowerbed:quantity1", new Item[] {
            new Item("money", 1000)
        }},
        {"structure:flowerbed:quantity2", new Item[] {
            new Item("money", 50000)
        }},
        {"structure:flowerbed:soilQuality1", new Item[] {
            new Item("money", 500)
        }},
        {"structure:flowerbed:soilQuality2", new Item[] {
            new Item("money", 10000)
        }},
        {"structure:flowerbed:soilQuality3", new Item[] {
            new Item("money", 500000)
        }},
        {"structure:flowerbed:soilRetension1", new Item[] {
            new Item("money", 250)
        }},
        {"structure:flowerbed:soilRetension2", new Item[] {
            new Item("money", 5000)
        }},
        {"structure:flowerbed:soilRetension3", new Item[] {
            new Item("money", 250000)
        }},


        {"structure:furnace:tier1", new Item[] {
            new Item("money", 1000)
        }},
        {"structure:furnace:tier2", new Item[] {
            new Item("money", 50000)
        }},
        {"structure:furnace:quantity1", new Item[] {
            new Item("money", 500)
        }},
        {"structure:furnace:quantity2", new Item[] {
            new Item("money", 10000)
        }},
        {"structure:furnace:quantity3", new Item[] {
            new Item("money", 500000)
        }},
        {"structure:furnace:speed1", new Item[] {
            new Item("money", 250)
        }},
        {"structure:furnace:speed2", new Item[] {
            new Item("money", 5000)
        }},
        {"structure:furnace:speed3", new Item[] {
            new Item("money", 250000)
        }}
    };
    public static Dictionary<string, int> maxUpgrade = new Dictionary<string, int>{

        {"structure:farmland:quantity", 2},
        {"structure:farmland:soilQuality", 3},
        {"structure:farmland:soilRetension", 3},

        {"structure:flowerbed:quantity", 2},
        {"structure:flowerbed:soilQuality", 3},
        {"structure:flowerbed:soilRetension", 3},

        {"structure:furnace:tier", 2},
        {"structure:furnace:quantity", 3},
        {"structure:furnace:speed", 3},
    };
    public static Dictionary<string, string[]> upgradeCategories = new Dictionary<string, string[]>{

        {"structure:farmland", new string[] {"resource","quantity","soilQuality","soilRetension"}},
        {"structure:flowerbed", new string[] {"resource","quantity","soilQuality","soilRetension"}},
        {"structure:furnace", new string[] {"resource","tier","quantity","speed"}},
    };
}
