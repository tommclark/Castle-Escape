using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseFunction : MonoBehaviour
{
    public bool isPaused = false;

    private void Update()
    {
        // game is paused once the user presses 
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
            
        }
    }


    public void PauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0.0000000000000000000000001f;
            isPaused = true;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
    }
}