using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
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
