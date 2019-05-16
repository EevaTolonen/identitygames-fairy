using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyHealth : MonoBehaviour
{
    PatrollingEnemy patrollingEnemy;

    Animator animator;
    //GameObject enemy;
    Rigidbody2D enemyBody;

    public int startingHealth = 3;
    public int currentHealth;

    bool damaged;
    bool isDead;

    float knockbackTimer;
    float flashTimes = 5;

    public Material mattaMaterial;
    public Material flashMaterial;

    Shader matta;
    Shader flash;

    // Start is called before the first frame update
    void Awake()
    {
        patrollingEnemy = GetComponent<PatrollingEnemy>();

        currentHealth = startingHealth;
        animator = GetComponent<Animator>();

        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyBody = GetComponent<Rigidbody2D>();

        flash = Shader.Find("Custom/DamageFlash");
        matta = Shader.Find("Sprites/Diffuse");
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackTimer > 0) knockbackTimer += Time.deltaTime;

        if (knockbackTimer > 3)
        {
            // here we allow the enemy to move after being stunned for 3 seconds
            //isStunned = false;
            //animator.SetBool("IsStunned", isStunned);
            patrollingEnemy.enabled = true;


            //enemyBody.freezeRotation = false;
            //enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            knockbackTimer = 0;

            // line checks if player backstabbed the enemy, then enemy switches from patrolling to approaching player
            if (patrollingEnemy.GetComponent<Animator>().GetBool("isPatrolling") == true)
            {
                patrollingEnemy.ApproachEnid();
            }

        }
        if (damaged)
        {
            StartCoroutine(SwitchToDamageShader());
            damaged = false;
        }
    }



    /// <summary>
    /// Enemy takes damage, this is called from EnidAttack script, and thus is public
    /// </summary>
    /// <param name="amount">how much damage the enemy takes</param>
    /// <param name="hitPoint">The point of death in order to play particle effect etc. when the enemy dies</param>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        damaged = true;
        //isStunned = true;
        //animator.SetBool("IsStunned", isStunned);

        if (isDead) return;
        currentHealth -= amount;

        Debug.Log("Enemy took damage, health left " + currentHealth);
        if (currentHealth <= 0)
        {
            Death();
        }

        // disables the enemyPatrol script and freezes rotation in order to give the enemy a "stun time"
        patrollingEnemy.enabled = false;

        //enemyBody.freezeRotation = true;
        enemyBody.velocity = Vector3.zero;
        enemyBody.angularVelocity = 0;
        // enemy is knocked back from the player damage. and can't move for 3 seconds (time set in the knockbacktimer)
        enemyBody.AddForce(new Vector2(800, 800), ForceMode2D.Force);
        knockbackTimer += Time.deltaTime;
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



    public IEnumerator SwitchToDamageShader()
    {
        for (int i = 0; i < flashTimes; i++)
        {
            enemyBody.GetComponent<Renderer>().material.shader = matta;

            enemyBody.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            //player.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.05f);

            enemyBody.GetComponent<Renderer>().material.shader = flash;

            enemyBody.GetComponent<Renderer>().material.SetColor("_FlashColor", Color.red);
            enemyBody.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.1f);
        }
        enemyBody.GetComponent<Renderer>().material.shader = matta;
        enemyBody.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
}
