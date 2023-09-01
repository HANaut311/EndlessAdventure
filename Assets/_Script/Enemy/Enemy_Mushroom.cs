using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        
        walkAround();
        CollisionChecks();
        
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    protected override void CreateEnemyDeath()
    {
        base.CreateEnemyDeath();
    }
}
