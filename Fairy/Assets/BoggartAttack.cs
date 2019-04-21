using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoggartAttack : MonoBehaviour
{
    public bool playerInRange;
    public Transform firePoint;
    public GameObject rockProjectilePrefab;
    private GameObject player;

    public float timer;
    PatrollingBoggart patrollingBoggart;

    private BoxCollider2D leftCol;
    private BoxCollider2D rightCol;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 1;
        patrollingBoggart = this.GetComponent<PatrollingBoggart>();

        // it's okay to use Find if we don't use it every frame, but to restore a variable etc.
        leftCol = GameObject.Find("ColliderLeft").gameObject.GetComponent<BoxCollider2D>();
        rightCol = GameObject.Find("ColliderRight").gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1) patrollingBoggart.enabled = true;
        if ((playerInRange) && timer > 1)
        {
            BoggartAttackThrow();
        }
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
