using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = manager.lastCheckPointPos;
    }



   /* private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/
}
// TO DO: VIELÄ PUUTTUU KOHTA, JOSSA ENIDIN KUOLLESSA RELOADATAAN KOKO SCENE UUDESTAAN, JOLLOIN PELAAJA SYNTYY PLAYERPOS:IIN
