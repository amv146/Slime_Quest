using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public bool CanAnimate = true;
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetBool("CanAnimate",CanAnimate);
    }
    private void Event_StartAnimation()
    {
        CanAnimate = false;

    }  
    private void Event_EndAnimation()
    {
        CanAnimate = true;
    }   
}
