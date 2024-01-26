using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private bool isSprinting = false;
    [SerializeField] private AudioClip playerJumpSound;

    public int score;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        //user input (left/right) corresponds to horizontal axis allowing player to move
        horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Incorrect input");
        }

        if (Input.GetKey(KeyCode.LeftShift) && isSprinting == false)
        {
            speed = 15;
            isSprinting = true;
        }
        else
        {
            speed = 10;
            isSprinting = false;
        }


        //Player facing left/right depending on user input (flips player sprite)
        if (horizontalInput > 0.01f) //going right
        {
            transform.localScale = Vector3.one; 
        }
        else if(horizontalInput < -0.01f) //going left
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        


        //wall jumping
        if (wallJumpCooldown > 0.2f)
        {
            if (wallDetection() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
                Jumping();
        }
        else 
            wallJumpCooldown += Time.deltaTime;



        //animation 
        animator.SetBool("Running", horizontalInput != 0);
        animator.SetBool("Grounded", isGrounded());



        if (Input.GetKey(KeyCode.S))
        {
            Data.Save();
            print("saving");
        }
        if (Input.GetKey(KeyCode.L))
        {
            Data.Load();
            print("loading");
        }

    }

    private void Jumping()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetTrigger("Jumping");
            SoundManagement.instance.PlaySound(playerJumpSound);
        }
        else if (wallDetection() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                SoundManagement.instance.PlaySound(playerJumpSound);
            }
            else 
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                SoundManagement.instance.PlaySound(playerJumpSound);
            }
            wallJumpCooldown = 0;
            
        }
    }

   private bool isGrounded()
   {
       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
       return raycastHit.collider != null;
   }
    private bool wallDetection()
   {
       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
       return raycastHit.collider != null;
   }

   public bool canAttack()
   {
       return isGrounded () && !wallDetection();
   }


    //gem pickup

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
        }
    }



        
}
