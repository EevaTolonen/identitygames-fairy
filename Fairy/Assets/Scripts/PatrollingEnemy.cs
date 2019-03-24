/*
 * Author: Eeva Tolonen
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PatrollingEnemy : MonoBehaviour
{
    /*TO DO:
     1. Siirrä Enidin healthista ja hyökkäämisestä vastuussa oleva koodi omiin osioihinsa ja viittaa
     niihin aina tarvittaessa,
     2. sama vihulle
     3. Nyt meillä yleinen enemypatrol-koodi, jonka voi käyttää pohjana projectile-vihulle
     4. toteuta projectile-vihulle oma hyökkäyskoodi
     */

    public float speed = 10f;
    public float distance = 5f;

    private bool movingRight = true;

    public Transform DetectGround;
    private Animator animator;

    private int patrolHash = Animator.StringToHash("Patrol");
    private int followHash = Animator.StringToHash("Follow");
    private int attackHash = Animator.StringToHash("Attack");

    //public PlatformerCharacter2D player;
    private GameObject player;
    private Rigidbody2D enemy;

    //PlatformerCharacter2D platformerCharacter2D;
    //Platformer2DUserControl platformer2DUserControl;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //platformerCharacter2D = player.GetComponent<PlatformerCharacter2D>();
        //platformer2DUserControl = player.GetComponent<Platformer2DUserControl>();
    }
    // Enemies move, we check if enemy is on the edge of the platform
    void Update()
    {
        EnemyMoves();

        IsEnemyOnTheEdge();

        CanEnemySeePlayer();
    }



    /// <summary>
    /// Moves the enemy
    /// </summary>
    public void EnemyMoves()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }



    /// <summary>
    /// Checks if raycast no longer detects collision, then makes the enemy turn the other direction, id. est. patrol to and fro the platform
    /// </summary>
    void IsEnemyOnTheEdge()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(DetectGround.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }


    /// <summary>
    /// Check the distance and only follow Enid if enemy is both close enough of Enid and facing Enid
    /// </summary>
    void CanEnemySeePlayer()
    {
        float enemyPlayerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (enemyPlayerDistance < 20f)
        {   // we check if enemy is behind Enid and facing left OR if enemy is in front of Enid and facing right, then enemy keeps patrolling since it can't see Enid
            if (((transform.position.x < player.transform.position.x) && !movingRight) || ((transform.position.x > player.transform.position.x) && movingRight))
            {
                animator.SetBool("isPatrolling", true);
            }
            else
            {
                animator.SetBool("isPatrolling", false);
                speed = 20f;
                ApproachEnid();
            }
        }
        else
        {
            animator.SetBool("isPatrolling", true);
            speed = 10f;
        }
    }



    private void ApproachEnid()
    {
        float step = 2f;
        Vector3.MoveTowards(transform.position, player.transform.position, step);
        /*if (Vector3.Distance(transform.position, playerObject.transform.position) <= 8f)
        {
            animator.SetTrigger(attackHash);
            Attack();
        }*/
    }



    /*void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("vihu hyökkää!!!");
        // tÄHÄN KOHTAAN HAETAAN ENIDIN KOODISTA HIPARIEN VÄHENNYS
        if (other.tag == "Player")
            other.GetComponent<PlatformerCharacter2D>().PlayerLosesHP();
        if (platformerCharacter2D.enidHealth <= 0)
        {
            platformer2DUserControl.enabled = false;

        }
    }*/
}
