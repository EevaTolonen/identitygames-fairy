// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Tear that boss shoots from it's tree trunk
public class Tear : MonoBehaviour
{
    public Rigidbody2D tear;
    float timer;
    float timeToLive = 3f;

    // Start is called before the first frame update
    void Start()
    {
        tear = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = tear.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    // Shoots tear in the air
    public void Shoot(Vector2 direction)
    {
        tear.AddForce(direction, ForceMode2D.Impulse);
    }


    // Tear ignores other projectile collisions altogether and is otherwise destroyed
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile") Physics2D.IgnoreLayerCollision(9, 9);
        if (other.gameObject.tag != "Projectile") Destroy(gameObject);
    }
}
