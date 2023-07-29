using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayContainer : MonoBehaviour
{
    public float scale = 0.4f;

    public void setItems(IEnumerable<Item> items){

        foreach (Transform child in transform){
            foreach (Transform leaf in child){
                Destroy(leaf.gameObject);
            }
            Destroy(child.gameObject);
        }

        foreach (Item item in items){

            ItemDisplay itemDisplay1 =
                Instantiate((GameObject)GameManager.Instance.getResource("general:ui:itemDisplay"), transform).GetComponent<ItemDisplay>();

            itemDisplay1.transform.localScale = new Vector3(scale, scale, scale);
            itemDisplay1.image.sprite =
                GameManager.Instance.getSprite("sprites:itemIcon:" + item.itemId);
            itemDisplay1.text.text = "" + item.itemCount;
        }
    }
}
