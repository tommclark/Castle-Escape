using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private float amount;

    //when the player collects a heart, health will be replenished with the value specified
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().Replenish(amount);
            gameObject.SetActive(false); 
        }
    }
}
