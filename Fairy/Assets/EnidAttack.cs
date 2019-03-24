using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnidAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;

    EnidHealth enidHealth;
    PlatformerCharacter2D platformerCharacter2D;
    Platformer2DUserControl platformer2DUserControl;

    EnemyHealth enemyHealth;
    Animator animator;

    private GameObject enemy;

    bool enemyInRange;
    bool canPlayerAttack;
    int attackDamage = 1;

    float timer;


    // Start is called before the first frame update
    void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        enidHealth = GetComponent<EnidHealth>();

        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
        platformer2DUserControl = GetComponent<Platformer2DUserControl>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        /*if (timer >= timeBetweenAttacks)
        {
            canPlayerAttack = true;
            animator.SetBool("CanPlayerAttack", canPlayerAttack);
            }
        */

        if (timer >= timeBetweenAttacks && enemyInRange && Input.GetButton("Attack") && enidHealth.currentHealth > 0)
        {
            Attack();
        }

        if (timer >= timeBetweenAttacks && Input.GetButton("Attack"))
        {
            animator.SetTrigger("Attack");
            timer = 0f;
            //canPlayerAttack = false;
        }
        // TO DO: TÄHÄN JÄÄTIIN, TSEKKAA VIDEO
        // Kun Enid painaa E:tä ja on tarpeeksi lähelläs vihua:
        // tekee damagea vihuun
        // vihun helat vähenee yhdellä, yhteensä kaksi helttiä koska ei jaksa enempää
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            enemyInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            enemyInRange = false;
        }
    }

    void Attack()
    {
        if (enemyHealth.currentHealth > 0)
        {
            animator.SetTrigger("Attack");
            enemyHealth.TakeDamage(attackDamage, Vector3.zero);
        }
        timer = 0f;
    }
}
