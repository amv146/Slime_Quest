using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the health of the enemies and the player
*/
public class HealthController : MonoBehaviour
{
    private TextMesh tm;
    public bool IsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMesh>();
    }

    public int currentHealth()
    {
        if(tm != null)
        {
            return tm.text.Length;
        }
        return 0;
    }
    public void IncreaseHealth()
    {
        if(currentHealth() >= 1 && currentHealth() <= 4)
        {
            tm.text += '-';
        }
    }

    public void DecreaseHealth()
    {
        if(currentHealth() > 1)
        {
            tm.text = tm.text.Remove(tm.text.Length - 1);
        }
        else
        {
            IsAlive = false;
            //End Battle
            transform.parent.gameObject.SetActive(false);
        }
    }
}
