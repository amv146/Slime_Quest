using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCConversationController : MonoBehaviour
{
    private ConversationPiece currentPiece;
    public ConversationPieceController conversationPieceController;
    public ConversationScript currentScript;

    public void GoToNextPiece(ConversationPiece piece) {
        if (piece == null) {
            return;
        }
        else {
            currentPiece = piece;
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


    public void SelectOption() {
        ConversationPiece nextPiece = currentScript.Get(conversationPieceController.GetNextPieceIDFromOption(GetHighlightedPiece()));
        GoToNextPiece(nextPiece);
    }

    public int GetHighlightedPiece() {
        return conversationPieceController.GetHighlightedOptionNumber();
    }
}
