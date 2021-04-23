using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public ConversationPieceController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Input.GetKeyDown   
    }

    void HandleOptionNavigation() {
        if (Input.GetKeyDown(KeyCode.A)) {
            NavigateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            NavigateRight();
        }
    }

    void NavigateLeft() {

    }

    void NavigateRight() {

    }
}
