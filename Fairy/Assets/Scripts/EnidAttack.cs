﻿// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

// Handles player attack
public class EnidAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    float timer;

    EnidHealth enidHealth;
    Animator animator;

    private GameObject boss;
    // the current enemy player collides with
    private GameObject enemy;
    public GameObject[] enemies;


    bool enemyInRange;
    bool canPlayerAttack;
    int attackDamage = 1;


    // Start is called before the first frame update
    void Awake()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enidHealth = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<EnidHealth>();

        animator = transform.parent.gameObject.GetComponent<Animator>();
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


    /// <summary>
    /// Checks which enemy type player collides with
    /// </summary>
    /// <param name="other">collider of the enemy</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EvilTree")
        {
            enemyInRange = true;
            // here we take the right enemy to a variable so that we can access it when doing damage
            enemy = other.gameObject;
        }
        if (other.gameObject.tag == "Boss" || other.gameObject.tag == "BossFairy")
        {
            enemyInRange = true;
            enemy = other.gameObject;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss" || other.gameObject.tag == "BossFairy" || other.gameObject.tag == "EvilTree")
        {
            enemyInRange = false;
        }
    }


    // Handles player attacking enemies, applying damage to different enemies and stopping attack damage if enemy is dead
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
            }
            else if (enemy.tag == "EvilTree")
            {
                if (enemy.GetComponent<EvilTreeHealth>().currentHealth > 0)
                {
                    enemy.GetComponent<EvilTreeHealth>().TakeDamage(attackDamage, Vector3.zero);
                }
            }
            else if (enemy.tag == "Boss")
            {
                enemy.GetComponent<WeepingWillow>().TakeHit();
            }
            else if (enemy.tag == "BossFairy")
            {
                GameObject.FindGameObjectWithTag("Boss").GetComponent<WeepingWillow>().FairyHit();
            }

            timer = 0f;
        }

    }
}
