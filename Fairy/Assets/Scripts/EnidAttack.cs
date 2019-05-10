using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnidAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;

    EnidHealth enidHealth;
    Animator animator;



    // the current enemy player collides with
    private GameObject boss;
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
        enidHealth = GetComponent<EnidHealth>();
        
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
        if (other.gameObject.tag == "Boss")
        {
            enemyInRange = true;
            enemy = other.gameObject;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            enemyInRange = false;
        }
    }



    void Attack()
    {
        if (enemy != null)
        {
            animator.SetTrigger("Attack");

            if (enemy.tag == "Enemy")
            {
                if (enemy.GetComponent<EnemyHealth>().currentHealth > 0)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage, Vector3.zero);
                } 
            } else if (enemy.tag == "Boss")
            {
                enemy.GetComponent<WeepingWillow>().TakeHit();
            }
            timer = 0f;
        }

    }
}
