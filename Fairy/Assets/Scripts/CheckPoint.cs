// @author Eeva Tolonen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles that checkpoint is updated to manager when player collides with it
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
        }
    }
}
