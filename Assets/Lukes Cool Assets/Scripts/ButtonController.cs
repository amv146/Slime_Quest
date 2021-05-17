using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Work on Updating the Text so whenever it is called the text will automatically be the Keybinding: Key
//One way is to have the button's text at start be equal to the Keybinding: Key




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
        Pause
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
    public void updateButtonText()
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
    public void OnButtonPress()
    {

        if(BType == ButtonType.Keybinding)
        {
            StartCoroutine(getButton());
        }
        else if(BType == ButtonType.SceneChanger)
        {
            SceneManager.LoadScene(SceneToChange);
        }
        else if(BType == ButtonType.Resume)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
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
    }
    IEnumerator getButton()
    {
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
                    pData.updateKeybinding(Keybinding,keyString);

                    this.GetComponentInChildren<Text>().text = "Keybinding: " + keyString;
                    isExit = true;
                    break;

                }
            }
            //If it does
            //Go to PlayerData and change the keybinding to new key
            yield return new WaitForSeconds(0.1f);
        }
    }
}
