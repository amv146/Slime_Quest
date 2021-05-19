
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC245-01/CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the buttons and how they work
*   This code is used in the Menu which is the final project for my(Luke Driscoll) unity programming class(CPSC245) that I am currently taking
*   I(Luke Driscoll) am the only person who has worked on this section
*   I needed to include this menu inside this project to show the functionality of the menu
*/
public class ButtonController : MonoBehaviour
{
    public string Keybinding;
    public GameObject PauseMenu;
    public string SceneToChange;
    public enum ButtonType
    {
        SceneChanger,
        Keybinding,
        Exit,
        Resume,
        Pause,
        PauseMenu
    }
    public ButtonType BType;

    public GameObject PlayerData;
    PlayerData pData;

    // Start is called before the first frame update
    void Start()
    {
        if(BType == ButtonType.Keybinding)
        {
            pData = PlayerData.GetComponent<PlayerData>();
            this.GetComponentInChildren<Text>().text = "Keybinding: "+pData.GetKeybind(Keybinding);
        }
    }
    //Updates the text of the button
    public void UpdateButtonText()
    {
        if(BType == ButtonType.Keybinding)
        {
            this.GetComponentInChildren<Text>().text = "Keybinding: "+pData.GetKeybind(Keybinding);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //Each button does something different on press
    public void OnButtonPress()
    {

        if(BType == ButtonType.Keybinding)
        {
            StartCoroutine(GetButton());
        }
        else if(BType == ButtonType.SceneChanger)
        {
            SceneManager.LoadScene(SceneToChange);
        }
        else if(BType == ButtonType.Resume)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            PlayerPrefs.Save();
        }
        else if(BType == ButtonType.Exit)
        {
            Application.Quit();
        }
        else if(BType == ButtonType.Pause)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if(BType == ButtonType.PauseMenu)
        {
            PauseMenu.SetActive(true);
        }
    }
    //Runs the keybinding changer
    IEnumerator GetButton()
    {
        //List of all keycodes
        List<KeyCode> keyCodes = new List<KeyCode>();
        keyCodes.Add(KeyCode.Alpha0);
        keyCodes.Add(KeyCode.Alpha1);
        keyCodes.Add(KeyCode.Alpha2);
        keyCodes.Add(KeyCode.Alpha3);
        keyCodes.Add(KeyCode.Alpha4);
        keyCodes.Add(KeyCode.Alpha5);
        keyCodes.Add(KeyCode.Alpha6);
        keyCodes.Add(KeyCode.Alpha7);
        keyCodes.Add(KeyCode.Alpha8);
        keyCodes.Add(KeyCode.Alpha9);
        keyCodes.Add(KeyCode.Alpha0);
        keyCodes.Add(KeyCode.A);
        keyCodes.Add(KeyCode.B);
        keyCodes.Add(KeyCode.C);
        keyCodes.Add(KeyCode.D);
        keyCodes.Add(KeyCode.E);
        keyCodes.Add(KeyCode.F);
        keyCodes.Add(KeyCode.G);
        keyCodes.Add(KeyCode.H);
        keyCodes.Add(KeyCode.I);
        keyCodes.Add(KeyCode.J);
        keyCodes.Add(KeyCode.K);
        keyCodes.Add(KeyCode.O);
        keyCodes.Add(KeyCode.P);
        keyCodes.Add(KeyCode.Q);
        keyCodes.Add(KeyCode.R);
        keyCodes.Add(KeyCode.S);
        keyCodes.Add(KeyCode.T);
        keyCodes.Add(KeyCode.U);
        keyCodes.Add(KeyCode.V);
        keyCodes.Add(KeyCode.W);
        keyCodes.Add(KeyCode.X);
        keyCodes.Add(KeyCode.Y);
        keyCodes.Add(KeyCode.Z);
        keyCodes.Add(KeyCode.Backspace);
        keyCodes.Add(KeyCode.Delete);
        keyCodes.Add(KeyCode.Tab);
        keyCodes.Add(KeyCode.Return);
        keyCodes.Add(KeyCode.Escape);
        keyCodes.Add(KeyCode.Space);
        keyCodes.Add(KeyCode.UpArrow);
        keyCodes.Add(KeyCode.DownArrow);
        keyCodes.Add(KeyCode.LeftArrow);
        keyCodes.Add(KeyCode.RightArrow);
        keyCodes.Add(KeyCode.DoubleQuote);
        keyCodes.Add(KeyCode.Comma);
        keyCodes.Add(KeyCode.Slash);
        keyCodes.Add(KeyCode.Semicolon);
        keyCodes.Add(KeyCode.Period);
        keyCodes.Add(KeyCode.LeftCurlyBracket);
        keyCodes.Add(KeyCode.RightCurlyBracket);
        keyCodes.Add(KeyCode.LeftControl);
        keyCodes.Add(KeyCode.RightControl);
        keyCodes.Add(KeyCode.LeftCommand);
        keyCodes.Add(KeyCode.RightCommand);
        keyCodes.Add(KeyCode.LeftShift);
        keyCodes.Add(KeyCode.RightShift);
        keyCodes.Add(KeyCode.Tilde);
        keyCodes.Add(KeyCode.RightAlt);
        keyCodes.Add(KeyCode.LeftAlt);
        bool isExit = false;
        //Wait for next button to be pressed
        while(!isExit)
        {
            //If button is pressed
            //Check if the Button equals any enum
            foreach (KeyCode key in keyCodes)
            {

                if(Input.GetKey(key))
                {
                    //Parse the keycode into a string
                    string keyString = key.ToString();
                    //Change Specified Keybinding to this Keycode
                    KeyCode keycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
                    Debug.Log("Keycode Recognized");
                    //CHecks if duplicate
                    if(!pData.IsDuplicate(keycode))
                    {
                        Debug.Log("Not Duplicate");
                        //Updates keybinding and its text
                        pData.UpdateKeybinding(Keybinding,keyString);
                        this.GetComponentInChildren<Text>().text = "Keybinding: " + keyString;
                        isExit = true;
                    }
                    else
                    {
                        Debug.Log("Is Duplicate");
                        isExit = true;
                    }
                    break;

                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
