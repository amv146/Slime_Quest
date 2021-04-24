using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public ConversationPieceController conversationController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleOptionNavigation();
    }

    void HandleOptionNavigation() {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            NavigateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            NavigateRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            SelectOption();
        }
    }

    void NavigateLeft() {
        if (conversationController.GetHighlightedOptionNumber() == 0) {
            return;
        }
        else {
            conversationController.SetHighlighted(conversationController.GetHighlightedOptionNumber() - 1);
        }
    }

    void NavigateRight() {
        if (conversationController.GetHighlightedOptionNumber() == 1) {
            return;
        }
        else {
            conversationController.SetHighlighted(conversationController.GetHighlightedOptionNumber() + 1);
        }
    }

    void SelectOption() {

    }
}
