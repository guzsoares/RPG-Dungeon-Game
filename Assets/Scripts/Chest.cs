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
            //test
            GameManager.instance.ShowText("+" + goldCount + " gold!", 21, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
            GameManager.instance.gold += goldCount;
        }
    }
}
