using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool isDead;
    public GameObject enemy;


    // maxiumum health on startup
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    
    public void EnemyDamage(float _damage)
    {
        //subtracts _damage from current health when damage has been dealt by the player
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
          
        }
        else
        {
            if (!isDead)
            {
                GetComponent<EnemyMovement>().enabled = false;
                enemy.gameObject.SetActive(false);
                isDead = true;
            }

        }

     
    }
}


