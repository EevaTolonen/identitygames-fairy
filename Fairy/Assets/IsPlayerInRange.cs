using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInRange : MonoBehaviour
{
    GameObject parentti;
    GameObject boggart;

    // Start is called before the first frame update
    void Start()
    {
        // we get the parent of the colliders and boggart in order to get in touch with parent's other child, boggart
        parentti = transform.parent.gameObject;
        // so this line goes nowhere...
        Debug.Log("Parent of our colliders is " + parentti.name);
        boggart = parentti.transform.Find("Boggart").gameObject;


        Debug.Log("Child of BoggartObject is " + boggart.name);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player is inside the colliders");
        if (other.gameObject.tag != "Player") return;

        if (name == "ColliderRight")
        {
            Debug.Log("We are in RIGHT");

            if (other.transform.position.x < transform.position.x) boggart.GetComponent<BoggartAttack>().playerInRange = false;
            if (other.transform.position.x > transform.position.x) boggart.GetComponent<BoggartAttack>().playerInRange = true;
        }

        if (name == "ColliderLeft")
        {
            Debug.Log("We are in LEFT");
            if (other.transform.position.x > transform.position.x) boggart.GetComponent<BoggartAttack>().playerInRange = false;
            if (other.transform.position.x < transform.position.x) boggart.GetComponent<BoggartAttack>().playerInRange = true;
        }
    }
}

