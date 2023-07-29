using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupAnim : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float jumpScale = 1f;
    public float fadeTime = 1f;
    public float downwardsAcceleration = -5f;
    private Vector3 position;

    void Start()
    {
        StartCoroutine(UpAndGo());
    }

    private IEnumerator UpAndGo(){

        Vector3 low = transform.position;
        Vector3 high = transform.position + transform.up * jumpScale;

        float currentSpeed = 2.5f;
        float currentY = 0f;
        float currentAlpha = 1f;

        while (true){

            transform.position = Vector3.Lerp(low, high, currentY);

            currentY += currentSpeed * Time.deltaTime;
            currentSpeed +=  downwardsAcceleration * Time.deltaTime;

            if (currentY > 1f){

                currentY = 1f;
            }

            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            currentAlpha -= fadeTime * Time.deltaTime;

            if (currentAlpha < 0){
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        Destroy(spriteRenderer.gameObject);
        Destroy(this.gameObject);
    }
}
