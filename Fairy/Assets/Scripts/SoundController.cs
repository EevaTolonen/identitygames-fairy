using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip forestBackground;
    public AudioClip caveBackground;

    private GameObject BGM;
    private AudioSource audioSource;

    private void Awake()
    {
        BGM = GameObject.FindGameObjectWithTag("BGM");

        if (BGM == null)
            Debug.Log("GameObject with tag 'BGM' could not been found.");

        audioSource = BGM.GetComponent<AudioSource>();
    }

    public void SetToDay()
    {

        audioSource.clip = forestBackground;
        audioSource.Play();
    }

    public void SetToNight()
    {

        audioSource.clip = caveBackground;
        audioSource.Play();
    }
}
