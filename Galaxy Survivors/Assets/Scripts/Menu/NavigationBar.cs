using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject[] navTexts;

    public GameObject previousMenu;
    public GameObject previousNavText;

    public void openMenu(int ID)
    {
        previousMenu.SetActive(false);
        previousNavText.SetActive(false);

        menus[ID].SetActive(true); 
        navTexts[ID].SetActive(true);

        previousMenu = menus[ID];
        previousNavText = navTexts[ID];
    }
}
