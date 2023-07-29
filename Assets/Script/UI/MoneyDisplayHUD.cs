using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDisplayHUD : MonoBehaviour
{
    public ItemDisplay itemDisplay;

    void Start(){

        StartCoroutine(updateMoneyDisplayRoutine());
    }

    public void updateMoneyDisplay(){

        StartCoroutine(updateMoneyDisplayRoutine());
    }

    private IEnumerator updateMoneyDisplayRoutine(){

        float lerp = 0f;

        while (lerp <= 1f){

            itemDisplay.text.text = "" +
                (int) Mathf.Lerp(int.Parse(itemDisplay.text.text),
                           GameManager.Instance.itemInventory.getItemCount("money"),
                           lerp);

            lerp += Time.deltaTime * 1f;

            yield return new WaitForEndOfFrame();
        }

        itemDisplay.text.text = "" + GameManager.Instance.itemInventory.getItemCount("money");
    }
}
