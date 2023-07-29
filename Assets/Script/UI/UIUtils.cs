using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtils : MonoBehaviour
{
    public static IEnumerator movePanel(Transform panel, Vector2 from, Vector2 to){

        float lerp = 0f;

        while (lerp < 1f){

            panel.localPosition = Vector2.Lerp(from, to, lerp);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator moveCameraHorizontal(Transform camera, int from, int to){

        float lerp = 0f;

        while (lerp < 1f){

            camera.localPosition = new Vector3(Mathf.Lerp(-2f, 0f, lerp), camera.localPosition.y, camera.localPosition.z);

            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }
}
