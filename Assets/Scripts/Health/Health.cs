using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool isDead;

    [Header("iFrames")]
    [SerializeField] private float invinciblePeriod;
    [SerializeField] private float flashAmount;
    private SpriteRenderer render;


    [SerializeField] private GameObject patroller;
    [SerializeField] private AudioClip replenishSound;
    [SerializeField] private AudioClip playerDamageSound;
    [SerializeField] private GameObject droppedHeart;

    [SerializeField] private GameObject key;
    [SerializeField] private GameObject gem;

    [SerializeField] private bool isBoss;



    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    
    public void Damage(float _damage)
    {
        //when the damage function is called, the current health will be reduced from starting health by the value of _damage
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        // if the current health is greater than zero, the hurt animation will play
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            animator.SetTrigger("hurt");
            SoundManagement.instance.PlaySound(playerDamageSound);
            // period where player cannot take damage immediately after having taken damage
            StartCoroutine(Invincibility());
        }
        // else, the player's health is zero, so play the death animation
        else
        {
            if(!isDead)
            {
                animator.SetTrigger("Dead");
                animator.SetTrigger("die");

                //disable playermovement so the player can't move when they are dead
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                    isDead = true;
                    Invoke("Death", 1.5f);  //waits 1.5 seconds to allow death animation to finish, then loads death screen (loads scene)
                }
                
               // same logic as above, for enemies
                if (GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    GetComponent<Collider2D>().enabled = false;
                    Destroy(patroller, 3);
                }
                    
                if (GetComponent<MeleeEnemy>() != null)
                {
                   // if the enemy is a boss, it should drop a key and a gem when they die for the player to collect
                    if(isBoss == true)
                    {
                        var instantiationPoint = new Vector2(transform.position.x+10, transform.position.y+1);
                        var droppedKey = Instantiate(key, instantiationPoint, Quaternion.identity);

                        var instantiationPoint2 = new Vector2(transform.position.x, transform.position.y + 1);
                        var droppedGem = Instantiate(gem, instantiationPoint2, Quaternion.identity);
                        GetComponent<MeleeEnemy>().enabled = false;
                        GetComponent<Collider2D>().enabled = false;
                        Destroy(patroller, 3);
                    }
                    // if the enemy is not a boss, it should only drop a gem when they die for the player to collect
                    else
                    {
                        var instantiationPoint = new Vector2(transform.position.x, transform.position.y + 1);
                        var droppedGem = Instantiate(gem, instantiationPoint, Quaternion.identity);
                        GetComponent<MeleeEnemy>().enabled = false;
                        GetComponent<Collider2D>().enabled = false;
                        Destroy(patroller, 3);
                    }
               
                  
                }


                
            }
            
        }
    }


    public void Death()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    public void Replenish(float _amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + _amount, 0, startingHealth);
        SoundManagement.instance.PlaySound(replenishSound);
    }

    //period of invincibility which occurs after they take damage
    private IEnumerator Invincibility()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < flashAmount; i++)
        {
            render.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invinciblePeriod / (flashAmount * 2));
            render.color = Color.white;
            yield return new WaitForSeconds(invinciblePeriod / (flashAmount * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    

}

   
