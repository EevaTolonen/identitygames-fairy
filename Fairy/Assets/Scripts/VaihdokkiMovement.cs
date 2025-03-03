﻿// @Olli Paakkunainen & Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;
using UnityEngine;

// Little fairy Changeling follows player around and turns to follow player
[RequireComponent(typeof(SpriteRenderer))]
public class VaihdokkiMovement : MonoBehaviour
{
    public GameObject followTarget;
    public Vector3 offset = Vector3.zero;
    public float baseSpeed = 10f;
    public float maxDistance = 2;
    public float maxDistanceSpeedModifier = 2;

    private bool facingRight = true;
    private SpriteRenderer vaihdokkiRenderer;


    private void Awake()
    {
        if (followTarget == null)
            followTarget = GameObject.FindGameObjectWithTag("Player");

        vaihdokkiRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        // we see whether player is facing right or not, and position changeling accordingly behind the player
        facingRight = followTarget.GetComponent<PlatformerCharacter2D>().GetPlayerFacingRight();
        if (!facingRight && offset.x < 0)
        {
            offset.x *= -1;
            vaihdokkiRenderer.flipX = true;
            
        }
        if (facingRight && offset.x > 0)
        {
            offset.x *= -1;
            vaihdokkiRenderer.flipX = false;
        }

        float moveSpeed = baseSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, followTarget.transform.position + offset);
        
        if (distanceToTarget > maxDistance)
        {
            moveSpeed = baseSpeed * maxDistanceSpeedModifier;
        }


        // Move our position a step closer to the target.
        float step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, followTarget.transform.position + offset, step);
    }
}
