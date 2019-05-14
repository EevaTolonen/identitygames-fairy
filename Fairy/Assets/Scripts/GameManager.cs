using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Vector2 lastCheckPointPos = Vector2.zero;

    private Transform startingPoint;

    // GameManager that manages the state of our game
    // Start is called before the first frame update
    void Awake()
    {
        startingPoint = GameObject.FindGameObjectWithTag("StartingPoint").transform;
        if (lastCheckPointPos == Vector2.zero) lastCheckPointPos = startingPoint.position;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.M))
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-737, -725, 0); 
        }
    }
}





    /* void InitGame()
     {
         throw new NotImplementedException();
         // ensimerkkikoodissa script.setUpscene(level), mutta meillä jotain muuta
     }*/

