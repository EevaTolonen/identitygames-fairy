using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnidHealth : MonoBehaviour
{
    EnidHealth enidHealth;
    PlatformerCharacter2D platformerCharacter2D;
    Platformer2DUserControl platformer2DUserControl;

    EnemyHealth enemyHealth;
    Animator animator;

    public int startingHealth = 30;
    public int currentHealth;

    bool isDead;
    bool damaged;

    // Start is called before the first frame update
    void Awake()
    {
        enidHealth = GetComponent<EnidHealth>();
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
        platformer2DUserControl = GetComponent<Platformer2DUserControl>();
        enemyHealth = GetComponent<EnemyHealth>();

        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            // HERE WE PUT CHANGES TO ENID'S SPRITE WHEN TAKING DAMAGE, IF WE WANT
        }
        // TO DO: pitää kirjaa Enidin helteistä, hoitaa vähentämisen ja Enidin kuolemisen
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enid took damage, health left " + currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            //isDead = true;
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("IsDead");
        platformer2DUserControl.enabled = false;
        Debug.Log("Enid died, health left " + currentHealth);
    }
}
