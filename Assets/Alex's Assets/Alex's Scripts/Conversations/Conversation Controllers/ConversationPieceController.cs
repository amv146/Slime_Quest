using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the conversation pieces
*/
public class ConversationPieceController : MonoBehaviour {
    public ConversationPiece conversationPiece;
    public OptionController[] optionControllers;
    public DialogueController dialogueController;
    private int numControllersDonePrinting;

    private void PrintOptions() {
        foreach (OptionController optionController in optionControllers) {
            optionController.PrintText(TryToHighlight);
        }
    }

    public void BeginPiece(ConversationPiece piece) {
        numControllersDonePrinting = 0;
        conversationPiece = piece;
        dialogueController.Reset();
        dialogueController.SetText(piece.text);
        for (int i = 0; i < optionControllers.Length; ++i) {
            if (i >= piece.options.Count) {
                optionControllers[i].SetEnabled(false);
                optionControllers[i].Reset();
                continue;
            }
            else {
                optionControllers[i].SetOption(piece.options[i]);
            }
        }

        dialogueController.PrintText(PrintOptions);
    }

    public void TryToHighlight() {
        numControllersDonePrinting++;
        if (conversationPiece.options.Count == 0) {
            conversationPiece.highlightedOption = -1;
        }
        else if (numControllersDonePrinting == conversationPiece.options.Count) {
            SetHighlighted(0);
        }
    }

    public void SetHighlighted(int optionNum) {
        if (optionNum >= 2) {
            return;
        }
        for (int i = 0; i < optionControllers.Length; ++i) {
            if (i == optionNum) {
                optionControllers[i].SetHighlighted(true);
                conversationPiece.highlightedOption = i;
            }
            else {
                optionControllers[i].SetHighlighted(false);
            }
        }
    }

    public int GetHighlightedOptionNumber() {
        return conversationPiece.highlightedOption;
    }

    public string GetNextPieceIDFromOption(int index) {
        return conversationPiece.GetNextDialogue(index);
    }

    public void InvokeEvents(int optionNum) {
        optionControllers[optionNum].InvokeEvent();
    }
}
