using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the options
*/
public class OptionController : ConversationUIElementController {
    private bool isHighlighted = false;
    private ConversationOption option;

    public void SetOption(ConversationOption option) {
        this.option = option;
        textToPrint = option.text;
    }

    public void SetHighlighted(bool highlighted) {
        isHighlighted = highlighted;

        if (isHighlighted) {

            TextBox.color = Color.blue;
        }
        else {
            TextBox.color = Color.black;
        }
    }

    public bool ToggleHighlighted() {
        return (isHighlighted = !isHighlighted);
    }

    public void InvokeEvent() {
        option.optionEvent.Invoke();
    }

    public void SetEnabled(bool enabled) {
        gameObject.SetActive(enabled);
    }

}
