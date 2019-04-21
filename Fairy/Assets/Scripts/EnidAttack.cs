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
    


    // the current enemy player collides with
    private GameObject enemy;
    public GameObject[] enemies;


    bool enemyInRange;
    bool canPlayerAttack;
    int attackDamage = 1;

    float timer;


    // Start is called before the first frame update
    void Awake()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyHealth = GetComponent<EnemyHealth>();
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
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyInRange = true;
            // here we take the right enemy () to a variable so that we can access it when doing damage
            enemy = other.gameObject;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyInRange = false;
        }
    }



    void Attack()
    {
        if (enemy != null)
        {
            if (enemy.GetComponent<EnemyHealth>().currentHealth > 0)
            {
                animator.SetTrigger("Attack");
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage, Vector3.zero);
            }
            timer = 0f;
        }

    }
}
