using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyDustManager : MonoBehaviour
{
    private GameObject[] fairyDusts;
    // Start is called before the first frame update
    void Start()
    {
        fairyDusts = new GameObject[] { };
    }

    // Update is called once per frame
    void Update()
    {
       fairyDusts = GameObject.FindGameObjectsWithTag("Keijupoly");
    }
}
