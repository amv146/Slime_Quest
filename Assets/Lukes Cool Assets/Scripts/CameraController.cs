/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the camera and how it follows the character
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float Distance = 20.0f;
    public float Height = 10.0f;
    public float Damping = 5.0f;
    public bool SmoothRotation = true;
    public bool FollowBehind = true;
    public float RotationDamping = 10.0f;

    void Update ()
    {
        //Camera follows at angle behind the Character
        Vector3 wantedPos;
        if(FollowBehind)
                wantedPos = Target.TransformPoint(0, Height, -Distance);
        else
                wantedPos = Target.TransformPoint(0, Height, Distance);

        transform.position = Vector3.Lerp (transform.position, wantedPos, Time.deltaTime * RotationDamping);

        if (SmoothRotation)
        {
                Quaternion wantedRotation = Quaternion.LookRotation(Target.position - transform.position, Target.up);
                transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * RotationDamping);
        }
        else transform.LookAt (Target, Target.up);
    }
}
