using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWormSoldier : MonoBehaviour
{
    Rigidbody2D theRB;
    SpriteRenderer sr;
    CapsuleCollider2D boxCol2D;

    [SerializeField]
    Transform groundDetector;

    float xdir = 1f;
    public float moveSpeed = 1f;

    float randomSwitchDirTreshold = 3f;
    float randomSwitchDirTimer;

    bool bGroundAhead = true;
    [SerializeField]
    LayerMask groundMask;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        theRB = GetComponent<Rigidbody2D>();
        boxCol2D = GetComponent<CapsuleCollider2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        randomSwitchDirTimer = Time.time + randomSwitchDirTreshold;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGround();

        if (Time.time > randomSwitchDirTimer)
        {
            if (Random.Range(0, 3) >= 2)
            {
                xdir = 0;
                randomSwitchDirTimer = Time.time + (randomSwitchDirTreshold * 0.4f);
            }
            else
            {
                if (bGroundAhead)
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        xdir = -1f;
                    }
                    else
                    {
                        xdir = 1f;
                    }
                }
                randomSwitchDirTimer = Time.time + randomSwitchDirTreshold;
            }
            
        }
        switchDir();




    }

    private void FixedUpdate()
    {
        theRB.velocity= new Vector2(xdir * moveSpeed, theRB.velocity.y);
    }
    private void CheckForGround()
    {
        bGroundAhead = Physics2D.Raycast(groundDetector.position, Vector2.down, 0.5f, groundMask);
        if (!bGroundAhead)
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
            if (transform.localScale.x > 0)
            {
                xdir = -1f;
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (transform.localScale.x < 0)
            {
                xdir = 1f;
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
        Debug.DrawRay(groundDetector.position, Vector2.down * 0.5f, Color.red);
    }
    void switchDir()
    {
        if (xdir > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (xdir < 0)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

}
