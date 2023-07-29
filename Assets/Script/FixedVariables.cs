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

    public static Dictionary<string, string> interactionIcons = new Dictionary<string, string>{

        {"farmland:finishUpgrade", "sprites:interactionIcon:maintnenceComplete"},
        {"farmland:harvestPlant", "sprites:interactionIcon:farmlandCropReady"},
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
            "tree:cherry"
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

        {"tree:oak", new Item[] {new Item("money", 250)}},
        {"tree:pine", new Item[] {new Item("money", 250)}},
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

        {"tile", "Tiles"},
        {"ground:grass", "Grass"},
        {"path:dirt", "Dirt path"},
        {"path:brick", "Dirt path"},
        {"path:water", "Water tile"},
    };

    public static Dictionary<string, string> structureNames = new Dictionary<string, string>{

        {"farmland", "Farmland"},
        {"furnace", "Furnace"},
        {"beehouse", "Bee house"},
        {"flowerbed", "Flower bed"},
    };

    public static Dictionary<string, string> structureResourceName = new Dictionary<string, string>{

        {"farmland", "Crop"}
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

        {"tree:oak", 2},
    };

    //Build
    public static Dictionary<string, string> structureDescriptions = new Dictionary<string, string>{

        {"farmland", "A patch of farmland that can be used to grow vegetables and other crops."},
        {"furnace", "A large furnace to smelt your minerals."},
    };

    public static Dictionary<string, Item[]> structureBuildRequirements = new Dictionary<string, Item[]>{

        {"farmland", new Item[] {
            new Item("money", 1000)
        }},
        {"furnace", new Item[] {
            new Item("money", 1000)
        }},
        {"beehouse", new Item[] {
            new Item("money", 1000)
        }},
        {"flowerbed", new Item[] {
            new Item("money", 1000)
        }},
    };

    public static Dictionary<string, bool> isDecor = new Dictionary<string, bool>{
        
        {"farmland", false},
        {"furnace", false},
        {"beehouse", false},
        {"flowerbed", false},

        {"tree", true},
        {"fruittree", true}
    };

    public static Dictionary<string, string> structureIcons = new Dictionary<string, string>{

        {"farmland", "sprites:upgradeIcon:farmlandCrop"},
        {"furnace", "sprites:upgradeIcon:farmlandCrop"},
        {"beehouse", "sprites:upgradeIcon:farmlandCrop"},
        {"flowerbed", "sprites:upgradeIcon:farmlandCrop"},
    };

    public static string[] structureBuildList = new string[] {"farmland", "furnace", "beehouse", "flowerbed"};
    public static string[] decorBuildList = new string[] {"tile", "tree", "fruittree"};

    //UpgradeEffects
    public static Dictionary<string, int> harvestCount = new Dictionary<string, int>{

        {"farmland:quantity0", 10},
        {"farmland:quantity1", 20},
        {"farmland:quantity2", 50}
    };

    public static Dictionary<string, float> harvestSpeed = new Dictionary<string, float>{

        {"farmland:soilQuality0", 1f},
        {"farmland:soilQuality1", 1.5f},
        {"farmland:soilQuality2", 2f},
        {"farmland:soilQuality3", 3f},

        {"furnace:speed0", 1f},
        {"furnace:speed1", 1.5f},
        {"furnace:speed2", 2f},
        {"furnace:speed3", 3f},
    };

    //Upgrades
    public static Dictionary<string, string> speedUpgradeId = new Dictionary<string, string>{

        {"structure:farmland", "soilQuality"},
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

        {"furnace:copper", "A bar of copper, a cheap and conductive material."},
        {"furnace:iron", "A bar of iron, a common and strong material."},
        {"furnace:clay", "Very versatile and common building material."},
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

        {"furnace:copper", "Copper bar"},
        {"furnace:iron", "Iron bar"},
        {"furnace:clay", "Hardened clay"},
        {"furnace:gold", "Gold bar"},
        {"furnace:tungsten", "Tungsten bar"},
        {"furnace:glass", "Glass"},
        {"furnace:titanium", "Titanium bar"},
        {"furnace:niobium", "Niobium bar"},
    }; 

    public static Dictionary<string, bool> complexResources = new Dictionary<string, bool>{

        {"structure:farmland:resource", false},
        {"structure:furnace:resource", true},

    };

    public static Dictionary<string, float> proccessTimes = new Dictionary<string, float>
    {
        {"farmland:carrot0", 600f},
        {"farmland:potato0", 900f},
        {"farmland:tomato0", 1500f},
        {"farmland:tomato1", 1000f},

        {"furnace:copper", 200f},
        {"furnace:iron", 400f},
        {"furnace:clay", 200f},
        {"furnace:gold", 600f},
        {"furnace:tungsten", 1000f},
        {"furnace:glass", 600f},
        {"furnace:titanium", 1500f},
        {"furnace:niobium", 3000f},

        {"tree:oak0", 200f},
        {"tree:oak1", 200f},
        {"tree:oak2", 200f},
        {"tree:pine0", 200f},
        {"tree:pine1", 200f},
        {"tree:pine2", 200f},
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

        {"structure:furnace:resource0", new string[] {
            "furnace:copper",
            "furnace:iron",
            "furnace:clay",
        }},
        {"structure:furnace:resource1", new string[] {
            "furnace:copper",
            "furnace:iron",
            "furnace:clay",

            "furnace:gold",
            "furnace:tungsten",
            "furnace:glass",
        }},
        {"structure:furnace:resource2", new string[] {
            "furnace:copper",
            "furnace:iron",
            "furnace:clay",

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

        {"structure:furnace:resource", "sprites:upgradeIcon:farmlandCrop"},
        {"structure:furnace:tier1", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:tier2", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:quantity1", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:quantity2", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:quantity3", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:speed1", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:speed2", "sprites:upgradeIcon:farmlandQuantity"},
        {"structure:furnace:speed3", "sprites:upgradeIcon:farmlandQuantity"},
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

        {"structure:furnace:tier", 2},
        {"structure:furnace:quantity", 3},
        {"structure:furnace:speed", 3},
    };
    public static Dictionary<string, string[]> upgradeCategories = new Dictionary<string, string[]>{

        {"structure:farmland", new string[] {"resource","quantity","soilQuality","soilRetension"}},
        {"structure:furnace", new string[] {"resource","tier","quantity","speed"}},
    };
}
