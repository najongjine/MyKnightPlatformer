using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingSpike : MonoBehaviour
{
    Rigidbody2D myBody;

    [SerializeField]
    LayerMask collisionLayer;

    RaycastHit2D playerCast;

    bool collidedWithPlayer;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        playerCast = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, collisionLayer);
        Debug.DrawRay(transform.position, Vector2.down * Mathf.Infinity, Color.red);
        if (playerCast)
        {
            myBody.gravityScale = 1f;
        }
        */
        CheckForPlayerCollision();
    }
    void CheckForPlayerCollision()
    {
        if (collidedWithPlayer)
        {
            return;
        }
        playerCast = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, collisionLayer);
        Debug.DrawRay(transform.position, Vector2.down * 500f, Color.red);
        if (playerCast.collider)
        {
            collidedWithPlayer = true;
            myBody.gravityScale = 1f;
            Destroy(gameObject, 3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag==TagManager.PLAYER_TAG)
        {
            //hurt player
            Destroy(gameObject);
        }
    }
}
