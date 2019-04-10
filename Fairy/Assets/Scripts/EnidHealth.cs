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

    Rigidbody2D player;
    int knockbackForce = 40;

    // 
    public static GameObject[] checkpointList;

    // Platformer2DUserControl won't work after taking damage for x time 
    float knockbackTimer;
    float flashTimes = 5;

    // Start is called before the first frame update
    void Awake()
    {
        enidHealth = GetComponent<EnidHealth>();
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
        platformer2DUserControl = GetComponent<Platformer2DUserControl>();
        enemyHealth = GetComponent<EnemyHealth>();

        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
        player = GetComponent<Rigidbody2D>();

        checkpointList = GameObject.FindGameObjectsWithTag("Checkpoint");
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

        // tällä erää ei toimi, korjaa
        player.transform.position = Checkpoint.GetActiveCheckpoint();

        platformer2DUserControl.enabled = true;

        Debug.Log("Enid died, health left " + currentHealth);
    }



    public IEnumerator SwitchToDamageShader()
    {
        for (int i = 0; i < flashTimes; i++)
        {
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.4f);
            yield return new WaitForSeconds(0.05f);
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0);
            yield return new WaitForSeconds(0.1f);
        }
    }



    /// <summary>
    /// If player hits the spikes, they return to the previous checkpoint
    /// </summary>
    /// <param name="other">gameobject player collided with</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnvironmentalDanger")
        {
            currentHealth = startingHealth;
            Death();
        }
    }
}
