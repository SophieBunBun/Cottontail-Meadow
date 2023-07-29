using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlan : MonoBehaviour
{
    public FarmBase.StructureInstance structure;
    public FarmBase.TileInstance tile;
    private float[] middleOffset;
    private SpriteRenderer[,] tiles;

    public void Destroy(){

        foreach (SpriteRenderer tile in tiles){
            Destroy(tile.gameObject);
        }
        Destroy(gameObject);
    }
    public void setStructure(FarmBase.StructureInstance structure){

        this.structure = structure;
        middleOffset = new float[] 
        {(FarmBase.structureSize[structure.structureId][0] / 2f) % 1,
         (FarmBase.structureSize[structure.structureId][1] / 2f) % 1};

        tiles = new SpriteRenderer[FarmBase.structureSize[structure.structureId][0],FarmBase.structureSize[structure.structureId][1]];

        for (int x = 0; x < FarmBase.structureSize[structure.structureId][0]; x++){
            for (int y = 0; y < FarmBase.structureSize[structure.structureId][1]; y++){
                tiles[x,y] = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:plane"), transform).GetComponent<SpriteRenderer>();
                tiles[x,y].transform.localPosition = new Vector3(
                    (FarmBase.structureSize[structure.structureId][0] * -1f) + 1f + 2f * x, 
                    0, (FarmBase.structureSize[structure.structureId][1] * -1f) + 1f + 2f * y);
            }
        }

        updateTiles();
    }

    public void setTile(FarmBase.TileInstance tile){

        this.tile = tile;

        tiles = new SpriteRenderer[1,1];
        tiles[0,0] = Instantiate((GameObject)GameManager.Instance.getResource("general:tools:plane"), transform).GetComponent<SpriteRenderer>();
        tiles[0,0].transform.localPosition = new Vector3(0, 0, 0);
        tiles[0,0].color = new Color(1f, 0.6f, 0f, 0.75f);
    }

    public void updatePositioning(Vector2 position){

        if (structure != null){
            position /= 2;
            transform.position = new Vector3(
                Mathf.Min(Mathf.Max(Mathf.Round(position.x + middleOffset[0]) - middleOffset[0],
                FarmBase.structureSize[structure.structureId][0] / 2f), Farm.Instance.farmLayout.farmSize[0] - (FarmBase.structureSize[structure.structureId][0] / 2f)),
                transform.position.y,
                Mathf.Min(Mathf.Max(Mathf.Round(position.y + middleOffset[1]) - middleOffset[1],
                FarmBase.structureSize[structure.structureId][1] / 2f), Farm.Instance.farmLayout.farmSize[1] - (FarmBase.structureSize[structure.structureId][0] / 2f))) * 2;
            
            structure.anchorLocation = new int[] {
                (int) (transform.position.x / 2f - FarmBase.structureSize[structure.structureId][0] / 2f),
                (int) (transform.position.z / 2f - FarmBase.structureSize[structure.structureId][1] / 2f),
            };
            updateTiles();
        }
        else if (tile != null){

            position /= 2;
            position = new Vector2(Mathf.Floor(position.x), Mathf.Floor(position.y));
            transform.position = new Vector3((position.x * 2f) + 1f, transform.position.y, (position.y * 2f) + 1f);
            tile.tileLocation = position;
        }
    }

    public void updateTiles(){

        bool[,] freeTiles = Farm.Instance.farmLayout.getFreeTiles(structure);

        for (int x = 0; x < freeTiles.GetLength(0); x++){
            for (int y = 0; y < freeTiles.GetLength(1); y++){
                tiles[x,y].color = freeTiles[x,y] ? new Color(1f, 0.6f, 0f, 0.75f) : new Color(0.3f, 0.15f, 0.075f, 0.75f);
            }
        }
    }
}
