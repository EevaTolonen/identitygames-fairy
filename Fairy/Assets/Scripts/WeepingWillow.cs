using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingWillow : MonoBehaviour
{
    public int health = 5;
    public AudioClip afterBossBGM;
    public Sprite[] damageSprites;
    public SpriteRenderer spriteRenderer;
    public float shieldCloseTime = 5f;

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
        if(isVulnerable)
        {
            shieldTimer += Time.deltaTime;
            if(shieldTimer > shieldCloseTime)
            {
                shieldTimer = 0;
                SetToInvulnerable();
            }
        }
    }

    public void StartBossFight()
    {
        animations.BlinkEyes();
        InvokeRepeating("SpikeSweep", 2, 13);
        InvokeRepeating("TearBurst", 1, 7);
        //Invoke("SetToVulnerable", 10);
    }

    private void SetToVulnerable()
    {
        isVulnerable = true;
        animations.EnterHurtMode();
    }

    private void SetToInvulnerable()
    {
        isVulnerable = false;
        animations.ExitHurtMode();
    }

    private void SpikeSweep()
    {
        StartCoroutine(spikes.SpikeSweep());
    }

    private void TearBurst()
    {
        tears.SpawnTears();
    }

    public void FairyHit()
    {
        if(!isVulnerable)
            SetToVulnerable();
    }

    public void TakeHit()
    {
        if(isVulnerable)
        {
            isVulnerable = false;
            hitsTaken++;
            cameraScript.AddScreenShakeTime(1f);

            int damageSpriteIndex = hitsTaken - 1;
            spriteRenderer.sprite = damageSprites[damageSpriteIndex];

            if (hitsTaken >= health)
            {
                Destroy(spikes);
                Destroy(tears);

                animations.Deactivate();
                cameraScript.AddScreenShakeTime(5f);
                AudioSource audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
                audioSource.clip = afterBossBGM;
                audioSource.Play();
                Destroy(this);
            } else
            {
                shieldTimer = 0;
                animations.ExitHurtMode();
                //Invoke("SetToVulnerable", 10);
            }

        }
    }
}
