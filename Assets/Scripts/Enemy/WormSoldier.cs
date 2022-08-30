using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSoldier : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    Vector3 tempPos;
    Vector3 tempScale;

    bool moveLeft;

    [SerializeField]
    LayerMask groundLayer;

    Transform groundCheckPos;

    RaycastHit2D groundHit;

    private void Awake()
    {
        groundCheckPos = transform.GetChild(0).transform;

        moveLeft = Random.Range(0, 2) > 0 ? true : false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        CheckForGround();
    }

    private void CheckForGround()
    {
        groundHit = Physics2D.Raycast(groundCheckPos.position, Vector2.down,0.5f, groundLayer);
        if (!groundHit)
        {
            moveLeft = !moveLeft;
        }
        Debug.DrawRay(groundCheckPos.position,Vector2.down*0.5f,Color.red);
    }

    private void HandleMovement()
    {
        tempPos = transform.position;
        tempScale = transform.localScale;
        if (moveLeft)
        {
            tempPos.x -= moveSpeed * Time.deltaTime;
            tempScale.x = -1f;
        }
        else
        {
            tempPos.x += moveSpeed * Time.deltaTime;
            tempScale.x = 1f;
        }

        transform.position = tempPos;
        transform.localScale = tempScale;
    }
}
