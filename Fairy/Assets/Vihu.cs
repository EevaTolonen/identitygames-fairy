using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A base class for the enemies, includes hitpoints, damage the enemy makes and enemy name
public class Vihu // tuleeko jättää MonoBehaviour pois?
{
    public float hitPoints;
    public float damage;
    public string enemyName;

    // class constructor for the enemy
    public Vihu(float hp, float dmg, string eName)
    {
        hitPoints = hp;
        damage = dmg;
        enemyName = eName;
    }


    // Handler for the damage the enemy takes
    public void TakeDamage()
    {
        hitPoints--;
        Debug.Log(enemyName + " took damage, health left: " + hitPoints);
        if (hitPoints <= 0) Die();
    }


    // Handles enemy's hitpoints going to 0, enemy dies
    public void Die()
    {
        Debug.Log(enemyName + " has died.");
    }
}
