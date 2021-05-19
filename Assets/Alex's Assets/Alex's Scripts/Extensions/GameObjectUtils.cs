using UnityEngine;
using System.Collections;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the GameObjectUtils
*/
public static class GameObjectUtils {
    public static GameObject ObjectAt(Vector3 position) {
        Collider[] colliders;
        if ((colliders = Physics.OverlapSphere(position, 0.25f)).Length > 0) //Presuming the object you are testing also has a collider 0 otherwise
        {
            return colliders[0].gameObject;
        }
        else {
            return null;
        }
    }
}
