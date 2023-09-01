using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Danger
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("Attack Parameter")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameter")]
    [SerializeField] private LayerMask WhatIsPlayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header ("DeathSpawn")]
    [SerializeField] private GameObject EnemyDeathPrefab;
    [SerializeField] private Transform EnemyDeathOrigin;
   
    [HideInInspector] public bool invicinble;
    protected int facingDirection = -1;
    private Player PlayerHealth;
    private EnemyPatrol enemyPatrol;
    protected  virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight())
        {
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer =0;
                anim.SetTrigger("attack");
            }

        }

        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range *transform.localScale.x *facingDirection*colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0,Vector2.left, 0, WhatIsPlayer);
        if(hit.collider !=null)
            PlayerHealth = hit.transform.GetComponent<Player>();
        return hit.collider != null;
    }


    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range *transform.localScale.x*facingDirection*colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //if Player still in range damage him
        if(PlayerInSight())
        {
            PlayerHealth.KnockBack(transform);
        }
    }

    public virtual void Damage()
    {

        if(!invicinble)
        {
            anim.SetTrigger("gotHit");

                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                if(GetComponent<Enemy_2>() != null)
                    GetComponent<Enemy_2>().enabled = false;

        }
    }
    
    public virtual void DestroyMe()
    {   
        PlayerManager.instance.ScreenShake(-facingDirection);
        Destroy(gameObject);
        CreateEnemyDeath();
    }


    protected virtual void CreateEnemyDeath()
    {
        GameObject newEnemyDeath = Instantiate(EnemyDeathPrefab, EnemyDeathOrigin.transform.position, EnemyDeathOrigin.transform.rotation);
        newEnemyDeath.GetComponent<Enemy>();
        Destroy(newEnemyDeath,3f);

    }

}
