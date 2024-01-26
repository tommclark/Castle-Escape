using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementArea;
    [SerializeField] private float speed;
    private bool left;
    private float leftEdge;
    private float rightEdge;

    [SerializeField] private AudioClip playerDamageSound;
    [SerializeField] private AudioClip enemyDamageSound;

    private void Awake()
    {
        leftEdge = transform.position.x - movementArea;
        rightEdge = transform.position.x + movementArea;
    }
    private void Update()
    {
        if (left)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                left = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                left = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player collides with enemy, player will take damage (lose health)
        if (collision.tag == "Player")
        {
            SoundManagement.instance.PlaySound(playerDamageSound);
            collision.GetComponent<Health>().Damage(damage);
        }

        if (collision.tag == "Knife")
        {
            SoundManagement.instance.PlaySound(enemyDamageSound);

        }
    }
}
