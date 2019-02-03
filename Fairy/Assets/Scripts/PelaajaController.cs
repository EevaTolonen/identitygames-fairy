using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelaajaController : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera CameraLeft2Right;
    public Cinemachine.CinemachineVirtualCamera CameraRight2Left;
    public float putoamisNopeus;
    public float nopeusIlmassaJaettuna = 1.5F;
    public float maxSpeed = 10f;
    private Rigidbody2D pelaaja;
    private float movement;
    public float jumpForce = 350f;

    private bool facingRight = true;
    private bool flipSprite;
    private Animator pelaajaAnimaatio;

    private SpriteRenderer spriteRenderer;

    public bool grounded = false;
    float groundRadius = 0.2f;
    // tarkistaa, onko pallon alaosaan liitetty gameObject groundCheck maassa vai ei
    public LayerMask whatIsGround;
    // Every object in a Scene has a Transform. It's used to store and manipulate the position, rotation and scale of the object.
    public Transform groundCheck; // t.ex. groundCheck.position, groundCheck.scale etc.


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pelaajaAnimaatio = GetComponent<Animator>();
    }



    private void FixedUpdate()
    {
        OllaankoMaassa();

        HaePelaajaJaAxis();

        MitenSaaLiikkua();

        SaakoHypata();

        KameraVasenOikea();

        KaannetaankoPelaaja();

        pelaajaAnimaatio.SetBool("Grounded", grounded);
        pelaajaAnimaatio.SetFloat("Speed", Mathf.Abs(movement));
    }


    private void KaannetaankoPelaaja()
    {
        flipSprite = (spriteRenderer.flipX ? (movement > 0f) : (movement < 0f));
        if(flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        /*facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;**/

    }



    // ts. mahd. skenaariot: pelaaja menee oikealle/paikallaan ja kamera ko. hetkellä l2r -> pysyy samana
    // pelaaja menee oikealle/paikallaan ja kamera r2l -> vaihtuu
    // pelaaja menee vasemmalle/paikallaan ja kamera ko. hetkellä r2l -> pysyy samana
    // pelaaja menee vasemmalle/paikallaan ja kamera l2r -> vaihtuu

    private void KameraVasenOikea()
    {
        if (movement * maxSpeed < 0 && facingRight)
        {
            CameraLeft2Right.enabled = false;
            CameraRight2Left.enabled = true;
            //KaannetaankoPelaaja();
            facingRight = !facingRight;

        }
        // if (pelaaja.velocity.x > 0 && CameraLeft2Right.enabled == true
        if (movement * maxSpeed > 0 && !facingRight)
        {
            CameraRight2Left.enabled = false;
            CameraLeft2Right.enabled = true;
            //KaannetaankoPelaaja();
            facingRight = !facingRight;
        }
    }



    private void HaePelaajaJaAxis()
    {
        pelaaja = GetComponent<Rigidbody2D>();
        movement = Input.GetAxis("Horizontal");
    }



    // eli overläppääkö pallon alaosaan liitetty gameObject groundCheck maaksi määritellyn alueen kanssa 
    // parametrit: centre of the circle, radius of circle, filter to check objects only on spesific layers (kenttalayer, ei pelaajaLayer)
    // Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    private void OllaankoMaassa()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }



    void MitenSaaLiikkua()
    {
        if (grounded) pelaaja.velocity = new Vector2(movement * maxSpeed, pelaaja.velocity.y);
        // pelaajan nopeutta hidastetaan, kun ilmassa, ettei tule naurettavia hyppyjä
        if (!grounded && pelaaja.velocity.y <= 0)
        {   // pelaaja putoaa alas hieman nopeammin kuin hyppäsi, jotta ei tule outoa "leijumisefektiä" alas pudotessa
            pelaaja.AddForce(new Vector2(0, putoamisNopeus));
        }
        else pelaaja.velocity = new Vector2(movement * maxSpeed / nopeusIlmassaJaettuna, pelaaja.velocity.y);
    }



    void SaakoHypata()
    {
        // odotetaan välilyönnin painallusta ja hypätään
        if (Input.GetKeyDown(KeyCode.W) && grounded) // tee oma Jump-button, jonka pelaaja voi remapata haluamakseen, tee siis oikeaan peliin paremmin!
        {
            grounded = false;
            pelaaja.AddForce(new Vector2(0, jumpForce));
        }
    }
}
