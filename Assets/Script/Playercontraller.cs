using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontraller : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float restoreTime;
    public float climbSpeed;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    private bool isOneWayFlatform;

    private bool isLadder;
    private bool isClimbing;

    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float playerGravity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();//��ȡ�˽�ɫboxcollider2d����
        playerGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            Run();
            Flip();
            Jump();
            Climb();
            CheckAirStatus();
            //Attack();
            CheckGround();
            CheckLadder();
            SwitchAnimation();
            OneWayPlatformCheck();
        }
    }

    //����Ƿ��ǵ���
    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) || 
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
                    myFeet.IsTouchingLayers(LayerMask.GetMask("DestructLayer")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayFlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        // Debug.Log(isGround);
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
    void Flip()
    {
        bool playerHasXAixSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAixSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    
    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");//�ж��ƶ����򣬷���ֵ��Χ-1��0��1
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);//�����ٶȣ�y�᲻��
        myRigidbody.velocity = playerVel;
        bool playerHasXAixSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run",playerHasXAixSpeed);
    }

    void Climb()
    {
        if (isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Climbing", true);
                myRigidbody.gravityScale = 0.0f;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);

            }
            else
            {
                if (isJumping || isFalling || isDoubleFalling || isDoubleJumping)
                {
                    myAnim.SetBool("Climbing", false);
                }
                else
                {
                    myAnim.SetBool("Climbing", false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigidbody.gravityScale = playerGravity;
        }

    }
    //void Attack()
    //{
    //    if(Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))//��edit->project setting->input��
        {
            if (isGround)//��ز�����
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }

            else
            {
                if (canDoubleJump)
                {
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);//��ֵ�������ٶ�
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            } 
        }
    }

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle",false);
        if (myAnim.GetBool("Jump"))//һ����
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))//������
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatformCheck()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
          gameObject.layer = LayerMask.NameToLayer("Player");
        }

        float moveY = Input.GetAxis("Vertical");
        if (isOneWayFlatform && moveY < -0.1f)
        {   
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");//������ĳɺ͵���ƽ̨һ���㣬��Ϊͬһ������ײ
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");

    }
}
