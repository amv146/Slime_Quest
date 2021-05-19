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
*   This class controls the Conversation pierces
*/
[System.Serializable]
public class ConversationPiece
{
    [Multiline]
    public string text;
    public List<ConversationOption> options = new List<ConversationOption>();
    public string id;
    public string nextDialogueID;
    public int highlightedOption = -1;
    public Sprite image;

    public void AddOption(ConversationOption option) {
        options.Add(option);
    }

    public string GetNextDialogue(int index) {
        if (nextDialogueID != null && nextDialogueID != "" && index == -1) {
            return nextDialogueID;
        }
        else {
            return options[index].nextDialogueID;
        }
    }


}
