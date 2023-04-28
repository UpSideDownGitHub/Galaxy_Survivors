using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // public varaible
    public GameObject pauseMenu;

    // will resume the game
    public void resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    // will load the main menu
    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
