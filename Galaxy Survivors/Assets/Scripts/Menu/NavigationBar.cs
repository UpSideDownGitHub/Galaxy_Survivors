using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject[] navTexts;

    public GameObject previousMenu;
    public GameObject previousNavText;

    // will open the menu corresponding to the button pressed
    public void openMenu(int ID)
    {
        // turn of the previous menu 
        previousMenu.SetActive(false);
        previousNavText.SetActive(false);

        // enable the current menu
        menus[ID].SetActive(true); 
        navTexts[ID].SetActive(true);

        // set the current to be the previous
        previousMenu = menus[ID];
        previousNavText = navTexts[ID];
    }
}
