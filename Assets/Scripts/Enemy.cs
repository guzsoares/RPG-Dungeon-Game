using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;
    public float triggerLenght = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    private float lastWalk;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    
    private Animator anim;

    protected override void  Start(){
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        anim = GetComponent<Animator>();
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate(){

        // Player in range
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght){
                chasing = true;
            }

            if (chasing)
            {
                if(!collidingWithPlayer)
                {
                    anim.SetBool("walking",true);
                    UpdateMotor((playerTransform.position - transform.position).normalized, ySpeed, xSpeed);
                }

            }
            else
            {   
                anim.SetBool("walking",true);
                UpdateMotor(startingPosition - transform.position, ySpeed, xSpeed);
            }
        }
        else
        {   
            anim.SetBool("walking",true);
            UpdateMotor((startingPosition - transform.position), ySpeed, xSpeed);
            chasing = false;
        }

        if (transform.position == startingPosition)
        {
            anim.SetBool("walking",false);
        }

            //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter,hits);

        for (int i = 0; i < hits.Length; i++){
            if (hits[i] == null){
                continue;
            }

            if(hits[i].tag == "Fighter" && hits[i].name == "Player"){
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }


    protected override void Death(){
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }


}