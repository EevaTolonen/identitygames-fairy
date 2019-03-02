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
    private float movementX;
    private float movementY;
    public float jumpHeight = 10f;

    private bool facingRight = true;
    private bool flipSprite;
    private Animator pelaajaAnimaatio;

    private SpriteRenderer spriteRenderer;

    public bool grounded = false;
    public float groundRadius = 0.2f;
    // tarkistaa, onko pallon alaosaan liitetty gameObject groundCheck maassa vai ei
    public LayerMask whatIsGround;
    // Every object in a Scene has a Transform. It's used to store and manipulate the position, rotation and scale of the object.
    public Transform groundCheck; // t.ex. groundCheck.position, groundCheck.scale etc.
    


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pelaajaAnimaatio = GetComponent<Animator>();
        pelaaja = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        OllaankoMaassa();

        HaePelaajaAxis();

        MitenSaaLiikkua();

        SaakoHypata();

        KameraVasenOikea();

        KaannetaankoPelaaja();

        pelaajaAnimaatio.SetBool("Grounded", grounded);
        pelaajaAnimaatio.SetFloat("Speed", Mathf.Abs(movementX));
    }


    // This function is called by Unity before every “physic update”. Indeed, physic updates and classic updates are not synced. 
    // To achieve a convincing physic simulation, we need to calculate it smoothly. Unity decided to pull apart the physic update 
    //from the classic update. This way if the frame rate is too low or too fast, it won’t impact the simulation.
    private void FixedUpdate()
    {
        //pelaaja.MovePosition(pelaaja.position + new Vector2 (movementX, 0) * maxSpeed * Time.fixedDeltaTime);
    }


    private void KaannetaankoPelaaja()
    {
        flipSprite = (spriteRenderer.flipX ? (movementX > 0f) : (movementX < 0f));
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
        if (movementX * maxSpeed < 0 && facingRight)
        {
            CameraLeft2Right.enabled = false;
            CameraRight2Left.enabled = true;
            //KaannetaankoPelaaja();
            facingRight = !facingRight;

        }
        // if (pelaaja.velocity.x > 0 && CameraLeft2Right.enabled == true
        if (movementX * maxSpeed > 0 && !facingRight)
        {
            CameraRight2Left.enabled = false;
            CameraLeft2Right.enabled = true;
            //KaannetaankoPelaaja();
            facingRight = !facingRight;
        }
    }



    private void HaePelaajaAxis()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");
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
        if (grounded) pelaaja.velocity = new Vector2(movementX * maxSpeed, pelaaja.velocity.y);
        // pelaajan nopeutta hidastetaan, kun ilmassa, ettei tule naurettavia hyppyjä
        /*if (!grounded && pelaaja.velocity.y <= 0)
        {   // pelaaja putoaa alas hieman nopeammin kuin hyppäsi, jotta ei tule outoa "leijumisefektiä" alas pudotessa
            pelaaja.AddForce(new Vector2(0, putoamisNopeus) * Time.deltaTime);
        }
        else pelaaja.velocity = new Vector2(movementX * maxSpeed / nopeusIlmassaJaettuna, pelaaja.velocity.y);*/
    }



    void SaakoHypata()
    {
        // odotetaan välilyönnin painallusta ja hypätään
        // .AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        if (((Input.GetKeyDown(KeyCode.W)|| (Input.GetKeyDown(KeyCode.Space))) && grounded == true)) // tee oma Jump-button, jonka pelaaja voi remapata haluamakseen, tee siis oikeaan peliin paremmin!
        {
            grounded = false;
            Vector2 hyppy = new Vector2(0, jumpHeight);
            pelaaja.AddForce(hyppy);
        }
    }
}
