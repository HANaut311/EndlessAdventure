using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Danger
{
    Collider2D lightning;

    void Start()
    {
        lightning = this.GetComponent<Collider2D>();
    }

    public void On()
    {
        lightning.enabled = true;
    }

    public void Off()
    {
        lightning.enabled = false;

    }
}
