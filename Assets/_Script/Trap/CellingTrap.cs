using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CellingTrap : MonoBehaviour   
{
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range;
    private Player PlayerHealth;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private float attackCooldown;
    protected Animator anim;
    
    // [SerializeField] private float ValueY;
    // [SerializeField] private float ValueYExit;
    // [SerializeField] private float Time;
    // [SerializeField] private float TimeExit;
    // [SerializeField] private float TimeDelay;
    // [SerializeField] private float TimeDelayExit;

    // void Start()
    // {
    //     OnScale();
    // }

    // private void OnScale()
    // {
    //     transform.DOMoveY(ValueY, Time)
    //         .SetEase(Ease.InOutSine)
    //         .SetDelay(TimeDelay)
    //         .OnComplete(()=>
    //         {
    //             transform.DOMoveY(ValueYExit, TimeExit)
    //                 .SetDelay(TimeDelayExit)
    //                 .OnComplete(OnScale);

    //         });
            
            

    // }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;



    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.up * range *colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0,Vector2.left, 0, WhatIsPlayer);
        if(hit.collider !=null)
            PlayerHealth = hit.transform.GetComponent<Player>();
        return hit.collider != null;
    }
    private void DamagePlayer()
    {
        //if Player still in range damage him
        if(PlayerInSight())
        {
            PlayerHealth.KnockBack(transform);
        }

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.up * range *colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }



}
