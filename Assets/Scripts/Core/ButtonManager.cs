using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource MenuSelect; // Audio clip for when a button is pressed
    public GameObject PlotMenu; // game object that will take the user to a screen where the plot is displayed

    // Method that will start a level with a string passed in as a parameter E.g. start = Levels/Levels3
    public void StartGame(string start)
    {
        MenuSelect.Play(); // Menu button sound plays
        PlayerPrefs.DeleteAll(); // All player statistics are reset e.g. lives, score, health etc..
        SceneManager.LoadScene(start); // A new level is loaded
    }

    // Method that takes the user to the plot screen
    public void Plot()
    {
        MenuSelect.Play(); // Menu button sound plays
        PlotMenu.SetActive(true); // The PlotMenu was in the background the whole time, this segment just sets it's visibility so that it can be seen
    }

    // Method that will exit the game entirely
    public void QuitGame()
    {
        MenuSelect.Play(); // Menu button sound plays
        Application.Quit(); // Game is exited
    }

    // Method that will take the user back to the main menu when the string 'Levels/Level1' is passed in
    public void MainMenu(string start)
    {
        MenuSelect.Play(); // Menu button sound plays
        SceneManager.LoadScene(start); // Scene is loaded
    }
}
