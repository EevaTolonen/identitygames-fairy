using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rock;

    GameObject player;
    Vector3 playerPos;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player");

        // Add + 1 to player's last known position so bullet appears to float above ground.
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        // Aim bullet in player's direction.
        //transform.rotation = Quaternion.LookRotation(playerPos);
    }

    private void Update()
    {
        //playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // Move the projectile forward towards the player's last known direction;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > 3) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Boggart" || collision.gameObject.tag != "Projectile")
            Destroy(gameObject);
    }
}
