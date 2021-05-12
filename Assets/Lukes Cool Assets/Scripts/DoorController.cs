using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
