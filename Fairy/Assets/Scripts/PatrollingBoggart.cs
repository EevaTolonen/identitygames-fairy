/*
 * Author: Eeva Tolonen
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PatrollingBoggart : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 2f;
    public float wallDistance = 0.5f;

    float playerDistance = 20f;
    [SerializeField]
    private bool isAttacking = false;

    private bool movingLeft = true;
    private Vector2 enemyDirection = Vector2.left;

    public Transform DetectGround;
    public Transform DetectPlayer;

    private GameObject player;
    private Rigidbody2D enemy;

    private RaycastHit2D playerInfoFront;
    private RaycastHit2D groundInfo;

    BoggartAttack attack;

    private void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        attack = GetComponent<BoggartAttack>();
    }



    // Enemies move, we check if enemy is on the edge of the platform
    void Update()
    {
        EnemyMoves();

        IsEnemyOnTheEdge();

        //IsEnemyHittingWalls();

        CanEnemySeePlayer();

        IsAttackingEnemyFacingPlayer();

        CanEnemyAttack();

        DidEnemyLostPlayer();
    }



    /// <summary>
    /// Moves the enemy
    /// </summary>
    public void EnemyMoves()
    {
        // we don't want enemy to move when it's shooting player
        if (isAttacking) return;
        enemy.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }



    /// <summary>
    /// Checks if raycast no longer detects collision, then makes the enemy turn the other direction, id. est. patrol to and fro the platform
    /// </summary>
    void IsEnemyOnTheEdge()
    {
        //if (isAttacking) return;
        // detectGround is flipped -180 degrees in x-axis, so vector2.down still makes sense to a boggart which is upside down on the ceiling
        groundInfo = Physics2D.Raycast(DetectGround.position, Vector2.down, distance);

        // handles basic enemy turning
        if (groundInfo.collider == false)
        {
            if (movingLeft)
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
            }
        }
    }



    /// <summary>
    /// Checks if raycast no longer detects collision, then makes the enemy turn the other direction, id. est. patrol to and fro the platform
    /// </summary>
    void IsEnemyHittingWalls()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(DetectGround.position, enemyDirection, distance);

        // handles basic enemy turning
        if (wallInfo.collider == true)
        {
            if (wallInfo.collider.gameObject.name == "Keijupoly")
            {
                Physics2D.IgnoreCollision(wallInfo.collider.gameObject.GetComponent<Collider2D>(), GetComponent<CircleCollider2D>());
                return;
            }

            if (movingLeft)
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
            }
        }
    }




    /// <summary>
    /// Check the distance and only shoot if enemy is both close enough of Enid and facing Enid
    /// </summary>
    void CanEnemySeePlayer()
    {
        // we have already seen the player, and 5s will be spent shooting the player even after they're no longer colliding with the ray
        if (isAttacking) return;

        playerInfoFront = Physics2D.Raycast(DetectPlayer.position, Vector2.right, playerDistance);

        if (playerInfoFront.collider == true)
        {
            if (playerInfoFront.collider.tag == "Player")
            {
                isAttacking = true;
                
            }
        }
        else
        {
            //isAttacking = false;
        }
    }



    /// <summary>
    /// We check if enemy is able to attack on update (isAttacking is true), then we'll call method from BoggartAttack code
    /// </summary>
    void CanEnemyAttack()
    {
        if (isAttacking)
        {
            IsAttackingEnemyFacingPlayer();
            attack.BoggartAttackThrow();
        }
    }



    /// <summary>
    /// If player moves behind enemy, we flip the enemy to face the player, then projectiles also work correctly
    /// </summary>
    void IsAttackingEnemyFacingPlayer()
    {
        if (isAttacking == false) return;
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
    }



    void DidEnemyLostPlayer()
    {
        if (Vector3.Distance(enemy.position, player.transform.position) > playerDistance * 2) isAttacking = false;
    }



    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Keijupoly")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<CircleCollider2D>());
        }
    }

}
