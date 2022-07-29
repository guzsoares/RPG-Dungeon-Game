using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake(){
        if (GameManager.instance != null){

            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTxtManager.gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadGame;
        DontDestroyOnLoad(gameObject);
    }
    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    //References
    public Player player;
    public FloatingTextManager floatingTxtManager;
    public Weapon weapon;
    //Logic
    public int gold;
    public int xp;


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTxtManager.Show(msg,fontSize,color,position,motion,duration);
    }

    public bool TryUpgradeWeapon ()
    {
        // is the weapon max level
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }
        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    public int GetCurrentLevel ()
    {
        int r = 0;
        int add = 0;

        while (xp >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
            {
                return r;
            }
        }
        return r;
    }

    public int GetXpToLevel(int level){
        int r = 0;
        int exp = 0;

        while (r < level)
        {
            exp += xpTable[r];
            r++;
        }
        
        return exp;
    }

    public void GrantXp(int exp)
    {
        int currLevel = GetCurrentLevel();
        xp += exp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp(){
        ShowText("Level up!",22, Color.white, player.transform.position, Vector3.up * 40, 0.7f);
        player.OnLevelUp();
    }

    public void SaveGame(){
        string s = "";

        s+= "0" + "|";
        s+= gold.ToString() + "|";
        s+= xp.ToString() + "|";
        s+= weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveGame", s);
    }

    public void LoadGame(Scene s, LoadSceneMode mode){

        if(!PlayerPrefs.HasKey("SaveGame")){
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveGame").Split('|');

        //Load Game
        //player.SwapSprite(int.Parse(data[0]));
        gold = int.Parse(data[1]);
        xp = int.Parse(data[2]);
        weapon.SetWeaponLevel(int.Parse(data[3]));
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        player.transform.position = GameObject.Find("WayPoint").transform.position;
    }

}
