using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : ConversationUIElementController
{
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
            if (!_shouldPrintText) {
                StartCoroutine(StartMethod());
            }
        }
    }

    public void PrintText(Action action) {
        PrintText();
        method = action;
    }

    protected override void SetShouldPrintText(bool value) {
        shouldPrintText = value;
    }

    IEnumerator StartMethod() {
        yield return new WaitForSeconds(0.2f);
        method();
    }
}
