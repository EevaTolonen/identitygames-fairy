﻿// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

// Handles enemy (vihu) attacking
public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3;
    public int attackDamage = 1;

    EnidHealth enidHealth;
    EnemyHealth enemyHealth;

    GameObject player;
    Rigidbody2D enemy;

    Animator animator;
    PatrollingEnemy patrollingEnemy;

    bool playerInRange;
    float hitTimer;


    // Start is called before the first frame update
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("PlayerBody");
        enidHealth = player.GetComponent<EnidHealth>();

        patrollingEnemy = GetComponent<PatrollingEnemy>();

        // we need to know which enemy deals damage to player to determine knockback direction 
        enemy = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// Check when enemy trigger collides with player, and is then in range to attack player
    /// </summary>
    /// <param name="other">playerbody collider that isn't Enid's sword</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }


    /// <summary>
    /// Checks when enemy trigger no longer collides with player, enemy is no longer in range to attack
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        hitTimer += Time.deltaTime;
        animator.SetFloat("HitTimer", hitTimer);
        if (hitTimer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();     
        }

        if (enidHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
            patrollingEnemy.enabled = false;
           
        }
    }


    /// <summary>
    /// Makes player take damage from attack and checks that player isn't dead
    /// </summary>
    void Attack()
    {
        hitTimer = 0;
        if (enidHealth.currentHealth > 0)
        {
            animator.SetTrigger("Attack");
            enidHealth.TakeDamage(attackDamage, enemy.transform.position);
        }
    }
}
