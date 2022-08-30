using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDelegate : MonoBehaviour
{
    [SerializeField]
    float scaleTime = 1f;

    Vector3 myScale;
    bool canScale;
    BoxCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        Key.keyCollectedInfo += UnlockDoor;
    }
    private void OnDisable()
    {
        Key.keyCollectedInfo -= UnlockDoor;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Unlock();
    }
    void Unlock()
    {
        if (canScale)
        {
            myScale = transform.localScale;
            myScale.y -= scaleTime * Time.deltaTime;
            if (myScale.y <= 0f)
            {
                myScale.y = 0f;
                myCollider.enabled = false;
                canScale = false;
            }

            transform.localScale = myScale;
        }
    }
    void UnlockDoor()
    {
        canScale = true;
    }
}
