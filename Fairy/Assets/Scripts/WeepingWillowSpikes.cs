// @author Olli Paakkunainen

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


/// <summary>
/// Controller for weeping willow spikes-attack
/// </summary>
public class WeepingWillowSpikes : MonoBehaviour
{
    public GameObject[] spikes;              // Spike objects to rise, ordered from left -> right, 0 -> n
    public float timeBetweenSpikes = 0.5f;   // Time to wait between individual spike object
    public float timeBetweenSweeps = 1.5f;   // Time to wait between whole sweeps
    public bool debugMode = false;           // Starts infinite spike sweep at game load

    private void Awake()
    {
        if (debugMode)
        {
            InvokeRepeating("DebugSweep", 2, 13);
        }
    }

    private void DebugSweep()
    {
        StartCoroutine(SpikeSweep());
    }

    /// <summary>
    /// Rises each spike in array starting from 0.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpikeSweep()
    {
        // 0 -> n
        for(int i = 0; i < spikes.Length; i++)
        {
            spikes[i].GetComponent<SpikeController>().Activate();
            yield return new WaitForSeconds(timeBetweenSpikes);
        }

        yield return new WaitForSeconds(timeBetweenSweeps);

        // n -> 0
        for (int i = spikes.Length - 1; i >= 0; i--)
        {
            spikes[i].GetComponent<SpikeController>().Activate();
            yield return new WaitForSeconds(timeBetweenSpikes);
        }
    }
   
}
