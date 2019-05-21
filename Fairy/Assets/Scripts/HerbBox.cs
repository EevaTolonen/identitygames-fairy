// @author Olli Paakkunainen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbBox : MonoBehaviour
{
    public GameObject block;
    public Dialog successDialog;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            successDialog.isActive = true;
            Destroy(block);
            Destroy(gameObject);
        }
    }
}
