using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    /// <summary>
    /// TO DO: IGNOORAA TÖRMÄYKSET MUISTA PROJECTILEOBJEKTEISTA niin ei kyyneleet törmää toisiinsa :D
    /// </summary>


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
    }



    public void Shoot(Vector2 direction)
    {
        tear.AddForce(direction, ForceMode2D.Impulse);
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile") Physics2D.IgnoreLayerCollision(9, 9);
        if (other.gameObject.tag != "Projectile") Destroy(gameObject);
    }
}
