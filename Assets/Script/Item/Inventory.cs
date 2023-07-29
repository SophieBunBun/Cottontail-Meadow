using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Dictionary<string, Item> items = new Dictionary<string, Item>();

    public int getItemCount(string itemId){

        return items.ContainsKey(itemId) ? items[itemId].itemCount : 0;
    }

    public void addItem(Item item){

        if (!items.ContainsKey(item.itemId)){
            items.Add(item.itemId, item.clone());
        }
        else{
            items[item.itemId].itemCount += item.itemCount;
        }
    }

    public void removeItem(Item item){

        items[item.itemId].itemCount -= item.itemCount;
    }

    public bool containsAmount(Item item){

        return items.ContainsKey(item.itemId) && items[item.itemId].itemCount >= item.itemCount;
    }
}
