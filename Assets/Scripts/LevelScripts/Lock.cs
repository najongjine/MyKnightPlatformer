using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public static Lock instance;

    [SerializeField]
    float scaleTime = 1f;

    Vector3 myScale;
    bool  canScale;
    BoxCollider2D myCollider;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        myCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Unlock();
    }
    void Unlock()
    {
        if( canScale)
        {
            myScale = transform.localScale;
            myScale.y -= scaleTime * Time.deltaTime;
            if (myScale.y<=0f)
            {
                myScale.y = 0f;
                myCollider.enabled = false;
                canScale = false;
            }

            transform.localScale = myScale;
        }
    }
    public void UnlockDoor()
    {
        canScale = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(collision.gameObject.tag==TagManager.PLAYER_TAG && bUnlocked)
        {
            if (unlockCoroutine==null)
            {
                unlockCoroutine = _MyUnlock();
                StartCoroutine(unlockCoroutine);
            }
        }
        */
    }
    IEnumerator _MyUnlock()
    {
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector2(transform.localScale.x,Mathf.Clamp(transform.localScale.y-0.1f,0f,10f));
        if (transform.localScale.y>0){
            StartCoroutine(_MyUnlock());
        }

    }

}
