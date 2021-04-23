using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationPiece
{
    [Multiline]
    public string text;
    public List<ConversationOption> options = new List<ConversationOption>(2);
    public string id;
    public string targetID;
    public Sprite image;

    public void AddOption(ConversationOption option) {
        options.Add(option);
    }


}
