using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;
using System;

public class EnidHealth : MonoBehaviour
{
    Platformer2DUserControl platformer2DUserControl;
    Animator animator;

    public int startingHealth = 30;
    public int currentHealth;

    bool damaged;
    public Material flashMaterial;
    public Material mattaMaterial;
    
    [Header("Sounds:")]
    public AudioClip[] hurt;


    AudioSource audioSource;

    Rigidbody2D player;
    int knockbackForce = 40;

    // Platformer2DUserControl won't work after taking damage for x time 
    float knockbackTimer;
    float flashTimes = 5;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        platformer2DUserControl = GetComponent<Platformer2DUserControl>();

        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (knockbackTimer > 0.5) platformer2DUserControl.enabled = true;
        if (knockbackTimer != 0) knockbackTimer += Time.deltaTime;

        // changes to player sprite are made here, sprite flashes when taking damage
        if (damaged)
        {
            StartCoroutine(SwitchToDamageShader());
            damaged = false;
        }
    }



    public void TakeDamage(int amount, Vector3 enemyPos)
    {
        damaged = true;

        if (knockbackTimer > 0) knockbackTimer = 0;
        audioSource.PlayOneShot(GetRandomClip(hurt));
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
    
    void Death()
    {
        //onDeath.Invoke();
        animator.SetTrigger("IsDead");
        //platformer2DUserControl.enabled = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //platformer2DUserControl.enabled = true;

        //Debug.Log("Enid died, health left " + currentHealth);
    }



    public IEnumerator SwitchToDamageShader()
    {
        player.GetComponent<Renderer>().material = flashMaterial;
        for (int i = 0; i < flashTimes; i++)
        {
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.05f);
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0);
            yield return new WaitForSeconds(0.1f);
        }
        player.GetComponent<Renderer>().material = mattaMaterial;
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
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        int rnd = UnityEngine.Random.Range(0, clips.Length - 1);
        return clips[rnd];
    }
}
