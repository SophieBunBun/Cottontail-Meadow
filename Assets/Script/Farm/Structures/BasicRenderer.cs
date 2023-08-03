using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRenderer : MonoBehaviour
{
    public FarmBase.StructureInstance farmStructure;
    public GameObject structure;

    public void deepUpdateStructure(){

        //Setting anchor location
        transform.position = new Vector3((farmStructure.anchorLocation[0] + FarmBase.structureSize[farmStructure.structureId][0] / 2f) * 2f ,0.01f ,
                                    (farmStructure.anchorLocation[1] + FarmBase.structureSize[farmStructure.structureId][1] / 2f) * 2f);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    public void destroyStructure(){
        Destroy(structure);
        Destroy(this.gameObject);
    }
}
