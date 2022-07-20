using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldCount = 10;

    protected override void OnCollect(){

        if(!collected)
        {
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant gold " + goldCount);
        }
    }
}
