using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConversationUIElementController : MonoBehaviour {
    protected string textToPrint = "";
    protected bool _shouldPrintText = false;
    protected int framesLeftToWait;

    public Text TextBox;
    public int TextWaitFrames;

    private void FixedUpdate() {
        if (ShouldPrintText()) {
            if (IsFinishedWaiting()) {
                UpdateText();
            }
        }
        else {
            SetShouldPrintText(false);
        }
    }

    protected void UpdateText() {
        char newLetter = textToPrint[0];
        textToPrint = textToPrint.Substring(1);
        TextBox.text += newLetter;
    }

    protected bool ShouldPrintText() {
        return (_shouldPrintText && textToPrint != "");
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
        _shouldPrintText = true;
    }

    public void SetText(string text) {

        textToPrint = text;
    }

    public bool IsDonePrinting() {
        return textToPrint == "";
    }

    virtual protected void SetShouldPrintText(bool value) {
        _shouldPrintText = value;
    }
}
