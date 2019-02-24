/*
 * Author: Eeva Tolonen
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    /*public float speed;
    private float distance = 2f;

    private bool movingRight = true;

    public Transform DetectGround;

    // Enemies move, we check if enemy is on the edge of the platform
    void Update()
    {
        EnemyMoves();

        IsEnemyOnTheEdge();
    }



    /// <summary>
    /// Moves the enemy
    /// </summary>
    void EnemyMoves()
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
    }*/
}
