using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerJump : MonoBehaviour
{
    Rigidbody2D myBody;
    Animator anim;

    public bool bGround;

    public float jumpForce = 100f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && bGround)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce*Time.deltaTime);
        }
        anim.SetInteger(TagManager.JUMP_ANIMATION_PARAM, (int)myBody.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            bGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            bGround = false;
        }
    }
}
