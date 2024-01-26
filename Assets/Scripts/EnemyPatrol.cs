using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
  // MOST OF THIS CODE IS NOT USED- TRANSFERRED INTO MELEEENEMY CLASS
    
    
    [Header("Patrol Limits")]
   
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
   
    [Header("Movement Limits")]
    [SerializeField] private float movementSpeed;
    private Vector3 startingScale;
    private bool isLeft;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [SerializeField] private AudioClip enemyDamageSound;


    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        startingScale = enemy.localScale;
    }


    private void Update()
    {
        
       // if (isLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                StopMovement();
            }
            else
            {
                StopMovement();
            }
            
        }
       // else
        {
          if (enemy.position.x <= rightEdge.position.x)
            {
                StopMovement();
            }
            else
            {
                StopMovement();
            }
            
        }
        
    }

    private void ChangeDirection()
    {
        animator.SetBool("moving", false);
        isLeft = !isLeft;
    }

    private void MoveInDirection(int _direction)

    {
        animator.SetBool("moving", true);
        
        //Enemy faces direction of movement
        enemy.localScale = new Vector3(Mathf.Abs(startingScale.x) * _direction, startingScale.y, startingScale.z);
        //enemy moves in the direction above
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * movementSpeed, enemy.position.y, enemy.position.z);
        
    }

    private void StopMovement()
    {
        rb2d.velocity = new Vector2(0, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Knife")
        {
            SoundManagement.instance.PlaySound(enemyDamageSound);
            print("Take damage sound played");

        }
    }
}
