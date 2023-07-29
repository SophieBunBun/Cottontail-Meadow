using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeleftBar : MonoBehaviour
{
    public RectTransform background;
    public RectTransform foreground;
    private Vector2 sizeDelta;

    void Awake()
    {
        sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        background.sizeDelta = sizeDelta;
        foreground.sizeDelta = sizeDelta;
    }

    public void setPercentage(float percent){
        foreground.sizeDelta = new Vector2(sizeDelta.x * percent, sizeDelta.y);
    }

    public void setColor(Color color){
        foreground.GetComponent<Image>().color = color;
        background.GetComponent<Image>().color = color * 0.6f;
    }

    public static string toTime(int time){
        int minutes = time / 60;
        int hours = minutes / 60;

        string finalString = hours == 0 ? "" : hours.ToString() + "h:";
        finalString += minutes == 0 && hours == 0 ? "" : (minutes % 60).ToString() + "m:";
        finalString += (time % 60).ToString() + "s";
        return finalString;
    }
}
