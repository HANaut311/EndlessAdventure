using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public static Player instance;
    public bool inWindZone = false;
    public GameObject windZone;
    
    [Header("In Water")]
    [SerializeField]private float forceAmount;
    private Vector2 movementDirection;
    private bool moveWater = false;
    private float timeElapsed;

    [SerializeField] private bool testingOnPC;

    //public int fruits;
    //public int coins;
    [Header("Particles")]

    [SerializeField] private ParticleSystem  dustFx;
    private float dustFxTimer;




    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public Vector2 wallJumpDirection;

    private float defaultJumpForce;

    private bool canDoubleJump = true;
    private bool canMove;


    private bool canBeControlled;

    private bool readyToLand;


    [SerializeField] private float bufferJumpTime; //player trước khi chạm đất thì khi ấn nhảy sẽ nhảy (Nhảy đệm)
    private float bufferJumpCounter;
    [SerializeField] private float cayoteJumpTime;
    private float cayoteJumpCounter; //cho phép nhảy ở rìa mép Terrain (Nhảy trên không)                    
    private bool canHaveCayoteJump;

    private float defaultGravityScale;

    [Header("Knockedback info")]
    [SerializeField] private Vector2 knockbackDirection;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackProtectionTime;

    private bool isKnocked;
    private bool canBeKnocked = true;


    [Header("Collision info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsWater;
    [SerializeField] private float groundCheckDistance; 
    [SerializeField] private float wallCheckDistance;
    // [SerializeField] private float waterCheckDistance;
    [SerializeField] Transform enemyCheck;
    [SerializeField] private float waterCheckRadius;
    [SerializeField] private float enemyCheckRadius;
    private bool isGrounded;
    private bool isWallDetected;
    private bool isWaterDetected;
    private bool canWallSlide;
    private bool isWallSliding;


    private bool facingRight = true;
    private int facingDirection = 1;

    [Header("Controlls info")]
    public VariableJoystick joystick;
    private float hInput; // horizental
    private float vInput;//vertical

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetAnimationLayer();

        defaultJumpForce = jumpForce;
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

    }


    void Update()
    {

        AnimationControllers();

        if (isKnocked)
            return;

        FlipController();
        CollisionChecks();
        InputChecks();

        CheckForEnemy();
        CheckForEnemy2();
        CheckForBox();
        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if (isGrounded)
        {
            canDoubleJump = true;
            canMove = true;

            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }

            canHaveCayoteJump = true;

            if(readyToLand)
            {
                dustFx.Play();
                readyToLand = false;
            }
        }
        else
        {
            if(!readyToLand)
                readyToLand = true;

            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }

        }

        if (canWallSlide)
        {
            isWallSliding = true;
            anim.SetBool("isWallSliding", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
            
        }


        Move();
        
        if(isWaterDetected)
        {

            isWallSliding = false;
            anim.SetBool("isWallSliding", false);
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            timeElapsed += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                moveWater = true;
                anim.SetBool("isMoving", true);
            } 

            if (moveWater) 
            {
                rb.velocity = movementDirection * forceAmount;
            }



        }
 


              
    }

    void FixedUpdate()
    {
        if(inWindZone)
        {
            rb.AddForce(windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision )
    {
        if(collision.gameObject.tag == "windArea")
        {
            windZone = collision.gameObject;
            inWindZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "windArea")
        {
            inWindZone = false;
        }
    }

    private void CheckForEnemy() //enemy
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        
        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();

                if (newEnemy.invicinble)
                    return;


                if (rb.velocity.y < 0 || isWaterDetected)
                {
                    AudioManagerInGame.instance.PlaySFXInGame(1);

                    newEnemy.Damage();
                    Jump();
                }
            }
        }
    }
    private void CheckForEnemy2()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        foreach (var enemy2 in hitedColliders)
        {
            if (enemy2.GetComponent<Enemy_2>() != null)
            {
                Enemy_2 newEnemy = enemy2.GetComponent<Enemy_2>();

                if (newEnemy.invicinble)
                    return;


                if (rb.velocity.y < 0)
                {
                    AudioManagerInGame.instance.PlaySFXInGame(1);

                    newEnemy.Damage();
                    Jump();
                }
            }
        }
    }

    private void CheckForBox()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        foreach (var box in hitedColliders)
        {
            if (box.GetComponent<Enemy_Box>() != null)
            {
                Enemy_Box newEnemy = box.GetComponent<Enemy_Box>();

                if (newEnemy.invicinble)
                    return;


                if (rb.velocity.y < 0 || isWallDetected)
                {
                    AudioManagerInGame.instance.PlaySFXInGame(2);

                    newEnemy.Damage();
                    Jump();
                }

            }
        }
    }



    private void StopFlippingAnimation()
    {
        anim.SetBool("flipping", false);
    }

    private void StopWallSlidingAnimation()
    {
        anim.SetBool("isWallSliding", false);
    }

    private void SetAnimationLayer()
    {
        int skinIndex = PlayerManager.instance.choosenSkinId;
        for(int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(skinIndex, 1);    
    
    }



    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetBool("isWaterDetected", isWaterDetected);
        anim.SetBool("canBeControled", canBeControlled);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    private void InputChecks()
    {
        if(!canBeControlled)
            return;

        if(testingOnPC)
        {
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");

        }
        else
        {
            hInput = joystick.Horizontal;
            vInput = joystick.Vertical;
        }

        //nhảy trên tường
        if (vInput < 0)
            canWallSlide = false;

        //nhảy trên layer Ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
            anim.SetBool("isWallSliding", false);
        }
    }

    public void ReturnControll()
    {
        rb.gravityScale = defaultGravityScale;
        canBeControlled =  true;
    }

    public void JumpButton()//double jump
    {
        if (!isGrounded)
        {
            bufferJumpCounter = bufferJumpTime;
            anim.SetBool("flipping", false);
        }

        if (isWallSliding) //wall jump
        {
            WallJump();
            canDoubleJump = true;
  
        }
        else if (isGrounded || cayoteJumpCounter > 0)
        {
            Jump();
            anim.SetBool("flipping", false);
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            anim.SetBool("flipping", true);
            Jump();
            jumpForce = defaultJumpForce;
        }

        canWallSlide = false;

    }

    public void KnockBack(Transform damageTransform) //bị dính dame
    {
        // AudioManagerInGame.instance.PlaySFXInGame(7);
        if (!canBeKnocked)
            return;


        HealthManager.health --; // hết food thì chết
        if(HealthManager.health <3)
        {
            AudioManagerInGame.instance.PlaySFXInGame(5);
        }

        if(HealthManager.health <2)
        {
            AudioManagerInGame.instance.PlaySFXInGame(5);
        }

        if(HealthManager.health <1)
        {
            AudioManagerInGame.instance.PlaySFXInGame(5);
        }

        if(HealthManager.health <=0)

        {
            PlayerManager.instance.OnFalling();

        }

        // if(GameManager.instance.difficulty > 1)
        // {
        //      PlayerManager.instance.OnTakingDamage();

        // }

        // PlayerManager.instance.ScreenShake(-facingDirection);
        isKnocked = true;
        canBeKnocked = false;

        #region Define horizontal direction for knockback
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x)
            hDirection = 1;
        else if (transform.position.x < damageTransform.position.x)
            hDirection = -1;

        #endregion    

        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);

        Invoke("CancelKnockback", knockbackTime);
        Invoke("AllowKnockback", knockbackProtectionTime);

    }

    private void CancelKnockback()
    {
        isKnocked = false;
    }
    private void AllowKnockback()
    {
        canBeKnocked = true;
    }

    private void Move()//di chuyen
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed * hInput, rb.velocity.y);
        }


    }

    private void WallJump() //nhảy tường
    {
        AudioManagerInGame.instance.PlaySFXInGame(4);
        canMove = true;
        // anim.SetBool("isWallSliding", false);
        StopFlippingAnimation();
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);

        dustFx.Play();
    }

    private void Jump()//nhay
    {
        AudioManagerInGame.instance.PlaySFXInGame(4);

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // anim.SetBool("isWallSliding", false);

        if(isGrounded)
            dustFx.Play();
    }

    public void Push(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private void FlipController() //xoay người
    {
        dustFxTimer -= Time.deltaTime;
        
        if (facingRight && rb.velocity.x < -.1f)
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > .1f)
        {
            Flip();
        }

    }

    private void Flip() // xoay người
    {
        if(dustFxTimer < 0)
        {
            dustFx.Play();
            dustFxTimer = .7f;
        }


        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void CollisionChecks()//nhay tren layer Ground, nhay tren wall
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);//ground
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);//wall
        isWaterDetected = Physics2D.OverlapCircle(transform.position, waterCheckRadius, whatIsWater);

        if (isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
            // canDoubleJump = false;
        }
        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }

    }

    private void OnDrawGizmos()//Xét khoảng cách từ player xuống Ground, Xet khoảng cách từ player đến wall
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance)); //ground
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y)); // wall
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius); // enemy
        Gizmos.DrawWireSphere(transform.position, waterCheckRadius); // water
    }
}
