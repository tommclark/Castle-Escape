using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, pauseButton, levelSelectButton;


    private void Update()
    {
        // when the user presses P, the pause menu will appear
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        //make the menu visible and hide the pause button
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        //pause time
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        //hide the pause menu and make the pause button visible
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        //resume time
        Time.timeScale = 1f;
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
