using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed = 200f;
    float zAngle;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0,2)>0)
        {
            rotateSpeed *= -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        zAngle+=Time.deltaTime*rotateSpeed;
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagManager.PLAYER_TAG)
        {
            // hurt player
        }
    }

}
