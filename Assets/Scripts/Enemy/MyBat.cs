using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyBat : MonoBehaviour
{
    Rigidbody2D theRB;

    float xDir, yDir;
    float changeDirTimer = 0.1f;
    float moveSpeed = 1f;
    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(changeDir());
    }
    void FixedUpdate()
    {
        theRB.velocity = new Vector2(xDir * moveSpeed, yDir * moveSpeed);
    }
    IEnumerator changeDir()
    {
        yield return new WaitForSeconds(changeDirTimer);
        while (xDir==0f && yDir==0f)
        {
            xDir = Random.Range(0, 2);
            yDir = Random.Range(0, 2);
        }
        changeDirTimer = Random.Range(1f,2f);
        StartCoroutine(changeDir()); 
    }
}
