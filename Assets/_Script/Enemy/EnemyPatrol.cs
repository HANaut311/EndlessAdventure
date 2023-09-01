
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    protected int facingDirection = -1;

    [Header("Idle Behaviuor")]    
    [SerializeField]private float idleDuration;
    private float idleTimer;


    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("move", false);
    }

    private void Update()
    {
        if(movingLeft)
        {   
            if(enemy.position.x >= leftEdge.position.x)
                MoveInDirection(1);
            else
            {
                DiretionChange();
            }

        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)

            MoveInDirection(-1);
            else
            {
                DiretionChange();
            }
        }

    }

    private void DiretionChange()
    {
        anim.SetBool("move", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction)
    {   
        anim.SetBool("move", true);
        idleTimer = 0;
        //Make enemy face direction
        enemy.localScale  = new Vector3(Mathf.Abs( initScale.x*-1)* _direction, initScale.y, initScale.z);


        //Move in that direction
        enemy.position = new Vector3(enemy.position.x +  Time.deltaTime * _direction * speed *-1,
                enemy.position.y, enemy.position.z);
    }

}
