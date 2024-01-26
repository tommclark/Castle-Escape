using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : Key
{
    public static int level1Complete = 0;



    //if the user has collected the key after killing the boss, level 1 is complete and level 2 will be loaded
    public new void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasKey == true)
            level1Complete = 1;
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
