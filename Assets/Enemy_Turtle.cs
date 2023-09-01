using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turtle : Enemy
{
    private Player PlayerHealth;
    [SerializeField] private float aggroTime;
                     private float aggroTimeCounter;
    Collider2D SpikeIn;
    protected override void Start()
    {
        base.Start();
        SpikeIn = this.GetComponent<BoxCollider2D>();

        
    }

    public void On()
    {
        SpikeIn.enabled = true;

    }

    public void Off()
    {
        SpikeIn.enabled = false;
    }


}
