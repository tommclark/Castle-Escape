using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hitReg;
    private float direction;
    private BoxCollider2D boxCollider;
    private Animator animator;
    [SerializeField] private float damage;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // if the knife hits an object, it will stop 
        if(hitReg)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if knife collides with enemy, apply variable damage to enemy's health 
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().EnemyDamage(damage);

        }

        hitReg = true;
        boxCollider.enabled = false;
        animator.SetTrigger("KnifeHit");

       // do damage if it collides with an enemy
        if (collision.tag == "Patroller")
        {
            collision.GetComponent<Health>().Damage(1);
        }

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().EnemyDamage(1);
        }

    }

    // ensures that the knife is thrown in the correct direction, and that its sprite is the correct orientation
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true); 
        hitReg = false;
        boxCollider.enabled = true;


        float localScaleX = transform.localScale.x;

        //flip knife if player is facing backwards
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }


    // used for knifehit (ensures correct sprite is used for animation after knifehit is activated)
    private void Deactivate()
        {
            gameObject.SetActive(false);
        }



}
