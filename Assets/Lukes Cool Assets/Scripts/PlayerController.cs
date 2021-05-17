using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody PlayerRigidBody;

    public SpriteRenderer PlayerSpriteRenderer;

    public Sprite FrontSprite;

    public Sprite BackSprite;

    public float MoveSpeed;

    private Vector2 moveInput;
    
    public Animator Anim;

    public KeyCode MoveUpKeybinding;
    public KeyCode MoveDownKeybinding;
    public KeyCode MoveLeftKeybinding;
    public KeyCode MoveRightKeybinding;

    public KeyCode PauseKeybinding;
    public GameObject PauseMenu;


    private bool flipX = true;
    private bool flipY = true;

    //public VectorValue startingPos;


    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (SceneDataManager.Instance.scenePositions.ContainsKey(sceneName)) {
            transform.position = SceneDataManager.Instance.scenePositions[SceneManager.GetActiveScene().name];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(PauseKeybinding))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
           
        }
        if(Input.GetKey(MoveUpKeybinding))
        {
            moveInput.y = 1;
        }
        else if(Input.GetKey(MoveDownKeybinding))
        {
            moveInput.y = -1;
        }
        else
        {
            moveInput.y = 0;
        }
        if(Input.GetKey(MoveLeftKeybinding))
        {
            moveInput.x = -1;
        }
        else if(Input.GetKey(MoveRightKeybinding))
        {
            moveInput.x = 1;
        }
        else
        {
            moveInput.x = 0;
        }
        

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
