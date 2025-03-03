using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        [Header("Audio")]
        [SerializeField] private AudioClip[] footstepClips;
        [SerializeField] private bool limitSoundPlaytime = false;
        [SerializeField] private float maxSoundPlaytime = .5f;
        [SerializeField] private int[] randomWeights;

        [Header("Parallax scroll")]
        [SerializeField] private bool parallaxActive = false;

        [SerializeField] private GameObject bg1;
        [Range(0, 0.01f)]
        [SerializeField] private float bg1_speedmodifier;

        [SerializeField] private GameObject bg2;
        [Range(0, 0.01f)]
        [SerializeField] private float bg2_speedmodifier;

        [SerializeField] private GameObject bg3;
        [Range(0, 0.01f)]
        [SerializeField] private float bg3_speedmodifier;

        [SerializeField] private GameObject bg4;
        [Range(0, 0.01f)]
        [SerializeField] private float bg4_speedmodifier;

        public Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        public GameObject playerBody;
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private AudioSource m_AudioSource;  // Reference to the player's audio source component.


        public float enidHealth = 5;
        GameObject player;


        private void Awake()
        {
            // Setting up references.
            //m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_AudioSource = GetComponent<AudioSource>();

            player = GameObject.FindGameObjectWithTag("Player");
        }



        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            colliders = ClearColliders(colliders);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != playerBody)
                {
                    m_Grounded = true;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        }

        private Collider2D[] ClearColliders(Collider2D[] colliders)
        {
            List<Collider2D> newList = new List<Collider2D>();

            foreach(Collider2D coll in colliders)
            {
                if(!coll.isTrigger)
                {
                    newList.Add(coll);
                }
            }

            return newList.ToArray();
        }

        public void Move(float move, bool crouch, bool jump, bool attack)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                Vector2 moveAmount = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);


                //Parallax

                if (parallaxActive)
                {
                    bg1.transform.Translate(new Vector3(-moveAmount.x, 0, 0) * bg1_speedmodifier);
                    bg2.transform.Translate(new Vector3(-moveAmount.x, 0, 0) * bg2_speedmodifier);
                    bg3.transform.Translate(new Vector3(-moveAmount.x, 0, 0) * bg3_speedmodifier);
                    bg4.transform.Translate(new Vector3(-moveAmount.x, 0, 0) * bg4_speedmodifier);
                }

                // Move the character
                m_Rigidbody2D.velocity = moveAmount;

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

            if (move != 0 && m_Grounded && !m_AudioSource.isPlaying)
            {
                StartCoroutine(PlaySound());
            }
        }



        private IEnumerator PlaySound()
        {
            m_AudioSource.PlayOneShot(GetRandomClip(footstepClips), 0.5f);
            if (limitSoundPlaytime)
            {
                yield return new WaitForSeconds(maxSoundPlaytime);
                m_AudioSource.Stop();
            }
        }



        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private AudioClip GetRandomClip(AudioClip[] clips)
        {
            if (randomWeights == null)
            {
                return clips[0];
            }

            int randSum = GetSum(randomWeights);
            int rnd = UnityEngine.Random.Range(0, randSum);

            for (int i = 0; i < clips.Length; i++)
            {
                if (rnd <= randSum)
                {
                    return clips[i];
                }
            }

            return clips[0];
        }

        private int GetSum(int[] arr)
        {
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        //Eeva: Changes I've made to this code begin here, maybe we'll remove them to a separate script

        public bool GetPlayerFacingRight()
        {
            return m_FacingRight;
        }

        /*
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag == "Keijupoly")
            {
                Debug.Log("Enid hit poly");
                
                Vector3 hit = other.GetContact(0).normal;

                float angle = Vector3.Angle(hit, Vector3.up);
                // tutkitaan, mist� suunnasta collideriin osutaan, jos ei osuta ylh��lt�, ignoorataan collisioni

                if (Mathf.Approximately(angle, 90))
                {
                    other.gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
                    // sides
                    Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                    if (cross.y > 0)
                    {
                        Debug.Log("Enid hit the poly from Right");
                    }
                    else Debug.Log("Enid hit the poly from Left");
                }
            }
        }
        */
        //Physics2D.IgnoreLayerCollision(0, 8, (m_Character.GetComponent<Rigidbody2D>().velocity.y >= 0.0f));
    }
}
