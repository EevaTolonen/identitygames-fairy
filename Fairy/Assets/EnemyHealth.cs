using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyHealth : MonoBehaviour
{
    EnidHealth enidHealth;
    PlatformerCharacter2D platformerCharacter2D;
    Platformer2DUserControl platformer2DUserControl;
    PatrollingEnemy patrollingEnemy;
    
    Animator animator;
    GameObject enemy;

    public int startingHealth = 3;
    public int currentHealth;

    bool damaged;
    bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        enidHealth = GetComponent<EnidHealth>();
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
        platformer2DUserControl = GetComponent<Platformer2DUserControl>();
        patrollingEnemy = GetComponent<PatrollingEnemy>();

        currentHealth = startingHealth;
        animator = GetComponent<Animator>();

        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }



    /// <summary>
    /// Enemy takes damage, this is called from EnidAttack script, and thus is public
    /// </summary>
    /// <param name="amount">how much damage the enemy takes</param>
    /// <param name="hitPoint">The point of death in order to play particle effect etc. when the enemy dies</param>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;
        currentHealth -= amount;
        //patrollingEnemy.speed = 0f;

        Debug.Log("Enemy took damage, health left " + currentHealth);
        if (currentHealth <= 0)
        {
            Death();
        }
    }



    /// <summary>
    /// Enemy dies, and is destroyed after one second of delay, maybe make the enemy sink/turn into particles/etc.?
    /// </summary>
    void Death()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        patrollingEnemy.enabled = false;
        patrollingEnemy.speed = 0f;
        Debug.Log("Enemy died, health left " + currentHealth);
        Destroy(gameObject, 1f);
    }
}
