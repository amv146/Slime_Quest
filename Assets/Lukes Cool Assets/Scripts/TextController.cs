using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text TurnText;
    // Start is called before the first frame update
    void Start()
    {
        TurnText.text =  "Enemy Turn";
    }

    public void UpdateUI(bool isPlayerTurn)
    {
        if(isPlayerTurn)
        {
            TurnText.text = "Your Turn";
        }
        else
        {
            TurnText.text = "Enemy Turn";
        }
    }
}
