using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
 
    // game is restarted once function is called (button press)
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}
