using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaihdokkiMovement : MonoBehaviour
{
    public GameObject followTarget;
    public Vector3 offset = Vector3.zero;
    public float baseSpeed = 10f;
    public float maxDistance = 2;
    public float maxDistanceSpeedModifier = 2;

    private void Awake()
    {
        if (followTarget == null)
            followTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float moveSpeed = baseSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, followTarget.transform.position + offset);
        Debug.Log(distanceToTarget);
        if (distanceToTarget > maxDistance)
        {
            moveSpeed = baseSpeed * maxDistanceSpeedModifier;
        }

        // Move our position a step closer to the target.
        float step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, followTarget.transform.position + offset, step);
    }
}
