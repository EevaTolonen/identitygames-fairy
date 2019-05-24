// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Projectiles that boggart enemies shoot towards player
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
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }


    private void Update()
    {
        //playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // Move the projectile forward towards the player's last known direction;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        rock.velocity = new Vector2(speed, speed);
        timer += Time.deltaTime;
        if (timer > 2) Destroy(gameObject);
    }


    // Rockprojectile is destroyed if it collides with something other than boggarts or other projectiles
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Boggart" || collision.gameObject.tag != "Projectile")
            Destroy(gameObject);
    }
}
