using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TallyCounter : MonoBehaviour
{
    public Button down;
    public Button up;
    public TextMeshProUGUI text;
    public int count = 0;
    private int max;

    void Start(){
        down.onClick.AddListener(delegate {decrementCounter();});
        up.onClick.AddListener(delegate {incrementCounter();});
    }

    public void Setup(int max){

        this.max = max;
        count = max;
        text.text = count.ToString();
    }

    public void toggleButtons(bool value){
        down.interactable = value;
        up.interactable = value;
    }

    private void incrementCounter(){

        if (count != max){
            count++;
            text.text = count.ToString();
        }
    }

    private void decrementCounter(){

        if (count != 1){
            count--;
            text.text = count.ToString();
        }
    }
}
