using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform DetectGround;
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
