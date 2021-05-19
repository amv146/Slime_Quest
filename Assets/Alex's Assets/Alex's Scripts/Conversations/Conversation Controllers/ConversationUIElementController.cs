using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the conversation UI
*/
public class ConversationUIElementController : MonoBehaviour {
    protected string textToPrint = "";
    protected bool _shouldPrintText = false;
    protected int framesLeftToWait;

    public Text TextBox;
    public int TextWaitFrames;

    public Action method;

    public bool shouldPrintText
    {
        get
        {
            return _shouldPrintText;
        }
        set
        {
            _shouldPrintText = value;
            if (!_shouldPrintText && method != null) {
                StartCoroutine(StartMethod());
            }
        }
    }

    private void FixedUpdate() {
        if (ShouldPrintText()) {
            if (IsFinishedWaiting()) {
                UpdateText();
            }
        }
        else {
            shouldPrintText = false;
        }
    }

    protected void UpdateText() {
        char newLetter = textToPrint[0];
        textToPrint = textToPrint.Substring(1);
        TextBox.text += newLetter;
    }

    protected bool ShouldPrintText() {
        return (shouldPrintText && textToPrint != "");
    }

    protected bool IsFinishedWaiting() {
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

    public void PrintText() {
        method = null;
        _shouldPrintText = true;
    }

    public void SetText(string text) {
        textToPrint = text;
    }

    public void Reset() {
        TextBox.text = "";
        framesLeftToWait = TextWaitFrames;
    }

    public bool IsDonePrinting() {
        return textToPrint == "";
    }

    virtual protected void SetShouldPrintText(bool value) {
        _shouldPrintText = value;
    }

    IEnumerator StartMethod() {
        yield return new WaitForSeconds(0.2f);
        if (method != null) {
            method();
        }
        method = null;
    }


    public void PrintText(Action action) {
        PrintText();
        method = action;
    }

}
