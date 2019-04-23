using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoggartAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject rockProjectilePrefab;

    public float timer;
    PatrollingBoggart patrollingBoggart;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1;
        patrollingBoggart = GetComponent<PatrollingBoggart>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5) patrollingBoggart.enabled = true;
        /*if (timer > 1)
        {
            BoggartAttackThrow();
        }*/
    }



    /// <summary>
    /// Enemy attacks player by throwing projectiles at them
    /// </summary>
    public void BoggartAttackThrow()
    {
        patrollingBoggart.enabled = false;
        // enemy turns to face the player -> projectiles should work accordingly
        //if (transform.position.x > player.transform.position.x) transform.Rotate(0f,180f,0f);
        //Debug.Log("Player is in range");
        Shoot();
        timer = 0;
        // playerInRange = false;
    }



    // shoots a projectile towards the last known position of the player
    void Shoot()
    {
        Instantiate(rockProjectilePrefab, firePoint.position, firePoint.rotation);
    }
}
