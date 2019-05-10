using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingWillowAnimations : MonoBehaviour
{
    public GameObject shieldObject, leftEyeLight, rightEyeLight;
    public float riseAmount = 11f;
    public float moveSpeed = 20f;

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

    [ContextMenu("Rise shield")]
    public void EnterHurtMode()
    {
        moveStatus = State.Up;
        leftEyeLight.GetComponent<Light>().enabled = true;
        rightEyeLight.GetComponent<Light>().enabled = true;
    }

    [ContextMenu("Lower shield")]
    public void ExitHurtMode()
    {
        moveStatus = State.Down;
        leftEyeLight.GetComponent<Light>().enabled = false;
        rightEyeLight.GetComponent<Light>().enabled = false;
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
