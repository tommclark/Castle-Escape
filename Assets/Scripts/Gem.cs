    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int gemValue = 1;

    //when the player collides with a gem, the score will be updated with the gem's local value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScoreManager.instance.UpdateScore(gemValue);

        }
    }

}
