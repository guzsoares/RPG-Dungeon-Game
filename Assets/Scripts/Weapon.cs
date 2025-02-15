using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = {2.5f,2.7f,2.9f,3.2f,3.5f,4.0f,4.3f};

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // sWING
    private float cooldown = 0.5f;
    private float lastSwing;
    private Animator anim;

    protected override void Start(){
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update(){
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)){
            if(Time.time - lastSwing > cooldown){
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll){
        if (coll.tag == "Fighter"){
            
            if (coll.name != "Player"){

                Damage dmg = new Damage
                {
                    damageAmount = damagePoint[weaponLevel],
                    origin = transform.position,
                    pushForce = pushForce[weaponLevel]
                };

                coll.SendMessage("ReceiveDamage",dmg);
            }
        }
    }

    private void Swing(){
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon ()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change Stats
    }

    public void SetWeaponLevel (int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
