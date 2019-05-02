using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Attack;

        private Transform groundCheck;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            groundCheck = transform.Find("GroundCheck");
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            if (!m_Attack)
            {
                // Read the attack input in Update so button presses aren't missed.
                m_Attack = CrossPlatformInputManager.GetButtonDown("Attack");
            }
             
            Physics2D.IgnoreLayerCollision(0, 8, (m_Character.GetComponent<Rigidbody2D>().velocity.y >= 0.0f));
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            crouch = false;

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, m_Attack);
            m_Jump = false;
            m_Attack = false;
        }
    }
}
