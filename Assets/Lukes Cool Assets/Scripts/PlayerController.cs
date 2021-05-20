
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC245-01/CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the player and how it moves and animates
*   This code is used in the Menu which is the final project for my(Luke Driscoll) unity programming class(CPSC245) that I am currently taking
*   I(Luke Driscoll) am the only person who has worked on this section
*   I needed to include this menu inside this project to show the functionality of the menu
*/
public class PlayerController : MonoBehaviour
{
    public Rigidbody PlayerRigidBody;
    public bool IsInGame = true;
    public SpriteRenderer PlayerSpriteRenderer;

    public Sprite FrontSprite;

    public Sprite BackSprite;

    public float MoveSpeed;

    private Vector2 MoveInput;
    
    public Animator Anim;

    public KeyCode MoveUpKeybinding;
    public KeyCode MoveDownKeybinding;
    public KeyCode MoveLeftKeybinding;
    public KeyCode MoveRightKeybinding;

    public KeyCode PauseKeybinding;
    public GameObject PauseMenu;
    public GameObject DataController;


    private bool flipX = true;
    private bool flipY = true;

    //public VectorValue startingPos;


    void Start()
    {
        if(IsInGame)
        {
            //Makes the character move to its previous posistion in the scene
            string sceneName = SceneManager.GetActiveScene().name;
            if (SceneDataManager.Instance.ScenePositions.ContainsKey(sceneName)) {
                transform.position = SceneDataManager.Instance.ScenePositions[SceneManager.GetActiveScene().name];
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //Gets user input
        if(Input.GetKey(PauseKeybinding) && IsInGame)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
           
        }
        if(Input.GetKey(MoveUpKeybinding))
        {
            MoveInput.y = 1;
        }
        else if(Input.GetKey(MoveDownKeybinding))
        {
            MoveInput.y = -1;
        }
        else
        {
            MoveInput.y = 0;
        }
        if(Input.GetKey(MoveLeftKeybinding))
        {
            MoveInput.x = -1;
        }
        else if(Input.GetKey(MoveRightKeybinding))
        {
            MoveInput.x = 1;
        }
        else
        {
            MoveInput.x = 0;
        }
        
        //Moves Character
        PlayerRigidBody.velocity = new Vector3(MoveInput.x * MoveSpeed, PlayerRigidBody.velocity.y, MoveInput.y * MoveSpeed);

        //Flip animations
        if(!flipX && MoveInput.x < 0)
        {
            flipX = true;
            Anim.SetTrigger("Flip");

        }
        else if(flipX && MoveInput.x > 0)
        {
            flipX = false;
            Anim.SetTrigger("Flip");
        }
        if(!flipY && MoveInput.y < 0)
        {
            flipY = true;
            Anim.SetTrigger("FlipY");
            Anim.SetBool("FacingForwards",true);
        }
        else if(flipY && MoveInput.y > 0)
        {
            flipY = false;
            Anim.SetTrigger("FlipY");
            Anim.SetBool("FacingForwards",false);

        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Enemy")
        {
            if(other.gameObject.transform.position.x >= (this.gameObject.transform.position).x && other.gameObject.transform.position.x <= (this.gameObject.transform.position).x)
            {
                Debug.Log("Start Encounter");
                DataController.GetComponent<PlayerData>().EnemySprite = other.gameObject.GetComponent<EnemyController>().EnemySprite;
                DataController.GetComponent<PlayerData>().EnemyHealth = other.gameObject.GetComponent<EnemyController>().Health;
                SceneManager.LoadScene("Combat");
            }
            
            
        }
    }
}
