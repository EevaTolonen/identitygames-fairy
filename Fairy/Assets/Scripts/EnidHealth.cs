// @author Eeva Tolonen & Olli Paakkunainen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;
using System;

// Handles player losing health and dying
public class EnidHealth : MonoBehaviour
{
    Platformer2DUserControl platformer2DUserControl;
    Animator animator;

    public int startingHealth = 30;
    public int currentHealth;

    bool damaged;
    public PostProcess postProcess;
    public AudioClip deathSound;

    Shader matta;
    Shader flash;

    [Header("Sounds:")]
    public AudioClip[] hurt;

    AudioSource audioSource;

    Rigidbody2D player;
    int knockbackForce = 40;

    // Platformer2DUserControl won't work after taking damage for x time 
    public float knockbackTimer;
    float flashTimes = 5;

    bool isDead = false;


    // Start is called before the first frame update
    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        audioSource = playerObj.GetComponent<AudioSource>();
        platformer2DUserControl = playerObj.GetComponent<Platformer2DUserControl>();

        animator = playerObj.GetComponent<Animator>();
        currentHealth = startingHealth;
        player = playerObj.GetComponent<Rigidbody2D>();

        flash = Shader.Find("Custom/DamageFlash");
        matta = Shader.Find("Sprites/Diffuse");
    }


    // Update is called once per frame
    void Update()
    {

        if (knockbackTimer > 0.5)
        {
            platformer2DUserControl.enabled = true;
            knockbackTimer = 0;
        }
        if (knockbackTimer != 0) knockbackTimer += Time.deltaTime;

        // changes to player sprite are made here, sprite flashes when taking damage
        if (damaged)
        {
            StartCoroutine(SwitchToDamageShader());
            damaged = false;
        }
    }


    // Handles player knockback direction after taking damage, applying damage and death
    public void TakeDamage(int amount, Vector3 enemyPos)
    {
        damaged = true;

        if (knockbackTimer > 0)
        {
            knockbackTimer = 0;
        }

        currentHealth -= amount;

        platformer2DUserControl.enabled = false;
        // we check if enemy is on the left or right side of player, to determine knockback direction

        if (enemyPos.x >= player.transform.position.x)
        {
            player.AddForce(new Vector2(-knockbackForce, knockbackForce), ForceMode2D.Impulse);
        }
        else player.AddForce(new Vector2(knockbackForce, knockbackForce), ForceMode2D.Impulse);

        knockbackTimer += Time.deltaTime;

        Debug.Log("Enid took damage, health left " + currentHealth);
        if (currentHealth <= 0/* && !isDead*/)
        {
            //isDead = true;
            Death();
        }
    }

    
    //If player takes damage from object, they die straight away
    private void TakeDamageFromObject()
    {
        damaged = true;
        currentHealth--;

        if (currentHealth <= 0/* && !isDead*/)
        {
            //isDead = true;
            Death();
        }
    }


    // Handles screen turning to black and reloading the scene
    void Death()
    {
        //onDeath.Invoke();
        animator.SetTrigger("IsDead");
        //platformer2DUserControl.enabled = false;
        Debug.Log("Gameover");

        if (!isDead)
        {
            StartCoroutine(FadeToBlackAndLoad());
        }

        isDead = true;

        //platformer2DUserControl.enabled = true;

        //Debug.Log("Enid died, health left " + currentHealth);
    }


    // Handles screen turning to black and reloading the scene
    private IEnumerator FadeToBlackAndLoad()
    {
        audioSource.PlayOneShot(deathSound, 6f);

        float _VRadius = postProcess.material.GetFloat("_VRadius");

        while (_VRadius > 0)
        {
            _VRadius -= Time.deltaTime * .8f;
            postProcess.material.SetFloat("_VRadius", _VRadius);

            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // Switches between normal and damage shaders in order to create flashing damage effect on player
    public IEnumerator SwitchToDamageShader()
    {
        audioSource.PlayOneShot(GetRandomClip(hurt));

        for (int i = 0; i < flashTimes; i++)
        {
            player.GetComponent<Renderer>().material.shader = matta;

            player.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            //player.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.05f);

            player.GetComponent<Renderer>().material.shader = flash;

            player.GetComponent<Renderer>().material.SetColor("_FlashColor", Color.red);
            player.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.1f);
        }
        player.GetComponent<Renderer>().material.shader = matta;
        player.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }


    /// <summary>
    /// If player hits the spikes, they return to the previous checkpoint
    /// </summary>
    /// <param name="other">gameobject player collided with</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnvironmentalDanger")
        {
            Death();
        }
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamageFromObject();
        }
    }


    // Check which type of an object player collided with and either player takes damage or player health is refilled
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamageFromObject();
        }

        if(other.gameObject.tag == "CheckPoint")
        {
            currentHealth = startingHealth;
        }
    }


    // Gets a "hurt sound" audio clip when player takes damage
    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        int rnd = UnityEngine.Random.Range(0, clips.Length - 1);
        return clips[rnd];
    }
}
