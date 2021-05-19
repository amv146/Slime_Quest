using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the turn changing UI
*/
public class TextController : MonoBehaviour
{
    public Text TurnText;
    // Start is called before the first frame update
    void Start()
    {
        TurnText.text =  "Enemy Turn";
    }
    //Updates the UI
    public void UpdateUI(bool isPlayerTurn)
    {
        if(isPlayerTurn)
        {
            TurnText.text = "Your Turn";
        }
        else
        {
            TurnText.text = "Enemy Turn";
        }
    }
}
