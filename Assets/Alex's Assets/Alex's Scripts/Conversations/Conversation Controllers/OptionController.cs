using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

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
        option.unityEvent.Invoke();
    }

    public void SetEnabled(bool enabled) {
        gameObject.SetActive(enabled);
    }

}
