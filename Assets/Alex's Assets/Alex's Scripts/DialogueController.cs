using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private bool isTextToPrint = false;
    private bool isDonePrinting = false;
    private string textToPrint = "";
    private int framesLeftToWait;

    public Text DialogueBox;
    public int TextWaitFrames;

    // Start is called before the first frame update
    void Start()
    {
        SetDialogue("hello my name is luke and im gay yep cock I'm a simp yep cock yep cock");
        framesLeftToWait = TextWaitFrames;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (ShouldPrintText()) {
            if (IsFinishedWaiting()) {
                UpdateText();
            }
        }
        else {
            isDonePrinting = true;
        }
    }

    private void UpdateText() {
        char newLetter = textToPrint[0];
        textToPrint = textToPrint.Substring(1);
        print(newLetter);
        DialogueBox.text += newLetter;
    }

    private bool ShouldPrintText() {
        return (textToPrint != "" && !isDonePrinting);
    }

    private bool IsFinishedWaiting() {
        print(framesLeftToWait);
        if (framesLeftToWait <= 0) {
            framesLeftToWait = TextWaitFrames;
            return true;
        }
        else {
            framesLeftToWait--;
            return false;
        }
    }

    public void SetDialogue(string text) {
        isDonePrinting = false;

        textToPrint = text;
    }

    public bool IsDonePrinting() {
        return isDonePrinting;
    }

}
