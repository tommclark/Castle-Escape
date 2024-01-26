using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Patrol Limits")]

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;


    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float sightSize;
    [SerializeField] private float sightDistance;
    [SerializeField] private int damage;
    private float cooldownTime = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    
    private Health playerHealth;
    private Animator animator;

    [Header("Sight")]
    [SerializeField] Transform player;
    [SerializeField] float range;
    [SerializeField] float patrolMovementSpeed;
    private bool isChasingPlayer = false;
    private bool isLeft;
   


    [SerializeField] private float movementSpeed;
    private Vector3 startingScale;


    [SerializeField] private bool isBoss;


    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isLeft)
        {
            // if enemy is on the right of the left edge limit, move to the left
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                // flip enemy sprite
                ChangeDirection();
            }

        }
        else
        {
            // if enemy is on the left of the right edge limit, move to the right
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                // flip enemy sprite
                ChangeDirection();
            }

        }


        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);


        //if player is within range, chase player
        if (distToPlayer < range)
        {
            ChasePlayer();
        }


        // if enemy is at the left or right movement limit and is chasing the player, stop chasing the player to prevent chasing the player across the whole level
        if ((enemy.position.x >= rightEdge.position.x || enemy.position.x <= leftEdge.position.x) && isChasingPlayer == true)
        {
            StopChasingPlayer();
        }


        



        cooldownTime += Time.deltaTime;

        //detect player
        if (CanSeePlayer())
        {
            if (cooldownTime > attackCooldown)
            {
                //attack
                cooldownTime = 0;
                //animation
                animator.SetTrigger("meleeAttack");

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


    // uses raycast to detect the player
    private bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * sightSize * transform.localScale.x * sightDistance, new Vector3(boxCollider.bounds.size.x * sightSize, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    //development purposes- see enemy vision
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * sightSize * transform.localScale.x * sightDistance, new Vector3(boxCollider.bounds.size.x * sightSize, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    private void DamagePlayer()
    {
        if (CanSeePlayer())
        {
            //attack player
            playerHealth.Damage(damage);
        }
    }

    private void ChasePlayer()
    {
        isChasingPlayer = true;
        // if the enemy is to the right of the player
        if (transform.position.x < player.position.x)
        {
            //moving right (player on left)
            
            isLeft = false;
            rb2d.velocity = new Vector2(patrolMovementSpeed, 0);
            //enemy faces player
            if (isBoss == true)
            {
                transform.localScale = new Vector2(4, 4);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
  
        }
        else if (transform.position.x > player.position.x)
        {
            //moving left (player on right)
            isLeft = true;
          
            rb2d.velocity = new Vector2(-patrolMovementSpeed, 0);
            //enemy faces player
            if (isBoss == true)
            {
                transform.localScale = new Vector2(-4, 4);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }

        }
    }

    private void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}
