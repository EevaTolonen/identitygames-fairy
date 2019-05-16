using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingWillowAnimations : MonoBehaviour
{
    public GameObject shieldObject, leftEyeLight, rightEyeLight;
    public float riseAmount = 11f;
    public float moveSpeed = 20f;
    public AudioClip shieldOpen;


    public enum State { Neutral, Up, Down }
    public State moveStatus = State.Neutral;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = shieldObject.transform.position;

    }

    private void FixedUpdate()
    {
        switch (moveStatus)
        {
            case State.Neutral:
                break;
            case State.Up:
                Move(Direction.Up);
                break;
            case State.Down:
                Move(Direction.Down);
                break;
        }
    }

    public void BlinkEyes()
    {
        StartCoroutine("Blink");
    }

    private IEnumerator Blink()
    {
        int blinkCount = 4;

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < blinkCount; i++)
        {
            leftEyeLight.GetComponent<Light>().enabled = true;
            rightEyeLight.GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(0.3f);
            leftEyeLight.GetComponent<Light>().enabled = false;
            rightEyeLight.GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(0.3f); 
        }
    }

    [ContextMenu("Rise shield")]
    public void EnterHurtMode()
    {
        moveStatus = State.Up;
        leftEyeLight.GetComponent<Light>().enabled = true;
        rightEyeLight.GetComponent<Light>().enabled = true;
        GetComponent<AudioSource>().PlayOneShot(shieldOpen);
    }

    [ContextMenu("Lower shield")]
    public void ExitHurtMode()
    {
        moveStatus = State.Down;
        leftEyeLight.GetComponent<Light>().enabled = false;
        rightEyeLight.GetComponent<Light>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(shieldOpen);
    }

    public void Deactivate()
    {
        leftEyeLight.GetComponent<Light>().color = Color.green;
        rightEyeLight.GetComponent<Light>().color = Color.green;
    }

    private void Move(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                shieldObject.transform.Translate(Vector2.up * (Time.deltaTime * moveSpeed));

                if (shieldObject.transform.position.y > startPosition.y + riseAmount)
                {
                    moveStatus = State.Neutral;
                }

                break;
            case Direction.Down:
                shieldObject.transform.Translate(-Vector2.up * (Time.deltaTime * moveSpeed));

                if (shieldObject.transform.position.y < startPosition.y)
                {
                    shieldObject.transform.position = startPosition;
                    moveStatus = State.Neutral;
                }

                break;
            default:
                break;
        }
    }
}
