using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    void Start()
    {
        // Initially, the pause menu should be deactivated.
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Check if the Escape key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Activate the pause menu, freeze the game time, and set isPaused to true.
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Corrected the assignment operator (=) from (-)
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Deactivate the pause menu, resume the game time, and set isPaused to false.
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Set the time scale back to 1 to resume the game.
        isPaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartScene(re)");
    }

    public void QUIT()
    { 
        Application.Quit();
    }

        
}
