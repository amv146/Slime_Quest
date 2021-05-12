using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody PlayerRigidBody;

    public SpriteRenderer PlayerSpriteRenderer;

    public Sprite FrontSprite;

    public Sprite BackSprite;

    public float MoveSpeed;

    private Vector2 moveInput;
    
    public Animator Anim;


    private bool flipX = true;
    private bool flipY = true;

    //public VectorValue startingPos;


    void Start()
    {
        //transform.position = startingPos.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        PlayerRigidBody.velocity = new Vector3(moveInput.x * MoveSpeed, PlayerRigidBody.velocity.y, moveInput.y * MoveSpeed);


        if(!flipX && moveInput.x < 0)
        {
            flipX = true;
            Anim.SetTrigger("Flip");

        }
        else if(flipX && moveInput.x > 0)
        {
            flipX = false;
            Anim.SetTrigger("Flip");
        }
        if(!flipY && moveInput.y < 0)
        {
            flipY = true;
            Anim.SetTrigger("FlipY");
            Anim.SetBool("FacingForwards",true);
        }
        else if(flipY && moveInput.y > 0)
        {
            flipY = false;
            Anim.SetTrigger("FlipY");
            Anim.SetBool("FacingForwards",false);

        }
    }
}
