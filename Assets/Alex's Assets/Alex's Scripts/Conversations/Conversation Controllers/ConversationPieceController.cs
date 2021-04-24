using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ConversationPieceController : MonoBehaviour {
    ConversationPiece currentPiece;

    public OptionController[] optionControllers;
    public DialogueController dialogueController;
    private bool _isReadyToPrintOptions;
    private int highlightedOption;

    // Use this for initialization
    void Start() {
        ConversationPiece piece = new ConversationPiece();
        piece.text = "hello luke is bad";

        ConversationOption option = new ConversationOption();
        option.text = "kill luke";

        ConversationOption option2 = new ConversationOption();
        option2.text = "yep luke";

        piece.AddOption(option);
        piece.AddOption(option2);
        GoToNextPiece(piece);
    }

    // Update is called once per frame
    void Update() {

    }

    public void GoToNextPiece(ConversationPiece piece) {
        if (piece == null) {
            return;
        }
        else {
            SetNextPiece(piece);
            StartNextPiece();
        }
    }

    private void StartNextPiece() {
        dialogueController.PrintText(PrintOptions);
    }

    private void PrintOptions() {
        foreach (OptionController optionController in optionControllers) {
            optionController.PrintText();
        }
    }

    private void SetNextPiece(ConversationPiece piece) {
        currentPiece = piece;
        dialogueController.SetText(piece.text);
        for (int i = 0; i < optionControllers.Length; ++i) {
            if (i >= currentPiece.options.Count) {
                optionControllers[i].SetEnabled(false);
                continue;
            }
            else {
                optionControllers[i].SetOption(currentPiece.options[i]);
            }
        }

        SetHighlighted(0);
    }

    public void SetHighlighted(int optionNum) {
        if (optionNum >= 2) {
            return;
        }
        for (int i = 0; i < optionControllers.Length; ++i) {
            if (i == optionNum) {
                optionControllers[i].SetHighlighted(true);
                highlightedOption = i;
            }
            else {
                optionControllers[i].SetHighlighted(false);
            }
        }
    }

    public int GetHighlightedOptionNumber() {
        return highlightedOption;
    }
}
