using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTreeController : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private Vector2 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerInRange();
    }


    private void CheckIfPlayerInRange()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 10)
        {
            anim.SetTrigger("PahaPuuAttack");
        }
    }
}
