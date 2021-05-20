
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC245-01/CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the playerprefs for the controls for the character
*   This code is used in the Menu which is the final project for my(Luke Driscoll) unity programming class(CPSC245) that I am currently taking
*   I(Luke Driscoll) am the only person who has worked on this section
*   I needed to include this menu inside this project to show the functionality of the menu
*/
public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite EnemySprite;
    public int EnemyHealth;
    public GameObject Player;
    public PlayerController PlayerCntr;
    public GameObject Character;
    public bool InCombat = false;
    //At Start check if keybindings exist if not make them
    //IF they do exist then update the characters keybindings.
    void Start()
    {
        if(!InCombat)
        {
            PlayerCntr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Debug.Log(1);
        }
        else
        {
            PlayerCntr = GameObject.FindGameObjectWithTag("PlayerCombat").GetComponent<PlayerController>();
        }
        if(PlayerPrefs.HasKey("KeybindingUp"))
        {
            // The Key Exists
            string up = PlayerPrefs.GetString("KeybindingUp");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), up);
            PlayerCntr.MoveUpKeybinding = key;
        } 
        else
        {
            PlayerPrefs.SetString("KeybindingUp", "W");
            string up = PlayerPrefs.GetString("KeybindingUp");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), up);
            PlayerCntr.MoveUpKeybinding = key;
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingDown"))
        {
            // The Key Exists
            string down = PlayerPrefs.GetString("KeybindingDown");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), down);
            PlayerCntr.MoveDownKeybinding = key;

        } 
        else
        {
            PlayerPrefs.SetString("KeybindingDown", "S");
            string down = PlayerPrefs.GetString("KeybindingDown");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), down);
            PlayerCntr.MoveDownKeybinding = key;
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingLeft"))
        {
            // The Key Exists
            string left = PlayerPrefs.GetString("KeybindingLeft");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), left);
            PlayerCntr.MoveLeftKeybinding = key;

        } 
        else
        {
            PlayerPrefs.SetString("KeybindingLeft", "A");
            string left = PlayerPrefs.GetString("KeybindingLeft");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), left);
            PlayerCntr.MoveLeftKeybinding = key;
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingRight"))
        {
            // The Key Exists
            string right = PlayerPrefs.GetString("KeybindingRight");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), right);
            PlayerCntr.MoveRightKeybinding = key;

        } 
        else
        {
            PlayerPrefs.SetString("KeybindingRight", "D");
            string right = PlayerPrefs.GetString("KeybindingRight");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), right);
            PlayerCntr.MoveRightKeybinding = key;
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingAbiltyOne"))
        {
            // The Key Exists
            string abilityone = PlayerPrefs.GetString("KeybindingAbiltyOne");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilityone);
            Character.GetComponent<CharacterController>().KeybindingAbilityOne = key;
        } 
        else
        {
            PlayerPrefs.SetString("KeybindingAbiltyOne", "Alpha1");
            string abilityone = PlayerPrefs.GetString("KeybindingAbiltyOne");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilityone);
            Character.GetComponent<CharacterController>().KeybindingAbilityOne = key;
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingAbiltyTwo"))
        {
            // The Key Exists
            string abilitytwo = PlayerPrefs.GetString("KeybindingAbiltyTwo");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilitytwo);
            Character.GetComponent<CharacterController>().KeybindingAbilityTwo = key;
        } 
        else
        {
            PlayerPrefs.SetString("KeybindingAbiltyTwo", "Alpha2");
            string abilitytwo = PlayerPrefs.GetString("KeybindingAbiltyTwo");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilitytwo);
            Character.GetComponent<CharacterController>().KeybindingAbilityTwo = key;
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingPauseMenu"))
        {
            // The Key Exists
            string pause = PlayerPrefs.GetString("KeybindingPauseMenu");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), pause);
            PlayerCntr.PauseKeybinding = key;
        } 
        else
        {
            PlayerPrefs.SetString("KeybindingPauseMenu", "Escape");
            string pause = PlayerPrefs.GetString("KeybindingPauseMenu");
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), pause);
            PlayerCntr.PauseKeybinding = key;
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
    }
    //Updating Keybinding to the newkeybinding string
    public void UpdateKeybinding(string KeybindingName, string newKeybinding)
    {
        Debug.Log("Updating "+KeybindingName +" To "+ newKeybinding);
        if(KeybindingName == "KeybindingUp")
        {
            PlayerPrefs.SetString("KeybindingUp", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            PlayerCntr.MoveUpKeybinding = key;
        }
        else if(KeybindingName == "KeybindingDown")
        {
            PlayerPrefs.SetString("KeybindingDown", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            PlayerCntr.MoveDownKeybinding = key;
        }
        else if(KeybindingName == "KeybindingLeft")
        {
            PlayerPrefs.SetString("KeybindingLeft", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            PlayerCntr.MoveLeftKeybinding = key;
        }
        else if(KeybindingName == "KeybindingRight")
        {
            PlayerPrefs.SetString("KeybindingRight", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            PlayerCntr.MoveRightKeybinding = key;
        }
        else if(KeybindingName == "KeybindingAbiltyOne")
        {
            PlayerPrefs.SetString("KeybindingAbiltyOne", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            Character.GetComponent<CharacterController>().KeybindingAbilityOne = key;
        }
        else if(KeybindingName == "KeybindingAbiltyTwo")
        {
            PlayerPrefs.SetString("KeybindingAbiltyTwo", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            Character.GetComponent<CharacterController>().KeybindingAbilityTwo = key;
        }
        else if(KeybindingName == "KeybindingPauseMenu")
        {
            PlayerPrefs.SetString("KeybindingPauseMenu", newKeybinding);
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKeybinding);
            PlayerCntr.PauseKeybinding = key;
        }
        // Save the preference to an external file
        
        PlayerPrefs.Save();
        //Update Keybindings
        if(PlayerPrefs.HasKey("KeybindingUp"))
        {
            // The Key Exists
            string up = PlayerPrefs.GetString("KeybindingUp");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), up);
            PlayerCntr.MoveUpKeybinding = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingDown"))
        {
            // The Key Exists
            string down = PlayerPrefs.GetString("KeybindingDown");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), down);
            PlayerCntr.MoveDownKeybinding = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingLeft"))
        {
            // The Key Exists
            string left = PlayerPrefs.GetString("KeybindingLeft");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), left);
            PlayerCntr.MoveLeftKeybinding = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingRight"))
        {
            // The Key Exists
            string right = PlayerPrefs.GetString("KeybindingRight");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), right);
            PlayerCntr.MoveRightKeybinding = key;
        } 
        if(PlayerPrefs.HasKey("KeybindingAbiltyOne"))
        {
            // The Key Exists
            string abilityone = PlayerPrefs.GetString("KeybindingAbiltyOne");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilityone);
            Character.GetComponent<CharacterController>().KeybindingAbilityOne = key;
        } 
        if(PlayerPrefs.HasKey("KeybindingAbiltyTwo"))
        {
            // The Key Exists
            string abilitytwo = PlayerPrefs.GetString("KeybindingAbiltyTwo");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilitytwo);

        } 
        if(PlayerPrefs.HasKey("KeybindingPauseMenu"))
        {
            // The Key Exists
            string pause = PlayerPrefs.GetString("KeybindingPauseMenu");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), pause);
            PlayerCntr.PauseKeybinding = key;
        }
        PlayerPrefs.Save();
    }
    //Gets a keycode given the keybinding
    public KeyCode GetKeybind(string keybinding)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keybinding));
    }
    //Checks if a keycode is a duplicate
    public bool IsDuplicate(KeyCode key)
    {
        
        KeyCode up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingUp"));
        KeyCode down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingDown"));
        KeyCode left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingLeft"));
        KeyCode right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingRight"));
        KeyCode abilityone = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingAbiltyOne"));
        KeyCode abilitytwo = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingAbiltyTwo"));
        KeyCode pausemenu = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeybindingPauseMenu"));
        if(key == up || key == down || key == left || key == right || key == abilityone || key == abilitytwo || key == pausemenu)
        {
            return true;
        }
        else
        {
            return  false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
