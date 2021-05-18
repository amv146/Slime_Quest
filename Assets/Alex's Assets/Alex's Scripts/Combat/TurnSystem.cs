using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private Queue<CharacterController> turnQueue = new Queue<CharacterController>();
    private CharacterController currentPlayer;
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

    // Start is called before the first frame update
    void Start() {
        movesLeft = MAX_MOVES;
    }

    public void ChangeTurn() {
        SetPlayerTurn();
        EnqueuePlayer(currentPlayer);

        if (!IsPlayerTurn()) {
            currentPlayer.GetComponent<AI>().MoveAlongPath(tileGrid);
        }
    }

    public void EnqueuePlayer(CharacterController character) {
        turnQueue.Enqueue(character);
    }

    void SetPlayerTurn() {
        currentPlayer = turnQueue.Dequeue();
        movesLeft = MAX_MOVES;
        hasAttacked = false;
    }

    public void UseAttack() {
        hasAttacked = true;
        movesLeft--;
        CheckForTurnSwitch();
    }

    public void UseMove() {
        movesLeft--;
        CheckForTurnSwitch();
    }

    private void CheckForTurnSwitch() {
        if (CurrentTurnIsOver()) {
            ChangeTurn();
        }
    }

    public bool IsPlayerTurn() {
        return (currentPlayer.tag == "Player");
    }

    public bool CurrentTurnIsOver() {
        return movesLeft == 0;
    }

    public bool CanCurrentPlayerAttack() {
        return !hasAttacked;
    }
}
