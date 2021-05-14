using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
