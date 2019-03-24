using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public int attackDamage = 1;

    EnidHealth enidHealth;
    EnemyHealth enemyHealth;

    PlatformerCharacter2D platformerCharacter2D;
    Platformer2DUserControl platformer2DUserControl;

    Animator animator;
    GameObject player;
    PatrollingEnemy patrollingEnemy;

    bool playerInRange;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();

        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
        platformer2DUserControl = GetComponent<Platformer2DUserControl>();

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enidHealth = player.GetComponent<EnidHealth>();

        patrollingEnemy = GetComponent<PatrollingEnemy>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        animator.SetFloat("Timer", timer);
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();     
        }

        if (enidHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
            patrollingEnemy.enabled = false;
        }
    }



    void Attack()
    {
        timer = 0f;
        if (enidHealth.currentHealth > 0)
        {
            animator.SetTrigger("Attack");
            enidHealth.TakeDamage(attackDamage);
        }
    }
}
