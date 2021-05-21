using System.Collections;
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
        Time.timeScale = 0;
        TutText.text = "So Since you are ready to fight here is how combat works";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "As you can see in the top left there is a Turn UI that keeps track of whos turn it is";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "Also, if you look below either me or you, you can see 5 bars which is your health";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "If you get attacked with a spell you will lose one bar at 0 bars you lose";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "Now for Movement, on your turn you can hover over the grid and can look at where you can move";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "After making a path you can click on a location to move there which uses your entire turn";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "Another thing that can use your turn is using an attack which is similar to moving";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "To attack you need to press A and you will see the location where your attack will go";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "Right now since you are a begineer you can only attack in a box around the location";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "Now that you understand the basics try to defeat me";
        yield return new WaitForSecondsRealtime(5f);
        TutText.text = "";
        Time.timeScale = 1;
    }
}
