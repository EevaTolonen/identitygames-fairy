// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles other scripts attached to the enemy (health, attack) 
public class EvilTreeController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EvilTreeHealth>().isDead)
        {
            GetComponent<EvilTreeHealth>().enabled = false;
            GetComponent<EvilTreeAttack>().enabled = false;
        }
    }
}
