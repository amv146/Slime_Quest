using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private Queue<CharacterController> turnQueue = new Queue<CharacterController>();
    public CharacterController currentPlayer;
    private TileGridManager __tileGrid;
    public TileGridManager tileGrid {
        set
        {
            __tileGrid = value;
            turnQueue.Clear();
            foreach (CharacterController character in value.characters) {
                EnqueuePlayer(character);

            }
        }
        get
        {
            return __tileGrid;
        }
    }
    const int MAX_MOVES = 2;
    private int movesLeft;
    private bool hasAttacked = false;
    private bool isMoveOver = true;
    private bool readyToSwitchTurn = true;


    // Start is called before the first frame update
    void Start() {
        movesLeft = MAX_MOVES;
    }

    public bool IsMoveOver() {
        return isMoveOver;
    }

    public void SetMoveOver(bool value) {
        isMoveOver = value;
    }

    public bool ReadyToSwitchTurn() {
        return readyToSwitchTurn;
    }

    public void SetReadyToSwitchTurn(bool value) {
        readyToSwitchTurn = value;
    }

    public void ChangeTurn() {
        CharacterController lastPlayer = currentPlayer;
        SetPlayerTurn();
        EnqueuePlayer(currentPlayer);

        if (!IsPlayerTurn()) {
            StartCoroutine(RunAITurn(currentPlayer.GetComponent<AI>(), lastPlayer));
        }
    }

    public IEnumerator RunAITurn(AI ai, CharacterController enemy) {
        while (!IsPlayerTurn()) {
            readyToSwitchTurn = false;
            SetMoveOver(false);
            yield return StartCoroutine(ai.RunTurn(tileGrid, enemy, CanCurrentPlayerAttack()));
            Debug.Log("Is move over: " + isMoveOver);
            Debug.Log(currentPlayer.name + ": Turns left: " + movesLeft);
            tileGrid.ResetKnockbackTile();
        }
        Debug.Log(tileGrid.KnockbackTileExists());
    }

    public void EnqueuePlayer(CharacterController character) {
        turnQueue.Enqueue(character);
    }

    void SetPlayerTurn() {
        currentPlayer = turnQueue.Dequeue();
        movesLeft = MAX_MOVES;
        hasAttacked = false;
        tileGrid.SelectedObject = currentPlayer;
        tileGrid.mode = GridMode.Move;
    }

    public void UseMove(bool attack) {
        tileGrid.mode = GridMode.Move;
        if (attack) {
            hasAttacked = true;
        }
        movesLeft--;
        Debug.Log(currentPlayer.name + ": Move Used");
        readyToSwitchTurn = true;
        CheckForTurnSwitch();
    }

    public void CheckForTurnSwitch() {
        if (CurrentTurnIsOver()) {
            ChangeTurn();
        }
        tileGrid.mode = GridMode.Move;
        if (IsPlayerTurn()) {
            tileGrid.isHighlightEnabled = true;
        }
        else {
            tileGrid.isHighlightEnabled = false;
        }
    }

    public bool IsPlayerTurn() {
        return (currentPlayer.tag == "Player");
    }

    public bool CurrentTurnIsOver() {
        return movesLeft <= 0;
    }

    public bool CanCurrentPlayerAttack() {
        return !hasAttacked;
    }
}
