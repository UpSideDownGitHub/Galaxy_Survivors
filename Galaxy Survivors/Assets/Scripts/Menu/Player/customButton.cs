using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class customButton : MonoBehaviour
{
    public Button button;
    public int buttonID;
    public PlayerMenuManager manager;

    public void setUp(int ID, PlayerMenuManager man)
    {
        buttonID = ID;
        manager = man;
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonPressed);

    }

    public void buttonPressed()
    {
        manager.buttonPressed(buttonID);
    }
}
