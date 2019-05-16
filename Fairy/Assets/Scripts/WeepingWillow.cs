// @author Olli Paakkunainen

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages Weeping Willow boss event
/// </summary>
public class WeepingWillow : MonoBehaviour
{
    public int health = 5;
    public AudioClip afterBossBGM;
    public Sprite[] damageSprites;
    public SpriteRenderer spriteRenderer;
    public float shieldCloseTime = 5f;
    public GameObject[] exitSmokes;

    private Camera camera;
    private GameObject leftEye, rightEye;
    private UnityStandardAssets._2D.Camera2DFollow cameraScript;
    private WeepingWillowAnimations animations;
    private WeepingWillowSpikes spikes;
    private WeepingWillowTears tears;
    private bool isVulnerable = false;
    private int hitsTaken = 0;
    private float shieldTimer = 0;


    private void Awake()
    {
        camera = Camera.main;
        cameraScript = camera.gameObject.GetComponent<UnityStandardAssets._2D.Camera2DFollow>(); ;
        animations = GetComponent<WeepingWillowAnimations>();
        spikes = GetComponent<WeepingWillowSpikes>();
        tears = GetComponent<WeepingWillowTears>();
    }

    private void Update()
    {
        // Run shield timer when shield is lowered
        if(isVulnerable)
        {
            shieldTimer += Time.deltaTime;
            if(shieldTimer > shieldCloseTime)
            {
                // Raise shield if player hasn't managed hit boss in time
                shieldTimer = 0;
                SetToInvulnerable();
            }
        }
    }

    /// <summary>
    /// Starts the boss fight by invoking spikes and tears
    /// </summary>
    public void StartBossFight()
    {
        foreach(GameObject smoke in exitSmokes)
        {
            smoke.SetActive(true);
        }
        animations.BlinkEyes();
        InvokeRepeating("SpikeSweep", 2, 13);
        InvokeRepeating("TearBurst", 1, 7);
    }

    // Rises shield and makes boss vulnerable
    private void SetToVulnerable()
    {
        isVulnerable = true;
        animations.EnterHurtMode();
    }

    // Lowers shield and makes boss invulnerable
    private void SetToInvulnerable()
    {
        isVulnerable = false;
        animations.ExitHurtMode();
    }

    // Wrapper for spike controller
    private void SpikeSweep()
    {
        StartCoroutine(spikes.SpikeSweep());
    }

    // Wrapper for tear controller
    private void TearBurst()
    {
        tears.SpawnTears();
    }

    // EnidAttack script calls this when fairy on boss is hit
    public void FairyHit()
    {
        if(!isVulnerable)
            SetToVulnerable();
    }

    /// <summary>
    /// EnidAttack script calls this when boss is hit
    /// </summary>
    public void TakeHit()
    {
        if(isVulnerable)
        {
            isVulnerable = false;
            hitsTaken++;

            // Shake screen for 1 second
            cameraScript.AddScreenShakeTime(1f);

            // Change boss appearance based on hits taken
            int damageSpriteIndex = hitsTaken - 1;
            spriteRenderer.sprite = damageSprites[damageSpriteIndex];

            // Kill boss if enough hits taken else 
            if (hitsTaken >= health)
            {
                BossDead();
            }
            else
            {
                // Reset shield timer and lower shield
                shieldTimer = 0;
                animations.ExitHurtMode();
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void BossDead()
    {
        foreach (GameObject smoke in exitSmokes)
        {
            smoke.SetActive(false);
        }
        Destroy(spikes);
        Destroy(tears);

        animations.Deactivate();
        cameraScript.AddScreenShakeTime(5f);
        AudioSource audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        audioSource.clip = afterBossBGM;
        audioSource.Play();
        Destroy(this);
    }
}
