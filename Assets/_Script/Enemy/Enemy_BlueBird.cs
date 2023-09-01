using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueBird : Enemy
{
    private RaycastHit2D ceillingDectected;

    [Header("BlueBird specifics")]
    [SerializeField] private float ceillingDistance;
    [SerializeField] private float groundDistance;

    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;
                     private float flyForce;
    
    private bool canFly = true;

    protected override void Start()
    {
        base.Start();
        flyForce = flyUpForce;
    }

    void Update()
    {

        CollisionChecks();
        if(ceillingDectected)
        {
            flyForce = flyDownForce;
        }
        else if(groundDetected)
        {
            flyForce = flyUpForce;
        }
        if  (wallDetected)
        {
            Flip();
        }

    }

    public override void Damage()
    {
        canFly = false;
        rb.velocity = new Vector2 (0,0);
        rb.gravityScale = 0;
        
        base.Damage();
    }

    [SerializeField] private Transform movePoint;
    [SerializeField] private float xMultiplier;
    [SerializeField] private float yMultiplier;
    
    public void FlyUpEvent()
    {
        if (canFly)
            rb.velocity = new Vector2(speed * facingDirection, flyForce);

    }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        ceillingDectected = Physics2D.Raycast(transform.position, Vector2.up, ceillingDistance, whatIsGround);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceillingDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundDistance));
    }
}
