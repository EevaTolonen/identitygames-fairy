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
    private Vector2 enemyDirection = Vector2.left;
    
    public Transform DetectPlayer;

    private GameObject player;

    private RaycastHit2D playerInfoFront;
    private Transform tearSpawn;
    
    float timer;
    float timeBetweenWaves = 6f;

    private void Start()
    {
        tearSpawn = transform.Find("TearSpawn");
        player = GameObject.FindGameObjectWithTag("Player");
    }



    // Enemies move, we check if enemy is on the edge of the platform
    void Update()
    {
        timer += Time.deltaTime;
        CanEnemySeePlayer();
        ShootTears();

        if (timer > timeBetweenWaves) timer = 0f;
    }



    /// <summary>
    /// Check the distance and only shoot if enemy is both close enough of Enid and facing Enid
    /// </summary>
    void CanEnemySeePlayer()
    {/*
        playerInfoFront = Physics2D.Raycast(DetectPlayer.position, Vector2.left, playerDistance);

        if (playerInfoFront.collider == true)
        {
            if (playerInfoFront.collider.tag == "Player")
            {
                Debug.Log("Here player collides with the enemy");
            }
        }*/
    }


    void ShootTears()
    {
        if (timer < timeBetweenWaves) return;
        GetComponent<WeepingWillowTears>().SpawnTears();
    }
}
