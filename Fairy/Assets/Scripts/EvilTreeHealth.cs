// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Keeps track of enemy health and death
public class EvilTreeHealth : MonoBehaviour
{
    Animator animator;
    Rigidbody2D enemyBody;

    public int startingHealth = 3;
    public int currentHealth;

    bool damaged;
    public bool isDead;

    float knockbackTimer;
    float flashTimes = 5;

    public Material mattaMaterial;
    public Material flashMaterial;

    Shader matta;
    Shader flash;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();

        enemyBody = GetComponent<Rigidbody2D>();

        flash = Shader.Find("Custom/DamageFlash");
        matta = Shader.Find("Sprites/Diffuse");
    }


    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            StartCoroutine(SwitchToDamageShader());
            damaged = false;
        }
    }


    /// <summary>
    /// Enemy takes damage, this is called from EnidAttack script, and thus is public
    /// </summary>
    /// <param name="amount">how much damage the enemy takes</param>
    /// <param name="hitPoint">The point of death in order to play particle effect etc. when the enemy dies</param>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        damaged = true;

        if (isDead) return;
        currentHealth -= amount;

        Debug.Log("Enemy took damage, health left " + currentHealth);
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    /// <summary>
    /// Enemy dies, and is destroyed after one second of delay, maybe make the enemy sink/turn into particles/etc.?
    /// </summary>
    void Death()
    {
        isDead = true;
        animator.SetTrigger("PahaPuuDeath");
        Debug.Log("Enemy died, health left " + currentHealth);
        //Destroy(gameObject, 2f);
    }


    // Switches between normal and damage shaders in order to create flashing damage effect on enemy
    public IEnumerator SwitchToDamageShader()
    {
        for (int i = 0; i < flashTimes; i++)
        {
            transform.GetComponent<Renderer>().material.shader = matta;

            transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            //player.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.05f);

            transform.GetComponent<Renderer>().material.shader = flash;

            transform.GetComponent<Renderer>().material.SetColor("_FlashColor", Color.red);
            transform.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.1f);
        }
        transform.GetComponent<Renderer>().material.shader = matta;
        transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }


    // Handles how much damage player's attack does to an enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Spear") TakeDamage(1, Vector3.zero);
    }
}
