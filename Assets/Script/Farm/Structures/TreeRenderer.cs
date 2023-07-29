using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRenderer : MonoBehaviour
{
    public FarmBase.StructureInstance tree;

    public GameObject treeGraphic;
    public ParticleSystem particle;

    public void deepUpdateStructure(){

        transform.position = new Vector3((tree.anchorLocation[0] + FarmBase.structureSize[tree.structureId][0] / 2f) * 2f ,0.01f ,
                                    (tree.anchorLocation[1] + FarmBase.structureSize[tree.structureId][1] / 2f) * 2f);
        updateGraphics();
        updateStructure();
    }

    public void updateGraphics(){
        treeGraphic.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("structures:{0}{1}{2}",
         tree.structurePropreties["resource"].ToString(), tree.structurePropreties["stage"].ToString(), (bool)tree.structurePropreties["shaded"] ? "Shaded" : ""));
    }

    public void playLeafParticles(){

        float scale = (float)tree.structurePropreties["age"] /
                          FarmBase.growthTimes[(string)tree.structurePropreties["resource"] + (int)tree.structurePropreties["stage"]];
            scale /= (FixedVariables.resourceFinalStage[(string)tree.structurePropreties["resource"]] + 1f);
            scale += (float)(int)tree.structurePropreties["stage"] / (FixedVariables.resourceFinalStage[(string)tree.structurePropreties["resource"]] + 1f);

        var shape = particle.shape;
        shape.position = getParticlePosition(scale);
        shape.scale = getParticleScale(scale);
        for (int x = 0; x < getParticleCount(scale); x++){
            particle.Emit(1);
        }
    }

    public void updateStructure(){

        if ((int)(float)tree.structurePropreties["age"] != 0)
        {
            float scale = (float)tree.structurePropreties["age"] /
                          FarmBase.growthTimes[(string)tree.structurePropreties["resource"] + (int)tree.structurePropreties["stage"]];
            scale /= (FixedVariables.resourceFinalStage[(string)tree.structurePropreties["resource"]] + 1f);
            scale += (float)(int)tree.structurePropreties["stage"] / (FixedVariables.resourceFinalStage[(string)tree.structurePropreties["resource"]] + 1f);
            
            treeGraphic.transform.localScale = getScales(scale);
            treeGraphic.transform.localPosition = getPositions(scale);
        }
    }

    private static Vector3 getScales(float scale){
        return Vector3.Lerp(new Vector3(1f, 1f, 1.375f), new Vector3(4f, 4f, 5.5f), scale);
    }

    private static Vector3 getPositions(float scale){
        return Vector3.Lerp(new Vector3(-0.1f, 1.25f, 0.1f), new Vector3(-0.5f, 5.2f, 0.5f), scale);
    }

    private static Vector3 getParticleScale(float scale){
        return Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(1f, 8f, 8f), scale);
    }

    private static Vector3 getParticlePosition(float scale){
        return Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(0f, 8f, 0f), scale);
    }

    private static int getParticleCount(float scale){
        return Mathf.RoundToInt(Mathf.Lerp(25, 250, scale));
    }
}
