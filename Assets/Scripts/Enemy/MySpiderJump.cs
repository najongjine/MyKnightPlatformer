using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpiderJump : MonoBehaviour
{
    Animator anim;
    Rigidbody2D theRB;
    BoxCollider2D boxCol2D;

    RaycastHit2D groundCast;

    [SerializeField]
    LayerMask groundMask;

    float xDir = 1f;

    [SerializeField]
    float xForce = 1f, yForce = 1f;

    float jumpTimer;
    float jumpTimerTreshold=2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        theRB=GetComponent<Rigidbody2D>();
        boxCol2D=GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        jumpTimer = Time.time + jumpTimerTreshold;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        PlayAnim();
    }
    private void FixedUpdate()
    {
        if(groundCast.collider && Time.time > jumpTimer)
        {
            jumpTimer = Time.time + jumpTimerTreshold;
            theRB.AddForce(new Vector2(xDir*xForce,yForce),ForceMode2D.Impulse);
        }
    }
    void IsGrounded()
    {
        /*
        groundCast = Physics2D.Raycast(boxCol2D.bounds.center,Vector2.down,boxCol2D.bounds.extents.y+0.03f,groundMask);
        Debug.DrawRay(boxCol2D.bounds.center, Vector2.down * (boxCol2D.bounds.extents.y + 0.03f),Color.red);
        */
        groundCast = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.01f, groundMask);
        Debug.DrawRay(boxCol2D.bounds.center + new Vector3(boxCol2D.bounds.extents.x, 0f)
            , Vector2.down * (boxCol2D.bounds.extents.y + 0.01f), Color.red);
        Debug.DrawRay(boxCol2D.bounds.center - new Vector3(boxCol2D.bounds.extents.x, 0f)
            , Vector2.down * (boxCol2D.bounds.extents.y + 0.01f), Color.red);
        Debug.DrawRay(boxCol2D.bounds.center - new Vector3(boxCol2D.bounds.extents.x, boxCol2D.bounds.extents.y + 0.01f)
            , Vector2.right * boxCol2D.bounds.size.x, Color.red);

    }
    void PlayAnim()
    {
        if (groundCast.collider)
        {
            anim.SetBool(TagManager.JUMP_ANIMATION_PARAM, false);
        }
        else
        {
            anim.SetBool(TagManager.JUMP_ANIMATION_PARAM, true);
        }
    }
}
