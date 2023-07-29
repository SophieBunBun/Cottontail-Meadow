using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public GameObject player;
    public LayerMask layer;
    public float minDistance;
    public float maxDistance;
    public float interpolation;
    public float sensitivity;

    public bool orthographic;

    private RaycastHit lastHit;
    private Vector3 targetLocation;
    private float targetSize;
    private Camera cam;
    public Camera UICamera;
    public float currentDistance;

    void Start(){
        Instance = this;
        cam = this.GetComponent<Camera>();
        if (!orthographic){
            targetLocation = transform.localPosition;
            currentDistance = Vector3.Distance(transform.position, player.transform.position);
        }
        else{
            targetSize = cam.orthographicSize;
        }
    }

    void Update()
    {
        if (transform.localPosition.x != targetLocation.x){

            targetLocation.x = transform.localPosition.x;
        }

        if (!orthographic && transform.localPosition != targetLocation){

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocation, interpolation);
        }

        if (orthographic && cam.orthographicSize != targetSize){

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, interpolation);
            UICamera.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, interpolation);
        }
    }

    public void updateZoom(){

        float targetDelta = -1 * Input.GetAxis("Mouse ScrollWheel") * sensitivity;

        if (orthographic){
            if(currentDistance + targetDelta < maxDistance && currentDistance + targetDelta > minDistance){
                targetSize += targetDelta;
                currentDistance += targetDelta;
            }
        }
        else{
            Vector3 target = targetLocation - transform.forward * targetDelta;
            if(currentDistance + targetDelta < maxDistance && currentDistance + targetDelta > minDistance){
                targetLocation = target;
                currentDistance += targetDelta;
            }
        }
    }

    public RaycastHit raycast(){

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layer)){
            lastHit = hit;
            return hit;
        }
        return lastHit;
    }
}
