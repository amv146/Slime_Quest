using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the movement on the tiles
*/
public class Movement : MonoBehaviour {
    float moveDuration = 0.3f;
    public int movementAmount = 3;
    public bool readyToMove = true;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void MoveTo(Vector3 position) {
        StartCoroutine(Lerp(position));
    }

    public IEnumerator Lerp(Vector3 position) {
        readyToMove = false;
        float timeElapsed = 0;
        Vector3 originalPosition = transform.position;
        while (timeElapsed < moveDuration) {
            transform.position = Vector3.Lerp(originalPosition, position, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = position;
        readyToMove = true;
    }
}