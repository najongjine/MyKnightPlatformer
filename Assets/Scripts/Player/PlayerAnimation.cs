using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayWalk(int walk)
    {
        anim.SetInteger(TagManager.WALK_ANIMATION_PARAM, walk);
    }
    public void ChangeFacingDirection(int direction)
    {
        if (direction > 0)
        {
            sr.flipX = false;
        }else if (direction<0)
        {
            sr.flipX = true;
        }
    }
    public void ChangeFacingDirection_By_Scale(int direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
    public void PlayJumpAndFall(int jumpFall)
    {
        anim.SetInteger(TagManager.JUMP_ANIMATION_PARAM, jumpFall);
    }
    public void PlayAnimationWithName(string animName)
    {
        anim.Play(animName);
    }

}
