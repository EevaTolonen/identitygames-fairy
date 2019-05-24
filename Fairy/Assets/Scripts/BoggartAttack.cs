// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles enemy (Boggart) attacking
public class BoggartAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject rockProjectilePrefab;

    public float timer;
    PatrollingBoggart patrollingBoggart;

    private Rigidbody2D enemy;
    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 1;
        patrollingBoggart = GetComponent<PatrollingBoggart>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!patrollingBoggart.enabled)
        {
            IsAttackingEnemyFacingPlayer();
            timer += Time.deltaTime;
            if (timer > 2)
            {
                patrollingBoggart.enabled = true;
            }
        }
    }


    /// <summary>
    /// Enemy attacks player by throwing projectiles at them
    /// </summary>
    public void BoggartAttackThrow()
    {
        patrollingBoggart.enabled = false;
        enemy.velocity = Vector3.zero;
        enemy.angularVelocity = 0;
        Shoot();
        timer = 0;
    }


    // shoots a projectile towards the last known position of the player
    void Shoot()
    {
        Instantiate(rockProjectilePrefab, firePoint.position, firePoint.rotation);
    }


    /// <summary>
    /// If player moves behind enemy, we flip the enemy to face the player, then projectiles also work correctly
    /// </summary>
    void IsAttackingEnemyFacingPlayer()
    {
        // we check if enemy is behind Enid and facing left OR if enemy is in front of Enid and facing right, then we flip the enemy so it's always looking at right direction
        if (((enemy.transform.position.x < player.transform.position.x)))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }
}
