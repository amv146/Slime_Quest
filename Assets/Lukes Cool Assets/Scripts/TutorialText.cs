using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    public Text tutorialText;
    public GameObject textController;
    public GameObject tileGrid;

    public CharacterController character;
    public CharacterController enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(textWait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator textWait()
    {
        tileGrid.GetComponent<TileGrid>().changeTurns();
        tutorialText.text = "So Since you are ready to fight here is how combat works";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "As you can see in the top left there is a Turn UI that keeps track of whos turn it is";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Also, if you look below either me or you, you can see 5 bars which is your health";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "If you get attacked with a spell you will lose one bar at 0 bars you lose";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Now for Movement, on your turn you can hover over the grid and can look at where you can move";
        tileGrid.GetComponent<TileGrid>().changeTurns();
        yield return new WaitForSeconds(5f);
        tutorialText.text = "After making a path you can click on a location to move there which uses your entire turn";
        while(tileGrid.GetComponent<TileGrid>().IsPlayerTurn == true)
        {
            yield return new WaitForSeconds(1f);
        }
        tutorialText.text = "Another thing that can use your turn is using an attack which is similar to moving";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "To attack you need to press A and you will see the location where your attack will go";
        tileGrid.GetComponent<TileGrid>().changeTurns();

        while(tileGrid.GetComponent<TileGrid>().IsPlayerTurn == true)
        {
            yield return new WaitForSeconds(1f);
        }
        tutorialText.text = "Right now since you are a begineer you can only attack in a box around the location";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Now that you understand the basics try to defeat me";
        yield return new WaitForSeconds(5f);
        tileGrid.GetComponent<TileGrid>().changeTurns();
        tutorialText.text = "";
        while(enemy.IsAlive() && character.IsAlive())
        {
            if(!tileGrid.GetComponent<TileGrid>().IsPlayerTurn)
            {
                int moveDir;
                Vector3 pos = enemy.transform.position;
                while(true)
                {
                    moveDir = Random.Range(0, 3);
                    //LeftUp 0 z+
                    //RIghtUp 1 x+
                    //LeftDown 2 x-
                    //RightDown 3 z-
                    if(moveDir == 0)
                    {
                        if(enemy.transform.position.z < 5)
                        {
                            pos.z += 1;
                            break;
                        }
                    }
                    else if(moveDir == 1)
                    {
                        if(enemy.transform.position.x < 5)
                        {
                            pos.x += 1;
                            break;
                        }
                    }
                    else if(moveDir == 2)
                    {
                        if(enemy.transform.position.z > 0)
                        {
                            pos.z -= 1;
                            break;
                        }
                    }
                    else if(moveDir == 3)
                    {
                        if(enemy.transform.position.x > 0)
                        {
                            pos.x -= 1;
                            break;
                        }
                    }
                }
                enemy.MoveTo(pos);
                while(!enemy.readyToMove)
                {
                    yield return new WaitForSeconds(1f);
                }
                enemy.currentTile = tileGrid.GetComponent<TileGrid>().GetTileAt((int)enemy.transform.position.x,(int)enemy.transform.position.y);
                tileGrid.GetComponent<TileGrid>().changeTurns();
            }
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("DojoInterior");
        
    }
}
