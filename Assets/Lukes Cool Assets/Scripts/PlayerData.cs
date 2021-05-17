using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject Character;
    void Start()
    {
        if(PlayerPrefs.HasKey("KeybindingUp"))
        {
            // The Key Exists
            string up = PlayerPrefs.GetString("KeybindingUp");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), up);
            player.GetComponent<PlayerController>().MoveUpKeybinding = key;

            //Questions for prate
            //For storing user input should I store the string and parse it to a Keycode
            //Also, is the best way to change the user input to put the user in a loop until they press a button
            //That button will have the new keybinding associated to it unless another keybinding has the same keycode
        } 
        else
        {
            PlayerPrefs.SetString("KeybindingUp", "W");
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingDown"))
        {
            // The Key Exists
            string down = PlayerPrefs.GetString("KeybindingDown");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), down);
            player.GetComponent<PlayerController>().MoveDownKeybinding = key;

        } 
        else
        {
            PlayerPrefs.SetString("KeybindingDown", "S");
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingLeft"))
        {
            // The Key Exists
            string left = PlayerPrefs.GetString("KeybindingLeft");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), left);
            player.GetComponent<PlayerController>().MoveLeftKeybinding = key;

        } 
        else
        {
            PlayerPrefs.SetString("KeybindingLeft", "A");
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingRight"))
        {
            // The Key Exists
            string right = PlayerPrefs.GetString("KeybindingRight");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), right);
            player.GetComponent<PlayerController>().MoveRightKeybinding = key;

        } 
        else
        {
            PlayerPrefs.SetString("KeybindingRight", "D");
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
            PlayerPrefs.SetString("KeybindingAbiltyOne", "1");
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
            PlayerPrefs.SetString("KeybindingAbiltyTwo", "2");
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("KeybindingPauseMenu"))
        {
            // The Key Exists
            string pause = PlayerPrefs.GetString("KeybindingPauseMenu");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), pause);
            player.GetComponent<PlayerController>().PauseKeybinding = key;
        } 
        else
        {
            PlayerPrefs.SetString("KeybindingPauseMenu", "Escape");
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
    }
    public void updateKeybinding(string KeybindingName, string newKeybinding)
    {
        if(KeybindingName == "KeybindingUp")
        {
            PlayerPrefs.SetString("KeybindingUp", newKeybinding);
        }
        else if(KeybindingName == "KeybindingDown")
        {
            PlayerPrefs.SetString("KeybindingDown", newKeybinding);
        }
        else if(KeybindingName == "KeybindingLeft")
        {
            PlayerPrefs.SetString("KeybindingLeft", newKeybinding);
        }
        else if(KeybindingName == "KeybindingRight")
        {
            PlayerPrefs.SetString("KeybindingRight", newKeybinding);
        }
        else if(KeybindingName == "KeybindingAbiltyOne")
        {
            PlayerPrefs.SetString("KeybindingAbiltyOne", newKeybinding);
        }
        else if(KeybindingName == "KeybindingAbiltyTwo")
        {
            PlayerPrefs.SetString("KeybindingAbiltyTwo", newKeybinding);
        }
        else if(KeybindingName == "KeybindingPauseMenu")
        {
            PlayerPrefs.SetString("KeybindingPauseMenu", newKeybinding);
            Debug.Log(6);
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
            player.GetComponent<PlayerController>().MoveUpKeybinding = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingDown"))
        {
            // The Key Exists
            string down = PlayerPrefs.GetString("KeybindingDown");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), down);
            player.GetComponent<PlayerController>().MoveDownKeybinding = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingLeft"))
        {
            // The Key Exists
            string left = PlayerPrefs.GetString("KeybindingLeft");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), left);
            player.GetComponent<PlayerController>().MoveLeftKeybinding = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingRight"))
        {
            // The Key Exists
            string right = PlayerPrefs.GetString("KeybindingRight");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), right);
            player.GetComponent<PlayerController>().MoveRightKeybinding = key;

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
            Character.GetComponent<CharacterController>().KeybindingAbilityTwo = key;

        } 
        if(PlayerPrefs.HasKey("KeybindingPauseMenu"))
        {
            // The Key Exists
            string pause = PlayerPrefs.GetString("KeybindingPauseMenu");
            //Update Keybinding
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), pause);
            player.GetComponent<PlayerController>().PauseKeybinding = key;
            Debug.Log(5);
        }
        PlayerPrefs.Save();
    }
    public KeyCode GetKeybind(string keybinding)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keybinding));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
