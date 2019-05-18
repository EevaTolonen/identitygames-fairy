using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //Transform enemyPos;

    float hitTimer;
    bool isDead;

    bool damaged;
    float flashTimes = 5;

    Shader matta;
    Shader flash;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBody" && (hitTimer >= timeBetweenAttacks))
        {
            if (!evilTreeHealth.isDead) Attack();
        }

    }
}
