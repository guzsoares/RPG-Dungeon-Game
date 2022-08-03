using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    private SpriteRenderer spriteRenderer;
    public bool isAlive = true;

    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    protected override void Start ()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive){
            UpdateMotor(new Vector3(x,y,0), ySpeed, xSpeed);
        }
    }

    public void SwapSprite (int skinID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
        GameManager.instance.gold += 10;
        GameManager.instance.HitPointChange();
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++){
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {   
        if (hitpoint == maxHitpoint)
        {
            return;
        }
        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint){
            hitpoint = maxHitpoint;
        }
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.magenta, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.HitPointChange();
    }

    protected override void ReceiveDamage(Damage dmg){
        if (!isAlive)
        {
            return;
        }
        base.ReceiveDamage(dmg);
        GameManager.instance.HitPointChange();
        
    }

    protected override void Death()
    {
        GameManager.instance.deathMenuAnimator.SetTrigger("show");
        isAlive = false;
    }

    public void Respawn()
    {
        hitpoint = maxHitpoint;
        GameManager.instance.HitPointChange();
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
    
    
}
