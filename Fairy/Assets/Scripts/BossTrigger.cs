//@author Olli Paakkunainen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles starting of the bossfight when player collides with the boss
public class BossTrigger : MonoBehaviour
{
    public GameObject boss;

    private bool bossStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !bossStarted)
        {
            bossStarted = true;

            if(boss.GetComponent<WeepingWillow>())
                boss.GetComponent<WeepingWillow>().StartBossFight();
        }
    }
}
