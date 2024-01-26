using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;


    //locates player
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        //camera follows the player as they move
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        transform.position = temp;

        Vector3 temp1 = transform.position;
        //camera is slightly ahead of the player to indicate the direction they should move
        temp1.y = playerTransform.position.y + 2;
        transform.position = temp1;

           
    }
}
