using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    public bool hasKey = false;
    public int level;

    
    
    //sets hasKey to true when the player collects the key, allowing them to complete the level
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hasKey = true;
            print("hasKey = true");
        }
           
    
        
        if (hasKey == true)
        {
            if (collision.tag == "Player")
            {
                EndLevel1();
            }   
        }
        

    }

    private void EndLevel1()
    {
        SceneManager.LoadScene("Level2");
    }
}
