// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles attack of enemy type evilTree
public class EvilTreeAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    private Animator anim;
    GameObject player;
    private Vector2 playerPos;

    public int startingHealth = 2;
    public int currentHealth;
    public int attackDamage = 1;

    EnidHealth enidHealth;
    EvilTreeHealth evilTreeHealth;
    // Transform enemyPos;

    float hitTimer;
    private bool isDead;

    private bool damaged;
    private float flashTimes = 5;

    private Shader matta;
    private Shader flash;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        enidHealth = player.GetComponent<EnidHealth>();
        evilTreeHealth = GetComponent<EvilTreeHealth>();
        //enemyPos = GetComponent<Transform>();
        hitTimer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        hitTimer += Time.deltaTime;
        anim.SetFloat("HitTimer", hitTimer);
        //CheckIfPlayerInRange();
    }


    private void CheckIfPlayerInRange()
    {

        //animator.SetFloat("HitTimer", hitTimer);
        if ((hitTimer >= timeBetweenAttacks) && (Vector2.Distance(player.transform.position, transform.position) < 15))
        {
            anim.SetTrigger("PahaPuuAttack");
            Attack();
        }
    }


    // Checks that player has health left, and player takes no knockback from the enemy
    void Attack()
    {
        if (enidHealth.currentHealth > 0)
        {
            anim.SetTrigger("PahaPuuAttack");
            enidHealth.TakeDamage(attackDamage, Vector3.zero);
        }
        hitTimer = 0f;
        anim.SetFloat("HitTimer", hitTimer);
    }


    // Checks that enemy collides with player (not spear) and if enemy's time between attacks is already exceeded in order to attack
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBody" && (hitTimer >= timeBetweenAttacks))
        {
            if (!evilTreeHealth.isDead) Attack();
        }
    }
}
