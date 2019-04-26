using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBGM : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public float fadeTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(audioSource.clip != audioClip)
            {
                Debug.Log("Clip change");
                StartCoroutine(ChangeClip());
            }
        }
    }

    private IEnumerator ChangeClip()
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        
        audioSource.clip = audioClip;

        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    } 
}
