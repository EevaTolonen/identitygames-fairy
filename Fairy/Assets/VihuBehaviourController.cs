using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VihuBehaviourController : MonoBehaviour
{
    Vihu vihulainen;
    // Start is called before the first frame update
    void Start()
    {
        vihulainen = new Vihu(3, 3, "Vihulainen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
