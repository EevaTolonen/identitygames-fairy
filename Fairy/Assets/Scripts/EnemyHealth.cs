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
    Rigidbody2D enemyBody;

    public int startingHealth = 3;
    public int currentHealth;

    bool damaged;
    bool isDead;

    float knockbackTimer;

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
        enemyBody = enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackTimer > 0) knockbackTimer += Time.deltaTime;
        if (knockbackTimer > 3)
        {
            // here we allow the enemy to move after being stunned for 3 seconds
            patrollingEnemy.enabled = true;
            enemyBody.freezeRotation = false;
            enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            knockbackTimer = 0;

            // line checks if player backstabbed the enemy, then enemy switches from patrolling to approaching player
            if (patrollingEnemy.GetComponent<Animator>().GetBool("isPatrolling") == true)
            {
                patrollingEnemy.ApproachEnid();
            }

        }
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

        // disables the enemyPatrol script and freezes rotation in order to give the enemy a "stun time"
        patrollingEnemy.enabled = false;
        enemyBody.freezeRotation = true;

        // enemy is knocked back from the player damage. and can't move for 3 seconds (time set in the knockbacktimer)
        enemyBody.AddForce(new Vector2(800, 800), ForceMode2D.Impulse);
        knockbackTimer += Time.deltaTime;
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
