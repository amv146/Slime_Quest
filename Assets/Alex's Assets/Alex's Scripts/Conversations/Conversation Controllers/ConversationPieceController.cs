using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ConversationPieceController : MonoBehaviour {
    ConversationPiece currentPiece;

    public OptionController[] optionControllers;
    public DialogueController dialogueController;
    private bool _isReadyToPrintOptions;

    // Use this for initialization
    void Start() {
        ConversationPiece piece = new ConversationPiece();
        piece.text = "hello luke is bad";

        ConversationOption option = new ConversationOption();
        option.text = "kill luke";

        piece.AddOption(option);
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
            optionControllers[i].SetHighlighted(true);
            if (i >= currentPiece.options.Count) {
                optionControllers[i].SetEnabled(false);
                continue;
            }
            else {
                ConversationOption newOption = currentPiece.options[i];
                optionControllers[i].SetOption(currentPiece.options[i]);
            }
        }
    }
}
