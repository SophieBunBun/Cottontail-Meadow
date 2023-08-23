using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeRenderer : MonoBehaviour
{
    public GameObject resourceGraphic;
    public string resourceNodeId;

    public void setResourceRender(){

        resourceGraphic.transform.rotation = Quaternion.Euler(75, 135, 0);
        resourceGraphic.GetComponent<MeshRenderer>().material = (Material)GameManager.Instance.getResource(string.Format("resourceNodes:{0}{1}",
        resourceNodeId, Random.Range(0, FixedVariables.nodeVariantCount[resourceNodeId])));

        float scale = Random.Range(0, 100) / 100f;
        resourceGraphic.transform.localScale = Vector3.Lerp(new Vector3(0.8f, 0.8f, 1.6f), new Vector3(1f, 1f, 2f), scale);
        resourceGraphic.transform.localPosition = Vector3.Lerp(new Vector3(0f, 1.5f, 0f), new Vector3(0f, 1.9f, 0f), scale);
    }
}
