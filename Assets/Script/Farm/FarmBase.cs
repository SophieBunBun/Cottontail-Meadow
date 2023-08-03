using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBase : MonoBehaviour
{
    public static Dictionary<string, string> structureNames = new Dictionary<string, string>{

        {"farmland", "Farmland"},
        {"furnace", "Furnace"},
    };

    public static Dictionary<string, float> growthTimes = new Dictionary<string, float>
    {
        {"farmland:carrot0", 600f},
        {"farmland:potato0", 900f},
        {"farmland:tomato0", 1500f},
        {"farmland:tomato1", 1000f},

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

    public static Dictionary<string, int[]> structureSize = new Dictionary<string, int[]>
    {
        {"basichouse", new int[] {3,3}},
        {"furnace",  new int[]  {3,2}},
        {"farmland",  new int[]  {4,4}},
        {"beehouse",  new int[]  {1,1}},
        {"flowerbed",  new int[]  {2,2}},

        {"tree",  new int[]  {1,1}},
        {"fruittree",  new int[]  {3,3}},
    };

    public static StructureInstance createStructureInstance(string id, int[] anchorLocation, string resource){

        Dictionary<string, object> structurePropreties;
        switch (id){

            case "farmland":

                structurePropreties = new Dictionary<string, object>
                {
                    {"resource", resource},
                    {"age", 0f},
                    {"stage", 0},
                    {"hydration", 1f},

                    {"currentlyUpgrading", "tier0"},
                    {"currentInteraction", null},

                    {"maintenenceTime", (float)FixedVariables.upgradeTimes["structure:farmland:baseBuild"]},

                    {"tier", 0},
                    {"quantity", 0},
                    {"soilQuality", 0},
                    {"soilRetension", 0},

                    {"harvested", new Inventory()},
                    {"moneySpent", 1000}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);

            case "tree":

                structurePropreties = new Dictionary<string, object>
                {
                    {"resource", resource},
                    {"age", 0f},
                    {"stage", 0},

                    {"shaded", false}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);
        }

        return null;
    }

    public static StructureInstance createStructureInstance(string id, int[] anchorLocation){

        Dictionary<string, object> structurePropreties;
        switch (id){

            case "furnace":

                structurePropreties = new Dictionary<string, object>
                {
                    {"resource", null},
                    {"age", 0f},

                    {"currentlyUpgrading", "tier0"},
                    {"currentInteraction", null},

                    {"maintenenceTime", (float)FixedVariables.upgradeTimes["structure:furnace:tier0"]},

                    {"tier", 0},
                    {"quantity", 0},
                    {"speed", 0},

                    {"harvested", new Inventory()},
                    {"moneySpent", 1000}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);

            case "flowerbed":

                structurePropreties = new Dictionary<string, object>
                {
                    {"resource", null},
                    {"age", 0f},
                    {"stage", null},
                    {"hydration", 1f},

                    {"currentlyUpgrading", "tier0"},
                    {"currentInteraction", null},

                    {"maintenenceTime", (float)FixedVariables.upgradeTimes["structure:flowerbed:tier0"]},

                    {"tier", 0},
                    {"quantity", 0},
                    {"soilQuality", 0},
                    {"soilRetension", 0},

                    {"harvested", new Inventory()},
                    {"moneySpent", 1000}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);

            case "beehouse":

                structurePropreties = new Dictionary<string, object>
                {
                    {"age", 0f},
                    {"stage", 0},

                    {"currentInteraction", null},

                    {"harvested", new Inventory()},
                    {"moneySpent", 1000}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);

            case "basichouse":

                structurePropreties = new Dictionary<string, object>{};

                return new StructureInstance(id, structurePropreties, anchorLocation);

            case "farmland":

                structurePropreties = new Dictionary<string, object>
                {
                    {"resource", null},
                    {"age", 0f},
                    {"stage", null},
                    {"hydration", 1f},

                    {"currentlyUpgrading", "tier0"},
                    {"currentInteraction", null},

                    {"maintenenceTime", (float)FixedVariables.upgradeTimes["structure:farmland:tier0"]},

                    {"tier", 0},
                    {"quantity", 0},
                    {"soilQuality", 0},
                    {"soilRetension", 0},

                    {"harvested", new Inventory()},
                    {"moneySpent", 1000}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);

            case "tree":

                structurePropreties = new Dictionary<string, object>
                {
                    {"resource", null},
                    {"age", 0f},
                    {"stage", null},

                    {"shaded", false}
                };

                return new StructureInstance(id, structurePropreties, anchorLocation);
        }

        return null;
    }

    public class StructureInstance{

        public string structureId;
        public Dictionary<string, object> structurePropreties;
        public int[] anchorLocation;

        public StructureInstance(string structureId, Dictionary<string, object> structurePropreties, int[] anchorLocation){

            this.structureId = structureId;
            this.structurePropreties = structurePropreties;
            this.anchorLocation = anchorLocation;
        }

        public StructureInstance clone(){

            int[] newAnchor = new int[]{anchorLocation[0], anchorLocation[1]};

            return new StructureInstance(this.structureId, this.structurePropreties, newAnchor);
        }
    }

    public class TileInstance{

        public string tileId;
        public Vector2 tileLocation;

        public TileInstance(string tileId, Vector2 tileLocation){

            this.tileId = tileId;
            this.tileLocation = tileLocation;
        }
    }

    public class FarmLayout{

        public int[] farmSize;
        public TileInstance[,] tiles;
        public StructureInstance[,] ocupiedTiles;
        public List<StructureInstance> farmStructures;
        public int structureCount;

        public FarmLayout(int[] farmSize){

            this.farmSize = farmSize;
            this.tiles = new TileInstance[farmSize[0], farmSize[1]];
            this.ocupiedTiles = new StructureInstance[farmSize[0], farmSize[1]];
            this.farmStructures = new List<StructureInstance>();
            this.structureCount = 0;
        }

        public void reSize(int[] size, int[] offset){

            farmSize = size;

            TileInstance[,] tilesOld = tiles;
            tiles = new TileInstance[farmSize[0], farmSize[1]];

            foreach (TileInstance tile in tilesOld){

                tile.tileLocation[0] += offset[0];
                tile.tileLocation[1] += offset[1];
                insertTile(tile);
            }

            this.ocupiedTiles = new StructureInstance[farmSize[0], farmSize[1]];
            List<StructureInstance> structures = farmStructures;
            farmStructures = new List<StructureInstance>();

            foreach (StructureInstance structure in structures){

                structure.anchorLocation[0] += offset[0];
                structure.anchorLocation[1] += offset[1];
                insertStructure(structure);
            }
        }

        public bool canFit(StructureInstance instance){
     
            for (int x = instance.anchorLocation[0]; x < instance.anchorLocation[0] + FarmBase.structureSize[instance.structureId][0]; x++){
                for (int y = instance.anchorLocation[1]; y < instance.anchorLocation[1] + FarmBase.structureSize[instance.structureId][1]; y++){
                    if (x < 0 || x >= farmSize[0] || y < 0 || y >= farmSize[1] || ocupiedTiles[x, y] != null ||
                     (FixedVariables.tileRestrictions.ContainsKey(instance.structureId) && !FixedVariables.tileRestrictions[instance.structureId].Contains(tiles[x,y].tileId))){
                        return false;
                    }
                }
            }

            return true;
        }

        public List<string> getResourcesInRadius(int[] anchor, int radius, string structureId){

            List<StructureInstance> structures = new List<StructureInstance>();

            for (int x = anchor[0] - radius; x < anchor[0] + radius; x++){
                for (int y = anchor[1] - radius; y < anchor[1] + radius; y++){
                    if (x >= 0 && x < farmSize[0] && y >= 0 && y < farmSize[1] && ocupiedTiles[x,y] != null &&
                     ocupiedTiles[x,y].structureId == structureId && !structures.Contains(ocupiedTiles[x,y])){
                        structures.Add(ocupiedTiles[x,y]);
                    }
                }
            }

            List<string> resources = new List<string>();

            foreach (StructureInstance structure in structures){
                resources.Add(structure.structurePropreties["resource"].ToString());
            }

            return resources;
        }

        public bool canFitTile(TileInstance tile){

            return tiles[(int)tile.tileLocation.x,(int)tile.tileLocation.y].tileId != tile.tileId &&
                ocupiedTiles[(int)tile.tileLocation.x,(int)tile.tileLocation.y] == null;
        }

        public bool[,] getFreeTiles(StructureInstance instance){

            bool[,] free = new bool[structureSize[instance.structureId][0],structureSize[instance.structureId][1]];

            for (int x = instance.anchorLocation[0]; x < instance.anchorLocation[0] + FarmBase.structureSize[instance.structureId][0]; x++){
                for (int y = instance.anchorLocation[1]; y < instance.anchorLocation[1] + FarmBase.structureSize[instance.structureId][1]; y++){
                    if (x < 0 || x >= farmSize[0] || y < 0 || y >= farmSize[1] || ocupiedTiles[x, y] != null ||
                     (FixedVariables.tileRestrictions.ContainsKey(instance.structureId) && !FixedVariables.tileRestrictions[instance.structureId].Contains(tiles[x,y].tileId))){
                        free[x - instance.anchorLocation[0],y - instance.anchorLocation[1]] = false;
                    }
                    else{ free[x - instance.anchorLocation[0],y - instance.anchorLocation[1]] = true; }
                }
            }
            return free;
        }

        public List<TileInstance> getSurroundingTiles(TileInstance anchor){

            List<TileInstance> tiles = new List<TileInstance>();

            for (int x = (int)anchor.tileLocation.x - 1; x <= anchor.tileLocation.x + 1; x++){
                for (int y = (int)anchor.tileLocation.y - 1; y <= anchor.tileLocation.y + 1; y++){
                    if (x >= 0 && x < farmSize[0] && y >= 0 && y < farmSize[1] && this.tiles[x,y] != anchor){
                        tiles.Add(this.tiles[x,y]);
                    }
                }
            }

            return tiles;
        }

        public void insertTile(TileInstance tile){

            tiles[(int)tile.tileLocation.x, (int)tile.tileLocation.y] = tile;
        }

        public void insertStructure(StructureInstance instance){

            farmStructures.Add(instance);
            structureCount++;

            for (int x = instance.anchorLocation[0]; x < instance.anchorLocation[0] + FarmBase.structureSize[instance.structureId][0]; x++){
                for (int y = instance.anchorLocation[1]; y < instance.anchorLocation[1] + FarmBase.structureSize[instance.structureId][1]; y++){
                    ocupiedTiles[x, y] = instance;
                }
            }
        }

        public void removeStructure(StructureInstance instance){

            farmStructures.Remove(instance);

            for (int x = instance.anchorLocation[0]; x < instance.anchorLocation[0] + FarmBase.structureSize[instance.structureId][0]; x++){
                for (int y = instance.anchorLocation[1]; y < instance.anchorLocation[1] + FarmBase.structureSize[instance.structureId][1]; y++){
                    ocupiedTiles[x, y] = null;
                }
            }
        }
    }
}
