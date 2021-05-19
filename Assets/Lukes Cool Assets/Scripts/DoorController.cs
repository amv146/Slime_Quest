using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the door of the house and animates it when the trigger is entered
*/
public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation Anim;
    void OnTriggerEnter(Collider other)
    {
        Anim.Play("Door Open");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {}

}
