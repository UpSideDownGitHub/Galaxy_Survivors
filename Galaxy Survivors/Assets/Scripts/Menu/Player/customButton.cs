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
    public PerksUpgradeManager manager2;

    public void setUp(int ID, PlayerMenuManager man)
    {
        buttonID = ID;
        manager = man;
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonPressed);

    }
    public void setUp(int ID, PerksUpgradeManager man)
    {
        buttonID = ID;
        manager2 = man;
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonPressed2);

    }

    public void buttonPressed()
    {
        manager.buttonPressed(buttonID);
    }
    public void buttonPressed2()
    {
        manager2.buttonPressed(buttonID);
    }
}
