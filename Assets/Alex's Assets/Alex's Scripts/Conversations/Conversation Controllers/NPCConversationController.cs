using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the NPC conversation
*/
public class NPCConversationController : MonoBehaviour
{
    public ConversationPieceController conversationPieceController;
    public ConversationScript currentScript;

    public void GoToNextPiece(ConversationPiece piece) {
        if (piece == null) {
            return;
        }
        else {
            conversationPieceController.BeginPiece(piece);
        }
    }


    public void StartConversation(ConversationScript conversation) {
        currentScript = conversation;
        GoToNextPiece(currentScript.GetFirstPiece());
    }

    public void NavigateLeft() {
        if (conversationPieceController.GetHighlightedOptionNumber() == 0) {
            return;
        }
        else {
            conversationPieceController.SetHighlighted(conversationPieceController.GetHighlightedOptionNumber() - 1);
        }
    }

    public void NavigateRight() {
        if (conversationPieceController.GetHighlightedOptionNumber() == 1) {
            return;
        }
        else {
            conversationPieceController.SetHighlighted(conversationPieceController.GetHighlightedOptionNumber() + 1);
        }
    }


    public bool SelectOption() {
        int selectedOption = GetHighlightedPiece();
        if (selectedOption != -1) {
            conversationPieceController.InvokeEvents(selectedOption);
        }
        string nextDialogueID = conversationPieceController.GetNextPieceIDFromOption(selectedOption);
        if (nextDialogueID == "None") {
            return false;
        }

        ConversationPiece nextPiece = currentScript.Get(nextDialogueID);
        GoToNextPiece(nextPiece);

        return true;
    }

    public int GetHighlightedPiece() {
        return conversationPieceController.GetHighlightedOptionNumber();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    public void Show() {
        this.gameObject.SetActive(true);
    }
}
