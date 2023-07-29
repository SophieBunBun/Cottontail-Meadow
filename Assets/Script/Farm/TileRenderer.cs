using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    public FarmBase.TileInstance tile;
    public List<string> connectedPathsId = new List<string>();

    private List<GameObject> layers = new List<GameObject>();
    private int currentLayer;

    public void updatePlacement(){

        this.transform.position = new Vector3((tile.tileLocation.x * 2) + 1, 0, (tile.tileLocation.y * 2) + 1);
    }

    public void updateRenderer(FarmBase.FarmLayout layout){

        foreach (GameObject layer in layers){
                Destroy(layer);
            }
        layers.Clear();
        currentLayer = 0;

        if (tile.tileId.Split(":")[0] == "path"){

            Dictionary<string, FixedVariables.TileLinkRelation> tileLinkRelation = FixedVariables.tileLinkRelationship[tile.tileId];

            //Creating container for path connections
            Dictionary<string, List<Vector2>> connecting = new Dictionary<string, List<Vector2>>();
            Dictionary<string, List<Vector2>> peripheralRaw = new Dictionary<string, List<Vector2>>();
            List<string> differentPaths = new List<string>();
            List<FarmBase.TileInstance> surroundingTiles = layout.getSurroundingTiles(tile);

            connecting.Add(tile.tileId, new List<Vector2>());
            peripheralRaw.Add(tile.tileId, new List<Vector2>());
            differentPaths.Add(tile.tileId);

            foreach (FarmBase.TileInstance tileIn in surroundingTiles){
                if (FixedVariables.tilesOfSignificance[tile.tileId].Contains(tileIn.tileId)
                && FixedVariables.tileLinkRelationship[tile.tileId][tileIn.tileId] != FixedVariables.TileLinkRelation.Hi 
                && !differentPaths.Contains(tileIn.tileId)){
                    connecting.Add(tileIn.tileId, new List<Vector2>());
                    peripheralRaw.Add(tileIn.tileId, new List<Vector2>());
                    differentPaths.Add(tileIn.tileId);
                }
            }

            foreach (string difPathId in differentPaths){

                foreach (FarmBase.TileInstance tileIn in surroundingTiles){

                    if (FixedVariables.tilesOfSignificance[difPathId].Contains(tileIn.tileId)){

                        int relativeX = (int)(tileIn.tileLocation.x - this.tile.tileLocation.x);
                        int relativeY = (int)(tileIn.tileLocation.y - this.tile.tileLocation.y);

                        bool tileLow = FixedVariables.tileLinkRelationship[difPathId][tileIn.tileId] == FixedVariables.TileLinkRelation.Low;

                        if (((relativeX + 1) % 2 == 0 && (relativeY + 1) % 2 == 1) || ((relativeY + 1) % 2 == 0 && (relativeX + 1) % 2 == 1)){
                            if (!tileLow)
                             {connecting[difPathId].Add(new Vector2(relativeX, relativeY));}
                        }
                        else{
                            if (!tileLow)
                             {peripheralRaw[difPathId].Add(new Vector2(relativeX, relativeY));}
                        }
                    }
                }
            }

            //Cleaning peripheral tiles
            Dictionary<string, List<Vector2>> peripheral = new Dictionary<string, List<Vector2>>();

            foreach (string tileId in differentPaths){
                peripheral.Add(tileId, new List<Vector2>());
                foreach (Vector2 test in peripheralRaw[tileId]){
                    if ((connecting[tileId].Contains(test * Vector2.right)) && (connecting[tileId].Contains(test * Vector2.up))){
                        peripheral[tileId].Add(test);
                    }
                }
            }

            //Creating path layers
            differentPaths.Remove(tile.tileId);
            foreach (string tileId in FixedVariables.tilesOfSignificance[tile.tileId]){
                if (differentPaths.Contains(tileId) && (connecting[tileId].Count != 0 || peripheral[tileId].Count != 0))
                 createPathLayersNonMain(tileId, connecting[tileId], peripheral[tileId]);
            }
            createPathLayers(tile.tileId, connecting[tile.tileId], peripheral[tile.tileId]);
        }
    }

    private void createPathLayersNonMain(string tileId, List<Vector2> connecting, List<Vector2> peripheral){

        connectedPathsId.Add(tileId);

        GameObject newLayer = Instantiate((GameObject)GameManager.Instance.getResource("tiles:ground:tile"), this.transform);
        newLayer.transform.localPosition = new Vector3(0, 0.0001f * ++currentLayer, 0);
        layers.Add(newLayer);
        if (tileId.Equals("path:water")){
            BoxCollider collider = newLayer.GetComponent<BoxCollider>();
            collider.size = new Vector3(2f, 10f, 2f);
            collider.center = new Vector3(0f, 5f, 0f);
            newLayer.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        float angle;
        Vector2 sum;
        switch (connecting.Count){

            case 1:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 1));
                newLayer.transform.localEulerAngles = new Vector3(0, Vector2.Angle(Vector2.right, connecting[0]) * (Vector2.Dot(connecting[0], Vector2.down) < 0 ? -1 : 1), 0);
                break;

            case 2:
                if (!Mathf.Approximately(Vector2.Angle(connecting[0], connecting[1]), 90)){
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 2));
                    newLayer.transform.localEulerAngles = new Vector3(0,Vector2.Angle(Vector2.right, connecting[0]),0);
                }
                else{
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 3));
                    sum = connecting[0] + connecting[1];
                    angle = 90f + (Vector2.Angle(sum, Vector2.right) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1));
                    newLayer.transform.localEulerAngles = new Vector3(0, angle - 45f, 0);
                }
                break;

            case 3:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 4));
                sum = connecting[0] + connecting[1] + connecting[2];
                newLayer.transform.localEulerAngles = new Vector3(0, 90f + (Vector2.Angle(Vector2.right, sum) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1)), 0);
                break;

            case 4:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 5));
                newLayer.transform.localEulerAngles = new Vector3(0,0,0);
                break;
            
            default:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 0));
                newLayer.transform.localEulerAngles = new Vector3(0,0,0);
                break;
        }

        if (peripheral.Count > 0) {
            newLayer = Instantiate((GameObject)GameManager.Instance.getResource("tiles:ground:tile"), this.transform);
            newLayer.transform.localPosition = new Vector3(0, 0.0001f * ++currentLayer, 0);
            layers.Add(newLayer);
        }

        switch(peripheral.Count){

            case 1:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner0"));
                newLayer.transform.localEulerAngles = new Vector3(0, 45f + (Vector2.Angle(Vector2.right, peripheral[0]) * (Vector2.Dot(peripheral[0], Vector2.down) < 0 ? -1 : 1)), 0);
                break;

            case 2:
                if (!Mathf.Approximately(Vector2.Angle(peripheral[0], peripheral[1]), 90)){
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner2"));
                    newLayer.transform.localEulerAngles = new Vector3(0,45f + (Vector2.Angle(Vector2.right, peripheral[0]) * (Vector2.Dot(peripheral[0], Vector2.down) < 0 ? -1 : 1)),0);
                }
                else{
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner1"));
                    sum = peripheral[0] + peripheral[1];
                    angle = (Vector2.Angle(sum, Vector2.right) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1));
                    newLayer.transform.localEulerAngles = new Vector3(0, angle, 0);
                }
                break;
            case 3:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner3"));
                sum = peripheral[0] + peripheral[1] + peripheral[2];
                newLayer.transform.localEulerAngles = new Vector3(0, -45f + (Vector2.Angle(Vector2.right, sum) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1)), 0);
                break;

            case 4:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner4"));
                break;
        }
    }

    private void createPathLayers(string tileId, List<Vector2> connecting, List<Vector2> peripheral){

        GameObject newLayer = Instantiate((GameObject)GameManager.Instance.getResource("tiles:ground:tile"), this.transform);
        newLayer.transform.localPosition = new Vector3(0, 0.0001f * ++currentLayer, 0);
        layers.Add(newLayer);
        if (tileId.Equals("path:water")){
            BoxCollider collider = newLayer.GetComponent<BoxCollider>();
            collider.size = new Vector3(2f, 10f, 2f);
            collider.center = new Vector3(0f, 5f, 0f);
            newLayer.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        float angle;
        Vector2 sum;
        switch (connecting.Count){

            case 1:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 1));
                newLayer.transform.localEulerAngles = new Vector3(0, Vector2.Angle(Vector2.right, connecting[0]) * (Vector2.Dot(connecting[0], Vector2.down) < 0 ? -1 : 1), 0);
                break;

            case 2:
                if (!Mathf.Approximately(Vector2.Angle(connecting[0], connecting[1]), 90)){
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 2));
                    newLayer.transform.localEulerAngles = new Vector3(0,Vector2.Angle(Vector2.right, connecting[0]),0);
                }
                else{
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 3));
                    sum = connecting[0] + connecting[1];
                    angle = 90f + (Vector2.Angle(sum, Vector2.right) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1));
                    newLayer.transform.localEulerAngles = new Vector3(0, angle - 45f, 0);
                }
                break;

            case 3:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 4));
                sum = connecting[0] + connecting[1] + connecting[2];
                newLayer.transform.localEulerAngles = new Vector3(0, 90f + (Vector2.Angle(Vector2.right, sum) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1)), 0);
                break;

            case 4:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 5));
                newLayer.transform.localEulerAngles = new Vector3(0,0,0);
                break;
            
            default:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, 0));
                newLayer.transform.localEulerAngles = new Vector3(0,0,0);
                break;
        }

        if (peripheral.Count > 0) {
            newLayer = Instantiate((GameObject)GameManager.Instance.getResource("tiles:ground:tile"), this.transform);
            newLayer.transform.localPosition = new Vector3(0, 0.0001f * ++currentLayer, 0);
            layers.Add(newLayer);
        }

        switch(peripheral.Count){

            case 1:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner0"));
                newLayer.transform.localEulerAngles = new Vector3(0, 45f + (Vector2.Angle(Vector2.right, peripheral[0]) * (Vector2.Dot(peripheral[0], Vector2.down) < 0 ? -1 : 1)), 0);
                break;

            case 2:
                if (!Mathf.Approximately(Vector2.Angle(peripheral[0], peripheral[1]), 90)){
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner2"));
                    newLayer.transform.localEulerAngles = new Vector3(0,45f + (Vector2.Angle(Vector2.right, peripheral[0]) * (Vector2.Dot(peripheral[0], Vector2.down) < 0 ? -1 : 1)),0);
                }
                else{
                    newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner1"));
                    sum = peripheral[0] + peripheral[1];
                    angle = (Vector2.Angle(sum, Vector2.right) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1));
                    newLayer.transform.localEulerAngles = new Vector3(0, angle, 0);
                }
                break;
            case 3:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner3"));
                sum = peripheral[0] + peripheral[1] + peripheral[2];
                newLayer.transform.localEulerAngles = new Vector3(0, -45f + (Vector2.Angle(Vector2.right, sum) * (Vector2.Dot(sum, Vector2.down) < 0 ? -1 : 1)), 0);
                break;

            case 4:
                newLayer.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("materials:{0}{1}", tileId, "corner4"));
                break;
        }
    }
}
