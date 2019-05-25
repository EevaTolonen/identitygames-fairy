// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Projectiles that boggart enemies shoot towards player
public class RockProjectile : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rock;

    GameObject player;
    Vector2 moveDirection;

    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rock = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rock.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3) Destroy(gameObject);
    }


    // Rockprojectile is destroyed if it collides with something other than boggarts or other projectiles
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Boggart" || collision.gameObject.tag != "Projectile")
            Destroy(gameObject);
    }
}
