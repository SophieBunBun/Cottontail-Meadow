using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBobPlan : MonoBehaviour
{
    public float bobScale = 1f;
    public float downwardsAcceleration = -5f;

    void Start()
    {
        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce(){

        float low = 0;
        float high = bobScale;

        float currentSpeed = 0f;
        float currentY = 1f;

        while (true){

            transform.position = new Vector3(transform.position.x, Mathf.Lerp(low, high, currentY), transform.position.z);

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
