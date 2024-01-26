using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKey(KeyCode.L))
        {
            Load();
        }
    }


    //save user data
    public static void Save()
    {
        int playerScore = ScoreManager.score;
        

        PlayerPrefs.SetInt("playerScore", playerScore);
        PlayerPrefs.Save();
    }

    //load user data
    public static void Load()
    {
        {
            if (PlayerPrefs.HasKey("level1Complete"))
            {
                int playerScore = PlayerPrefs.GetInt("playerScore");
                ScoreManager.UpdateScoreFromSave(playerScore);
            }

        }
    }

}
