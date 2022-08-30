using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myBody;

    [SerializeField]
    float moveSpeed = 5f;

    float horizontalMovement;

    PlayerAnimation playerAnim;

    [SerializeField]
    float normalJumpForce = 5f, doubleJumpForce = 5f;
    float jumpForce = 5f;

    RaycastHit2D groundCast;
    BoxCollider2D boxCol2D;

    [SerializeField]
    LayerMask groundMask;

    bool canDoubleJump;
    bool jumped;

    Animator anim;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        playerAnim=GetComponent<PlayerAnimation>();
        boxCol2D=GetComponent<BoxCollider2D>();
        anim=GetComponent<Animator>();

        canDoubleJump = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw(TagManager.HORIZONTAL_MOVEMENT_AXIS);
        HandleAnimation();
        HandleJumping();
        CheckToDoubleJump();
        FromJumpToWalkOrIdle();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        //myBody.velocity = new Vector2(horizontalMovement*moveSpeed, myBody.velocity.y);
        if (horizontalMovement>0)
        {
            myBody.velocity = new Vector2(moveSpeed,myBody.velocity.y);
        }
        else if (horizontalMovement < 0)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }
    }
    void HandleAnimation()
    {
        if (myBody.velocity.y==0f)
        {
            playerAnim.PlayWalk(Mathf.Abs((int)myBody.velocity.x));
        }
        playerAnim.ChangeFacingDirection((int)myBody.velocity.x);
        playerAnim.PlayJumpAndFall((int)myBody.velocity.y);
    }
    void HandleJumping()
    {
        if (Input.GetButtonDown(TagManager.JUMP_BUTTON))
        {
            if (IsGrounded())
            {
                jumpForce = normalJumpForce;
                Jump();
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    jumpForce = doubleJumpForce;
                    Jump();
                }
            }
            
        }
    }
    bool IsGrounded()
    {
        /*
        groundCast = Physics2D.Raycast(boxCol2D.bounds.center,Vector2.down,boxCol2D.bounds.extents.y+0.03f,groundMask);
        Debug.DrawRay(boxCol2D.bounds.center, Vector2.down * (boxCol2D.bounds.extents.y + 0.03f),Color.red);
        */
        groundCast = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.01f,groundMask);
        Debug.DrawRay(boxCol2D.bounds.center+new Vector3(boxCol2D.bounds.extents.x,0f)
            , Vector2.down * (boxCol2D.bounds.extents.y + 0.01f), Color.red);
        Debug.DrawRay(boxCol2D.bounds.center - new Vector3(boxCol2D.bounds.extents.x, 0f)
            , Vector2.down * (boxCol2D.bounds.extents.y + 0.01f), Color.red);
        Debug.DrawRay(boxCol2D.bounds.center - new Vector3(boxCol2D.bounds.extents.x, boxCol2D.bounds.extents.y+0.01f)
            , Vector2.right * boxCol2D.bounds.size.x, Color.red);

        return groundCast.collider != null;
    }
    void Jump()
    {
        myBody.velocity = Vector2.up * jumpForce;
        jumped = true;
    }
    void CheckToDoubleJump()
    {
        if (!canDoubleJump && myBody.velocity.y==0f)
        {
            canDoubleJump = true;
        }
    }
    void FromJumpToWalkOrIdle()
    {
        if (jumped && myBody.velocity.y==0f)
        {
            jumped = false;
            if (Mathf.Abs((int)myBody.velocity.x)>0f)
            {
                playerAnim.PlayAnimationWithName(TagManager.WALK_ANIMATION_NAME);
            }
            else
            {
                playerAnim.PlayAnimationWithName(TagManager.IDLE_ANIMATION_NAME);
            }
        }
    }
    

}
