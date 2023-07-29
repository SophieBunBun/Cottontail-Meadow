using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemId;
    public int itemCount;

    public Item(string itemId, int itemCount){

        this.itemId = itemId;
        this.itemCount = itemCount;
    }

    public Item clone(){

        Item newItem = new Item((string) itemId.Clone(), itemCount);
        return newItem;
    }
}
