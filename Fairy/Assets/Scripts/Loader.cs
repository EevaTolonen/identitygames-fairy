using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Loads the gameManager if it does not have an instance in the beginning
/// </summary>
public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
