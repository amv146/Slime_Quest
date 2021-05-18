using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private Queue<CharacterController> turnQueue;
    private CharacterController currentPlayer;
    public TileGrid tileGrid {
        set
        {
            turnQueue.Clear();
            foreach (CharacterController character in value.characters) {
                EnqueuePlayer(character);

            }
        }
        get
        {
            return tileGrid;
        }
    }
    const int MAX_MOVES = 2;
    private int movesLeft;
    private bool hasAttacked = false;

    // Start is called before the first frame update
    void Start() {
        movesLeft = MAX_MOVES;
    }

    void ChangeTurn() {
        SetPlayerTurn();
        EnqueuePlayer(currentPlayer);
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

    }

    public void UseMove() {

    }

    public bool IsPlayerTurn() {
        return (currentPlayer == GameObject.FindGameObjectWithTag("Player"));
    }
}
