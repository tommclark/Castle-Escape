using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public static int score;
    public static int highscore;
    
    [SerializeField] private GameObject scoreManager;


    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
 
         score = 0;
 
         highscore = PlayerPrefs.GetInt ("highscore", highscore);

       
    }

    private void Update()
    {
        // if the user's current score is greater than the high score then make the current score the new high score
        if (score > highscore)
        {
            highscore = score;
            text.text = "New High Score: " + score.ToString();

            PlayerPrefs.SetInt("highscore", highscore);
            print("highscore");

        }
    }

    public void UpdateScore(int gemValue)
    {
        score += gemValue;
        text.text = ": " + score.ToString();
    }


    public static void UpdateScoreFromSave(int savedScore)
    {
        score = savedScore;
    
    }

    public void ResetHighScore()
    {
        highscore = 0;
    }

}