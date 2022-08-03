using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPortal : Collidable
{
    public Boss boss;
    public string[] sceneNames;

    public float lastShown;


    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && boss.hasBeenKilled){

            //Teleport player
            GameManager.instance.SaveGame();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }
        else if (boss.hasBeenKilled == false && coll.name == "Player" && Time.time -lastShown  > 2.0f)
        {   
            lastShown = Time.time;
            GameManager.instance.ShowText("Boss has not been killed yet!",20,Color.red,transform.position, Vector3.up * 50, 2.0f);
        }
    }
}
