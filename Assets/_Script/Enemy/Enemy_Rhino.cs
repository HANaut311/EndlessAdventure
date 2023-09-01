using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rhino : Enemy
{
    [Header ("Rhnino spesific")]

    [SerializeField] private float agroSpeed;
    [SerializeField] private float shockTime;
                     private float shockTimeCounter;

    
    protected override void Start()
    {
        base.Start();
        invicinble = true;
    }


    void Update()
    {
        CollisionChecks();
        AnimatorController();

        if(!playerDectection)
        {
            walkAround();
            return;
        }
        
        if(playerDectection.collider.GetComponent<Player>() != null && playerDectection) 
            aggresive = true;
        
        if(!aggresive)
        {
            walkAround();
        }

        else
        {
            if(!groundDetected)
            {
                aggresive = false;
                Flip();

            }

            rb.velocity = new Vector2(agroSpeed * facingDirection, rb.velocity.y);

            if(wallDetected && invicinble)
            {
                invicinble = false;
                shockTimeCounter = shockTime;
            }


            if(shockTimeCounter <= 0 && !invicinble)
            {
                invicinble = true;
                Flip();
                aggresive = false;
                 
            }

            shockTimeCounter -= Time.deltaTime;

        }
        

    }

    private void AnimatorController()
    {
        anim.SetBool("invincible", invicinble);
        anim.SetFloat("xVelocity", rb.velocity.x);

    }


}
