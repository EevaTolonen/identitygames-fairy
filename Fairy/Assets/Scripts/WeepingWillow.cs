using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingWillow : MonoBehaviour
{
    public int health = 3;

    private WeepingWillowAnimations animations;
    private WeepingWillowSpikes spikes;
    private WeepingWillowTears tears;
    private bool isVulnerable = false;
    private int hitsTaken = 0;

    private void Awake()
    {
        animations = GetComponent<WeepingWillowAnimations>();
        spikes = GetComponent<WeepingWillowSpikes>();
        tears = GetComponent<WeepingWillowTears>();
    }

    public void StartBossFight()
    {
        InvokeRepeating("SpikeSweep", 2, 13);
        InvokeRepeating("TearBurst", 1, 5);
        Invoke("SetToVulnerable", 10);
    }

    private void SetToVulnerable()
    {
        isVulnerable = true;
        animations.EnterHurtMode();
    }

    private void SpikeSweep()
    {
        StartCoroutine(spikes.SpikeSweep());
    }

    private void TearBurst()
    {
        tears.SpawnTears();
    }

    public void TakeHit()
    {
        if(isVulnerable)
        {
            isVulnerable = false;
            hitsTaken++;

            if(hitsTaken >= health)
            {
                CancelInvoke();
            } else
            {
                animations.ExitHurtMode();
                Invoke("SetToVulnerable", 10);
            }

        }
    }
}
