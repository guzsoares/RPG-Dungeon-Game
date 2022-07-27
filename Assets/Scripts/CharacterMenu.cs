using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    //Text 
    public TextMeshProUGUI levelText, hitpointText, goldText, upgradeCostText, xpText;

    // Logic
    private int currentCharacter = 0;
    public Image characterSelectedSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick (bool right)
    {
        if (right)
        {
            currentCharacter++;

            if (currentCharacter == GameManager.instance.playerSprites.Count)
            {
                currentCharacter = 0;
            }

            OnSelectionChanged();
        }
        else
        {
             currentCharacter++;

            if (currentCharacter == GameManager.instance.playerSprites.Count)
            {
                currentCharacter = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged ()
    {
        characterSelectedSprite.sprite = GameManager.instance.playerSprites[currentCharacter];
    }

    // Weapon upgrade
    public void OnClickUpgrade ()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update charecter information
    public void UpdateMenu ()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (!(GameManager.instance.weaponPrices.Count <= GameManager.instance.weapon.weaponLevel))
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        else
        {
            upgradeCostText.text = "MAX";
        }

        // Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        goldText.text = GameManager.instance.gold.ToString();
        levelText.text = GameManager.instance.weapon.weaponLevel.ToString();

        // xp BAR
        xpText.text = "NOT IMPLEMENTED";
        xpBar.localScale = new Vector3(0.5f,1.0f,0);
    }
}
