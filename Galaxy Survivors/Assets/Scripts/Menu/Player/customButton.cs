using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class customButton : MonoBehaviour
{
    // public variables
    public Button button;
    public int buttonID;
    public PlayerMenuManager manager;
    public PerksUpgradeManager manager2;

    // set up the button for the player menu 
    public void setUp(int ID, PlayerMenuManager man)
    {
        // set the ID and the manager to call then add the listener
        buttonID = ID;
        manager = man;
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonPressed);

    }
    // set up the button for the perk menu
    public void setUp(int ID, PerksUpgradeManager man)
    {
        // set the ID and the manager to call then add the listener
        buttonID = ID;
        manager2 = man;
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonPressed2);

    }

    // called when the button is pressed
    public void buttonPressed()
    {
        manager.buttonPressed(buttonID);
    }
    // called when the button is pressed
    public void buttonPressed2()
    {
        manager2.buttonPressed(buttonID);
    }
}
