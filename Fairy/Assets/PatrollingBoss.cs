using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBoss : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 2f;
    public float wallDistance = 0.5f;

    float playerDistance = 60f;
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

    private void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }



    // Enemies move, we check if enemy is on the edge of the platform
    void Update()
    {
        CanEnemySeePlayer();

        CanEnemyAttack();

        DidEnemyLostPlayer();
    }



    /// <summary>
    /// Check the distance and only shoot if enemy is both close enough of Enid and facing Enid
    /// </summary>
    void CanEnemySeePlayer()
    {
        // we have already seen the player, and 5s will be spent shooting the player even after they're no longer colliding with the ray
        if (isAttacking) return;

        playerInfoFront = Physics2D.Raycast(DetectPlayer.position, Vector2.left, playerDistance);

        if (playerInfoFront.collider == true)
        {
            if (playerInfoFront.collider.tag == "Player")
            {
                Debug.Log("Here player collides with the enemy");
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
            //attack.BoggartAttackThrow();
        }
    }



    void DidEnemyLostPlayer()
    {
        //if (Vector3.Distance(enemy.position, player.transform.position) > playerDistance) isAttacking = false;
    }
}
