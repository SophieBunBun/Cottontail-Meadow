using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBobPointer : MonoBehaviour
{
    public float bobScale = 1f;
    public float downwardsAcceleration = -5f;
    private Vector3 position;

    void Start()
    {
        position = transform.position;
        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce(){

        Vector3 low = position;
        Vector3 high = position + transform.up * bobScale;

        float currentSpeed = 0f;
        float currentY = 1f;

        while (true){

            transform.position = Vector3.Lerp(low, high, currentY);

            currentY += currentSpeed * Time.deltaTime;
            currentSpeed +=  downwardsAcceleration * Time.deltaTime;

            if (currentY < 0f){

                currentSpeed = -currentSpeed;

            }
            else if (currentY > 1f){

                currentY = 1f;
                currentSpeed = 0f;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
