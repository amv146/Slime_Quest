using System;
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
*   This class controls arrow an direction
*/
public class DirectionArrow : MonoBehaviour
{
    float baseScale;
    float fixedHeadScale;
    float totalScale;


    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.Find("ArrowBody").transform.localScale.y;
        fixedHeadScale = transform.Find("ArrowHead").transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(Tile startNode, Tile targetNode) {
        if (startNode == targetNode) {
            gameObject.SetActive(false);
            Debug.Log("Same Tile");
            return;
        }
        gameObject.SetActive(true);


        int xStart = startNode.X;
        int xTarget = targetNode.X;
        int yStart = startNode.Y;
        int yTarget = targetNode.Y;

        float xDistance = (xTarget - xStart);
        float yDistance = (yTarget - yStart);

        float slope = yDistance / xDistance;
        float scale = Mathf.Sqrt(yDistance * yDistance + xDistance * xDistance);

        float angleDeg = 90 - Mathf.Rad2Deg * (float) Math.Atan2(yDistance, xDistance);

        transform.SetPositionAndRotation(new Vector3(xStart, 0, yStart), Quaternion.Euler(0, angleDeg, 0));
        PreserveHeadScale(scale - fixedHeadScale);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scale - fixedHeadScale);
        
    }

    public void PreserveHeadScale(float parentScale) {
        if ((int) parentScale == 0) {
            return;
        }
        Vector3 localScale = transform.Find("ArrowHead").localScale;
        localScale.z = 1.0f / parentScale;

        transform.Find("ArrowHead").localScale = localScale;
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
