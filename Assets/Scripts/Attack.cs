using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldown = Mathf.Infinity;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform knifeDirection;
    [SerializeField] private GameObject knife;
    [SerializeField] private float damage;
    [SerializeField] private AudioClip playerAttackSound;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Player can only attack if cooldown is met and correct inputs are entered
        if((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Mouse1) && cooldown > attackCooldown && playerMovement.canAttack()))
        {
            playerAttack();
        }
        cooldown += Time.deltaTime;
    }

    private void playerAttack()
    {
        //play attack sound
        SoundManagement.instance.PlaySound(playerAttackSound);
        animator.SetTrigger("Attack");
        cooldown = 0;

        //move knife
        knife.transform.position = knifeDirection.position; 
        knife.GetComponent<KnifeThrow>().SetDirection(Mathf.Sign(transform.localScale.x));

        //Hide knife after 0.8 seconds to prevent attacking from a large distance
        Invoke("HideKnife", 0.8f);
    }


    private void HideKnife()
    {
        if (knife.activeInHierarchy == true)
        {
            knife.SetActive(false);
        }

    }

}

