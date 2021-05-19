﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the tutorial in terms of its text and how it functions
*/
public class TutorialText : MonoBehaviour
{
    public Text TutText;
    public TileGridManager TileGrid;

    public CharacterController Character;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(textWait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //
    IEnumerator textWait()
    {
        TileGrid.changeTurns();
        TutText.text = "So Since you are ready to fight here is how combat works";
        yield return new WaitForSeconds(5f);
        TutText.text = "As you can see in the top left there is a Turn UI that keeps track of whos turn it is";
        yield return new WaitForSeconds(5f);
        TutText.text = "Also, if you look below either me or you, you can see 5 bars which is your health";
        yield return new WaitForSeconds(5f);
        TutText.text = "If you get attacked with a spell you will lose one bar at 0 bars you lose";
        yield return new WaitForSeconds(5f);
        TutText.text = "Now for Movement, on your turn you can hover over the grid and can look at where you can move";
        TileGrid.changeTurns();
        yield return new WaitForSeconds(5f);
        TutText.text = "After making a path you can click on a location to move there which uses your entire turn";
        while(TileGrid.GetComponent<TileGrid>().IsPlayerTurn == true)
        {
            yield return new WaitForSeconds(1f);
        }
        TutText.text = "Another thing that can use your turn is using an attack which is similar to moving";
        yield return new WaitForSeconds(5f);
        TutText.text = "To attack you need to press A and you will see the location where your attack will go";
        TileGrid.changeTurns();

        while(TileGrid.GetComponent<TileGrid>().IsPlayerTurn == true)
        {
            yield return new WaitForSeconds(1f);
        }
        TutText.text = "Right now since you are a begineer you can only attack in a box around the location";
        yield return new WaitForSeconds(5f);
        TutText.text = "Now that you understand the basics try to defeat me";
        yield return new WaitForSeconds(5f);
        TileGrid.changeTurns();
        TutText.text = "";
        while(Enemy.GetComponent<CharacterController>().IsAlive() && Character.IsAlive())
        {
            if(!TileGrid.GetComponent<TileGrid>().IsPlayerTurn)
            {
                int moveDir;
                Vector3 pos = Enemy.transform.position;
                while(true)
                {
                    moveDir = Random.Range(0, 3);
                    //LeftUp 0 z+
                    //RIghtUp 1 x+
                    //LeftDown 2 x-
                    //RightDown 3 z-
                    if(moveDir == 0)
                    {
                        if(Enemy.transform.position.z < 5)
                        {
                            pos.z += 1;
                            break;
                        }
                    }
                    else if(moveDir == 1)
                    {
                        if(Enemy.transform.position.x < 5)
                        {
                            pos.x += 1;
                            break;
                        }
                    }
                    else if(moveDir == 2)
                    {
                        if(Enemy.transform.position.z > 0)
                        {
                            pos.z -= 1;
                            break;
                        }
                    }
                    else if(moveDir == 3)
                    {
                        if(Enemy.transform.position.x > 0)
                        {
                            pos.x -= 1;
                            break;
                        }
                    }
                }
                Enemy.GetComponent<CharacterController>().MoveTo(pos);
                while(!Enemy.GetComponent<CharacterController>().readyToMove)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                Enemy.GetComponent<CharacterController>().currentTile = TileGrid.GetComponent<TileGrid>().GetTileAt((int)Enemy.transform.position.x,(int)Enemy.transform.position.z);
                Debug.Log("X: "+Enemy.GetComponent<CharacterController>().currentTile.tileX+" Z: "+Enemy.GetComponent<CharacterController>().currentTile.tileZ);
                TileGrid.changeTurns();
            }
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("DojoInterior");
        
    }
}
