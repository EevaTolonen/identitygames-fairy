using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingWillow : MonoBehaviour
{
    public int health = 1;

    private GameObject leftEye, rightEye;
    private UnityStandardAssets._2D.Camera2DFollow cameraScript;
    private WeepingWillowAnimations animations;
    private WeepingWillowSpikes spikes;
    private WeepingWillowTears tears;
    private bool isVulnerable = false;
    private int hitsTaken = 0;

    private void Awake()
    {
        cameraScript = Camera.main.gameObject.GetComponent<UnityStandardAssets._2D.Camera2DFollow>(); ;
        animations = GetComponent<WeepingWillowAnimations>();
        spikes = GetComponent<WeepingWillowSpikes>();
        tears = GetComponent<WeepingWillowTears>();
    }

    public void StartBossFight()
    {
        InvokeRepeating("SpikeSweep", 2, 13);
        InvokeRepeating("TearBurst", 1, 7);
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
            cameraScript.AddScreenShakeTime(1f);

            if(hitsTaken >= health)
            {
                Destroy(spikes);
                Destroy(tears);

                animations.Deactivate();
                cameraScript.AddScreenShakeTime(5f);
                Destroy(this);
            } else
            {
                animations.ExitHurtMode();
                Invoke("SetToVulnerable", 10);
            }

        }
    }
}
