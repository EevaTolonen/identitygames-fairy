// @author Olli Paakkunainen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GameEndTrigger : MonoBehaviour
{
    public Platformer2DUserControl platformer2DUserControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            platformer2DUserControl.stopMovement = true;
        }
    }
}
