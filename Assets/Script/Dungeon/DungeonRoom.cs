using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public string dungeonId;
    public int dungeonLevel;

    public Transform resourceNodes;
    void Start()
    {
        setResources();
    }

    public void setResources(){

        foreach(Transform node in resourceNodes){

            if(Random.Range(0, dungeonLevel + 6) < 5){
                if (Random.Range(0, dungeonLevel + 6) < 4){
                    Destroy(node.GetChild(0).gameObject);
                    Destroy(node.gameObject);
                }
                else{
                    node.GetComponent<ResourceNodeRenderer>().resourceNodeId = pickRandom(FixedVariables.dungeonFillers[dungeonId]);
                    node.GetComponent<ResourceNodeRenderer>().setResourceRender();
                }
            }
            else{
                node.GetComponent<ResourceNodeRenderer>().resourceNodeId = pickRandom(FixedVariables.dungeonResources[dungeonId]);
                node.GetComponent<ResourceNodeRenderer>().setResourceRender();
            }
        }
    }

    private string pickRandom(Dictionary<string, float> table){

        float value = Random.Range(0, 100) / 100f;
        float threshold = 0;
        foreach(string resource in table.Keys){
            threshold += table[resource];
            if (value < threshold) {
                return resource;
            }
        }
        return null;
    }
}
