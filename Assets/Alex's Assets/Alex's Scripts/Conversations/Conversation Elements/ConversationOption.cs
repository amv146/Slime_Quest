using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ConversationOption
{
    public string text;
    public string nextDialogueID;
    public UnityEvent unityEvent;
}
