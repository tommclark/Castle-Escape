using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool isDead;

    [Header("iFrames")]
    [SerializeField] private float invinciblePeriod;
    [SerializeField] private float flashAmount;
    private SpriteRenderer render;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    public void Damage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            // period where player cannot take damage immediately after having taken damage
            StartCoroutine(Invincibility());
        }
        else
        {
            if (!isDead)
            {
                animator.SetTrigger("Die");
                
                if (GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }
                
                if (GetComponent<MeleeEnemy>() != null)
                {
                    GetComponent<MeleeEnemy>().enabled = false;
                }

                isDead = true;


            }

        }
    }

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


