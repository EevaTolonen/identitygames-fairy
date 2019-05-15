using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoggartStandAndShoot : MonoBehaviour
{

    public float aggrpDistance = 10f;
    public float timeBetweenAttacks = 1.5f;
    public GameObject projectile;
    public float offsetX = 0;
    public float offsetY = 0;

    private Animator animator;
    private GameObject player;
    private SpriteRenderer renderer;
    private float distanceToPlayer;
    private bool attackOnCooldown = false;
    private float attackCooldownTimeLeft = 0f;
    private bool isFacingLeft = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < aggrpDistance)
        {
            if(!attackOnCooldown)
            {
                animator.SetTrigger("Attack");
                //Invoke("Shoot", 0.1f);
                Shoot();
                attackOnCooldown = true;
            }
        }

        isFacingLeft = player.transform.position.x < transform.position.x ? true : false;
        renderer.flipX = isFacingLeft;

        if(attackOnCooldown)
        {
            attackCooldownTimeLeft -= Time.deltaTime;

            if(attackCooldownTimeLeft <= 0)
            {
                attackOnCooldown = false;
            }
        }
    }

    private void Shoot()
    {
        attackCooldownTimeLeft += timeBetweenAttacks;

        Vector2 spawnPosition;
        if(isFacingLeft)
        {
            spawnPosition = new Vector2(transform.position.x - offsetX, transform.position.y - offsetY);
        } else
        {
            spawnPosition = new Vector2(transform.position.x + offsetX, transform.position.y - offsetY);
        }

        Instantiate(projectile, spawnPosition, Quaternion.identity);
    }
}
