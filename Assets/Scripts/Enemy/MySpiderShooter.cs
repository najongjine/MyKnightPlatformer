using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpiderShooter : MonoBehaviour
{
    float shootTimer;
    float shootTimeTreshold=2f;

    [SerializeField]
    GameObject spiderBullet;

    [SerializeField]
    Transform spiderBulletSpawnPoint;

    [SerializeField]
    LayerMask collisionLayer;

    RaycastHit2D playerCast;
    // Start is called before the first frame update
    void Start()
    {
        shootTimer=Time.time+ shootTimeTreshold;
    }

    // Update is called once per frame
    void Update()
    {
        playerCast = Physics2D.Raycast(transform.position, Vector2.down, 100f, collisionLayer);
        Debug.DrawRay(transform.position, Vector2.down * 100f, Color.red);
        ShootBullet();
    }
    void ShootBullet()
    {
        if (!playerCast.collider || Time.time< shootTimer)
        {
            return;
        }
        var obj = Instantiate(spiderBullet, spiderBulletSpawnPoint.position, spiderBulletSpawnPoint.rotation);
        shootTimer = Time.time + shootTimeTreshold;
        Destroy(obj, 3f);
    }
}
