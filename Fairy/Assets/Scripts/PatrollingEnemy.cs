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
    public float speed = 10f;
    public float distance = 2f;
    public float wallDistance = 0.5f;

    public float playerDistance = 10f;
    public bool isFollowing = false;

    private bool movingRight = true;
    private Vector2 enemyDirection = Vector2.right;

    public Transform DetectGround;
    public Transform DetectPlayer;
    private Animator animator;

    //public PlatformerCharacter2D player;
    private GameObject player;
    private Rigidbody2D enemy;

    private RaycastHit2D playerInfoFront;
    private RaycastHit2D groundInfo;

    float enemySearchTimer;
    public CircleCollider2D enemyCollider;

    private GameObject keijupoly;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<Collider2D>());
    }



    // Enemies move, we check if enemy is on the edge of the platform
    void Update()
    {
        //if (enemySearchTimer > 0) enemySearchTimer += Time.deltaTime;
        //if (Vector3.Distance(enemy.position, player.transform.position) > playerDistance) isAttacking = false;

        EnemyMoves();

        IsEnemyOnTheEdge();

        IsEnemyHittingWalls();

        CanEnemySeePlayer();

        FollowPlayer();

    }



    /// <summary>
    /// Moves the enemy
    /// </summary>
    public void EnemyMoves()
    {
        // we don't want enemy to patrol when it's following player
        if (isFollowing) return;
        enemy.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }



    /// <summary>
    /// Checks if raycast no longer detects collision, then makes the enemy turn the other direction, id. est. patrol to and fro the platform
    /// </summary>
    void IsEnemyOnTheEdge()
    {
        groundInfo = Physics2D.Raycast(DetectGround.position, Vector2.down, distance);
        // if enemy is following player but doesn't detect ground, it stops and after 2 seconds, starts patrolling again
        if (groundInfo.collider == false && isFollowing)
        {
            enemySearchTimer += Time.deltaTime;
            animator.SetFloat("EnemySearchTimer", enemySearchTimer);
            if (enemySearchTimer >= 2)
            {
                // remember to update BOTH timer in the code and timer in the animator
                enemySearchTimer = 0;
                animator.SetFloat("EnemySearchTimer", enemySearchTimer);
                isFollowing = false;
                animator.SetBool("isPatrolling", true);
                /*if (enemySearchTimer >= 5)
                {
                    isAttacking = false;
                    enemySearchTimer = 0;
                    enemy.constraints = RigidbodyConstraints2D.None;
                    enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    //Time.timeScale = 1;
                    //return;*/
            }
            else
            {
                // here enemy stays in place and we return since we don't want to execute the next if statement
                enemy.velocity = Vector2.zero;
                enemy.constraints = RigidbodyConstraints2D.FreezeAll;
                //Time.timeScale = 0;
                return;
            }
        }

        // handles basic enemy turning
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                enemyDirection = Vector2.left;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                enemyDirection = Vector2.right;
            }
        }
    }



    /// <summary>
    /// Checks if raycast no longer detects collision, then makes the enemy turn the other direction, id. est. patrol to and fro the platform
    /// </summary>
    void IsEnemyHittingWalls()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(DetectGround.position, enemyDirection, distance);
        // if enemy is following player but doesn't detect ground, it stops and after 2 seconds, starts patrolling again
        if (wallInfo.collider == true && isFollowing)
        {
            enemySearchTimer += Time.deltaTime;
            animator.SetFloat("EnemySearchTimer", enemySearchTimer);
            if (enemySearchTimer >= 2)
            {
                // remember to update BOTH timer in the code and timer in the animator
                enemySearchTimer = 0;
                animator.SetFloat("EnemySearchTimer", enemySearchTimer);
                isFollowing = false;
                animator.SetBool("isPatrolling", true);
                /*if (enemySearchTimer >= 5)
                {
                    isAttacking = false;
                    enemySearchTimer = 0;
                    enemy.constraints = RigidbodyConstraints2D.None;
                    enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    //Time.timeScale = 1;
                    //return;*/
            }
            else
            {
                // here enemy stays in place and we return since we don't want to execute the next if statement
                enemy.velocity = Vector2.zero;
                enemy.constraints = RigidbodyConstraints2D.FreezeAll;
                //Time.timeScale = 0;
                return;
            }
        }

        // handles basic enemy turning
        if (wallInfo.collider == true)
        {
            if (wallInfo.collider.gameObject.name == "Keijupoly")
            {
                Physics2D.IgnoreCollision(wallInfo.collider.gameObject.GetComponent<Collider2D>(), enemyCollider);
                return;
            }

            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                enemyDirection = Vector2.left;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                enemyDirection = Vector2.right;
            }
        }
    }




    /// <summary>
    /// Check the distance and only follow Enid if enemy is both close enough of Enid and facing Enid
    /// </summary>
    void CanEnemySeePlayer()
    {
        /*float enemyPlayerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (enemyPlayerDistance < 20f)
        {   // we check if enemy is behind Enid and facing left OR if enemy is in front of Enid and facing right, then enemy keeps patrolling since it can't see Enid
            if (((transform.position.x < player.transform.position.x) && !movingLeft) || ((transform.position.x > player.transform.position.x) && movingLeft))
            {
                animator.SetBool("isPatrolling", true);
            }
            else
            {
                animator.SetBool("isPatrolling", false);
                speed = 15f;

                ApproachEnid();
            }
        }
        else
        {
            animator.SetBool("isPatrolling", true);
            speed = 10f;
        }*/



        // if enemy is already following player, we don't need to check if it can see player
        if (isFollowing) return;

        playerInfoFront = Physics2D.Raycast(DetectPlayer.position, enemyDirection, playerDistance);
        if (playerInfoFront.collider == false)
        {
            animator.SetBool("isPatrolling", true);
        }
        if (playerInfoFront.collider == true)
        {
            if (playerInfoFront.collider.tag == "Player")
            {
                animator.SetBool("isPatrolling", false);
                isFollowing = true;
                ApproachEnid();
            }
        }
    }



    public void ApproachEnid()
    {
        // ts. kun pelaaja on ekaa kertaa huomattu, mennään pelaajaa kohti kunnes pelaaja on vihollisesta tietyn etäisyyden päässä
        if (Vector2.Distance(transform.position, player.transform.position) <= 5) return;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);


        // we check if enemy is behind Enid and facing left OR if enemy is in front of Enid and facing right, then we flip the enemy so it's always looking at right direction
        if (((transform.position.x < player.transform.position.x) /*&& !movingLeft) || ((transform.position.x > player.transform.position.x) && movingLeft*/))
        {
            // tsekataan riittääkö laittaa vain kääntymään, jos ei, koitetaan päivittää samalla muutkin arvot
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        /*if (movingLeft) turn left
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingLeft = false;
            enemyDirection = Vector2.left;
        }
        else turn right
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingLeft = true;
            enemyDirection = Vector2.right;
        }*/

        // no point with these, we have to check enemy turning otherwise
        /*if (movingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingLeft = false;
            enemyDirection = Vector2.left;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingLeft = true;
            enemyDirection = Vector2.right;
        }*/
    }



    void FollowPlayer()
    {
        // joko tämä rivi toimii tai sitten tämä if (groundInfo.collider == false && isAttacking)
        // ts. enemy doesn't follow player if isAttacking == false or enemy is following but is on edge
        if (!isFollowing || (groundInfo.collider == false && isFollowing)) return;
        ApproachEnid();
    }



    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Keijupoly")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), enemyCollider);
        }
    }
}
