using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public NPCConversationController conversationController;
    private ConversationScript conversationScript;
    // Start is called before the first frame update
    void Start() {
        conversationScript = GameObject.FindGameObjectWithTag("conversation").GetComponent<ConversationScript>();
        conversationController.StartConversation(conversationScript);
    }

    // Update is called once per frame
    void Update() {
        HandleOptionNavigation();
    }

    void HandleOptionNavigation() {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            conversationController.NavigateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            conversationController.NavigateRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (!conversationController.SelectOption()) {
                conversationController.Hide();
            }
        }
    }

    public void SetConversationScript(ConversationScript conversationScript) {
        this.conversationScript = conversationScript;
    }
}
