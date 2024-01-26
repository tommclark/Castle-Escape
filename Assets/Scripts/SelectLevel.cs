using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private GameObject LevelButton;
    public static int level;
 
   //when the corresponding button is pressed, the correct scene will be loaded
    
    public void OpenLevel1()
    {
        SceneManager.LoadScene("Level1");
        level = 1;
    }

    public void OpenLevel2()
    {
        SceneManager.LoadScene("Level2");
        level = 2;
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        level = 0;
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
