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
*   This class controls the animations of the characters
*/
public class EventManager : MonoBehaviour
{
    public bool CanAnimate = true;
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim.SetBool("CanAnimate",CanAnimate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Event to start the animation
    private void Event_StartAnimation()
    {
        CanAnimate = false;
        Anim.SetBool("CanAnimate",CanAnimate);

    }  
    //Event to end the animation
    private void Event_EndAnimation()
    {
        CanAnimate = true;
        Anim.SetBool("CanAnimate",CanAnimate);
    }   
}
