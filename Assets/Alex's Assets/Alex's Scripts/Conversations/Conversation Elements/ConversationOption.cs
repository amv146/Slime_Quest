using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the conversation options
*/
public class ConversationOption : ScriptableObject
{
    public string text;
    public string nextDialogueID;
    public UnityEvent optionEvent;

    public ConversationOption() {

    }

    public ConversationOption(string text = "", string nextDialogueID = "") {
        this.text = text;
        this.nextDialogueID = nextDialogueID;
        this.optionEvent = null;
    }
}
