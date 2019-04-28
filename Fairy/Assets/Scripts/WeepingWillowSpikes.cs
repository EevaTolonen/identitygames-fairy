using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// HOW TO USE:
//
// To sweep with spikes from left to right to left, 
// start the coroutine with:
// StartCoroutine(SpikeSweep());
// 
// If called from other object use correct reference:
// StartCoroutine(WeepingWillowSpikes.SpikeSweep());
//

public class WeepingWillowSpikes : MonoBehaviour
{
    public GameObject[] spikes;
    public float timeBetweenSpikes = 0.5f;
    public float timeBetweenSweeps = 1.5f;
    public bool debugMode = false;

    private void Awake()
    {
        if (debugMode)
            InvokeRepeating("DebugSweep", 2, 13);
    }

    private void DebugSweep()
    {
        StartCoroutine(SpikeSweep());
    }

    public IEnumerator SpikeSweep()
    {
        for(int i = 0; i < spikes.Length; i++)
        {
            spikes[i].GetComponent<SpikeController>().Activate();
            yield return new WaitForSeconds(timeBetweenSpikes);
        }

        yield return new WaitForSeconds(timeBetweenSweeps);

        for (int i = spikes.Length - 1; i >= 0; i--)
        {
            spikes[i].GetComponent<SpikeController>().Activate();
            yield return new WaitForSeconds(timeBetweenSpikes);
        }
    }
   
}
