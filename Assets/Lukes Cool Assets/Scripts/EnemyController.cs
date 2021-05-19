using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the enemy and how the encounters work
*/
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private float Speed = 10.0f;
    public GameObject Player;
    public Vector3 Target;
    private Vector3 Position;
    public bool CanMove;
    public List<GameObject> Enemies;
    
    void Start()
    {
        Position = gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        //move sprite towards the target location
        if(CanMove)
        {
            float Step = Speed * Time.deltaTime;
            Target = Player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, Target, Step);
        }
    }
    //When player enters trigger allow the enemy to move towards it
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            //Move towards Player
            CanMove = true;
        }
    }
}
