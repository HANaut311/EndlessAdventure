using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plant : Enemy
{
    [Header("Plant Specifics")]

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool facingRight;
    [SerializeField] private bool AutoAttack;
    protected override void Start()
    {
        base.Start();

        if(facingRight)
            Flip();



    }

    void Update()
    {
        CollisionChecks();
        idleTimeCounter -= Time.deltaTime;

        if(!playerDectection)
            return;

        bool playerDectected = playerDectection.collider.GetComponent<Player>() != null;
        if(AutoAttack)
        {
            if(idleTimeCounter < 0)//    && playerDectected) //hoặc bỏ playerDectected để auto bắn 
            {
                idleTimeCounter = idleTime;
                anim.SetTrigger("attack");

            }
        }
        else
        {
            if(idleTimeCounter < 0 && playerDectected) //hoặc bỏ playerDectected để auto bắn 
            {
                idleTimeCounter = idleTime;
                anim.SetTrigger("attack");

            }

        }





    }

    private void AttackEvent() //Instantiate: cho phép khởi tạo đối tượng trong khi thời gian đang chạy, từ 1 đối tượng đã được khởi tạo trước
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);

        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
    }

}
