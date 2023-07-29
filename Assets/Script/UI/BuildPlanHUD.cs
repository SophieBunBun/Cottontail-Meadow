using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPlanHUD : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform precisePanel;
    public Button confirm;
    public Button cancel;
    public RectTransform freePanel;
    public Button goBack;

    public IEnumerator OpenElement(bool free){

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        float lerp = 0f;

        while (lerp < 1f){

            if (free){
                freePanel.localPosition = new Vector2(0f, Mathf.Lerp(-250f, 0f, lerp));
            }
            else{
                precisePanel.localPosition = new Vector2(0f, Mathf.Lerp(-250f, 0f, lerp));
            }
            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator CloseElement(){

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float lerp = 0f;

        while (lerp < 1f){

            precisePanel.localPosition = new Vector2(0f, Mathf.Lerp(precisePanel.localPosition.y, -250f, lerp));
            freePanel.localPosition = new Vector2(0f, Mathf.Lerp(freePanel.localPosition.y, -250f, lerp));
            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }
}
