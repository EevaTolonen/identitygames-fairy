using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameManager manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            manager.lastCheckPointPos = transform.position;
            // apparently one has to make an instance reference to the code one's trying to access
            //CheckPointManager manager = new CheckPointManager();
            //CheckPoint checkPoint = new CheckPoint();

            // eli ongelmana tällaisenaan object reference not set to an instance of an object siis manager ei ole instanssi CheckPointManagerista
        }
    }
}
