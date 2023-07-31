using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionProgressBar : MonoBehaviour
{
    public RectTransform progressBar;

    public void setState(bool value){

        progressBar.gameObject.SetActive(value);
    }

    public void setBarPos(){

        progressBar.localPosition = new Vector3(GameManager.Instance.cameraController.lastTapPos.x -100f,
         GameManager.Instance.cameraController.lastTapPos.y, 0);
    }

    public void setSize(float size){

        progressBar.sizeDelta = new Vector2(50f, 400f * size);
    }
}
