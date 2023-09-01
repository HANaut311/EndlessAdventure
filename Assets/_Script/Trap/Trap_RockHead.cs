using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Trap_RockHead : Danger
{   
    protected Animator anim;
    protected Rigidbody2D rb;
    BoxCollider2D RockHead;
    [SerializeField] private float ValueX1;
    [SerializeField] private float ValueX2;
    [SerializeField] private float Time1;
    [SerializeField] private float Time2;
    [SerializeField] private float TimeDelay1;
    [SerializeField] private float TimeDelay2;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        RockHead = GetComponent<BoxCollider2D>();
        OnScale();


    }

    // Update is called once per frame
    void Update()
    {
        // OnScale();
    }
    private void OnScale()
    {   
        transform.DOMoveX(ValueX1, Time1)
            .SetEase(Ease.InOutSine)
            .SetDelay(TimeDelay1)
            .OnComplete(()=>
            {
                transform.DOMoveX(ValueX2, Time2)
                    .SetDelay(TimeDelay2)
                    .OnComplete(OnScale);




            });


    }

    private void On()
    {


    }
    
    private void OFF()
    {


    }

        

   
    
    
}
