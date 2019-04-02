using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private int level = 1;
    // GameManager that manages the state of our game
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        //InitGame();
    }




   /* void InitGame()
    {
        throw new NotImplementedException();
        // ensimerkkikoodissa script.setUpscene(level), mutta meillä jotain muuta
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}

