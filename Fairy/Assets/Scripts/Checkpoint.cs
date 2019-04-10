using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // indicates if this checkpoint is activated
    public bool activated = false;
    public static GameObject[] checkpointList;

    // Start is called before the first frame update
    void Start()
    {
        checkpointList = GameObject.FindGameObjectsWithTag("Checkpoint");
        Debug.Log("We have checkpoints: " + checkpointList[0].ToString() + " " + checkpointList[1].ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }



    /// <summary>
    /// Sets this checkpoint active and disables all other checkpoints
    /// </summary>
    private void ActivateCheckpoint()
    {
        foreach (GameObject point in checkpointList)
        {   // now we can access each checkpoint's own script in order to get the bool activated
            point.GetComponent<Checkpoint>().activated = false;
        }
        activated = true;
    }



    /// <summary>
    /// When player passes through the checkpoint, it's activated
    /// </summary>
    /// <param name="other">gameobject that collides with this checkpoint</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ActivateCheckpoint();
        }
    }



    /// <summary>
    /// Returns the position of the active checkpoint
    /// </summary>
    /// <returns>position of the active checkpoint</returns>
    public static Vector3 GetActiveCheckpoint()
    {
        // if no checkpoint is active, player returns to the beginning of the level
        Vector3 position = GameObject.FindGameObjectWithTag("StartingPoint").transform.position;

        if (checkpointList != null)
        {
            foreach (GameObject point in checkpointList)
            {// tääkö on null? pitääkö tarkistaa ettei point ole null?
             // miksi toimi aluksi muttei enää? 
             // Muutokset toimivan koodin jälkeen:
             // Lisäsin kolmannen checkpointin & Laitoin piikkeihin osumisesta siirtymään edelliseen checkpointtiin
                if (point.GetComponent<Checkpoint>().activated)
                {
                    position = point.transform.position;
                    break;
                }
            }
        }
        return position;
    }
}
