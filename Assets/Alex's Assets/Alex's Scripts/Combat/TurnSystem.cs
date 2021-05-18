using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public Queue<CharacterController> turnQueue;
    public CharacterController currentPlayer;
    const int MAX_MOVES = 2;
    int movesLeft;


    void ChangeTurn() {
        currentPlayer = turnQueue.Dequeue();
        turnQueue.Enqueue(currentPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
