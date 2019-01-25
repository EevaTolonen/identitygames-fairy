using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelaajaController : MonoBehaviour
{

    public float maxSpeed = 10f;
    private Rigidbody2D pallo;
    private float movement;
    private float jumpForce = 350f;

    public bool grounded = false;
    float groundRadius = 0.2f;
    // tarkistaa, onko pallon alaosaan liitetty gameObject groundCheck maassa vai ei
    public LayerMask whatIsGround;
    // Every object in a Scene has a Transform. It's used to store and manipulate the position, rotation and scale of the object.
    public Transform groundCheck; // t.ex. groundCheck.position, groundCheck.scale etc.


    // Update is called once per frame
    void Update()
    {
        // eli overläppääkö pallon alaosaan liitetty gameObject groundCheck maaksi määritellyn alueen kanssa 
        //                         centre of the circle, radius of circle, filter to check objects only on spesific layers (kenttalayer, ei pelaajaLayer)
        // Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        pallo = GetComponent<Rigidbody2D>();
        movement = Input.GetAxis("Horizontal");
        pallo.velocity = new Vector2(movement * maxSpeed, pallo.velocity.y);
        //pallo.transform.localScale = groundCheck.localScale;

        // odotetaan välilyönnin painallusta ja hypätään
        if(Input.GetKeyDown(KeyCode.W) && grounded) // tee oma Jump-button, jonka pelaaja voi remapata haluamakseen, tee siis oikeaan peliin paremmin!
        {
            grounded = false;
            pallo.AddForce(new Vector2(0, jumpForce));
        }

        // seuraavaksi hyppy == ts. vektori ylöspäin (AddForce() hmmm, toisaalta gravity? Jep, AddForce(0, 700)
    }
}
