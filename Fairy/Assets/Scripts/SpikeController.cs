using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }

public class SpikeController : MonoBehaviour
{
    public float riseAmount = 11f;
    public float moveSpeed = 20f;
    
    public enum State { Neutral, Up, Down }
    public State moveStatus = State.Neutral;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = gameObject.transform.position;

    }

    public void Activate()
    {
        moveStatus = State.Up;
    }

    private void FixedUpdate()
    {
        switch(moveStatus)
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

    private void Move(Direction dir)
    {
        switch(dir)
        {
            case Direction.Up:
                gameObject.transform.Translate(Vector2.up * (Time.deltaTime * moveSpeed));

                if(gameObject.transform.position.y > startPosition.y + riseAmount)
                {
                    moveStatus = State.Down;
                }

                break;
            case Direction.Down:
                gameObject.transform.Translate(-Vector2.up * (Time.deltaTime * moveSpeed));

                if (gameObject.transform.position.y < startPosition.y)
                {
                    gameObject.transform.position = startPosition;
                    moveStatus = State.Neutral;
                }

                break;
            default:
                break;
        }
    }
}
