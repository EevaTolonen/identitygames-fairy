using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(EdgeCollider2D))]
public class KeijupolyLine : MonoBehaviour
{
    private List<Vector2> mousePoints = new List<Vector2>();
    private Vector2[] colliderPoints;
    private LineRenderer lineRenderer;

    private GameObject player;
    private EdgeCollider2D edgeCol;
    private Transform groundCol;


    public List<Vector2> MousePoints
    {
        get
        {
            return mousePoints;
        }

        set
        {
            mousePoints = value;
        }
    }



    public Vector2[] ColliderPoints
    {
        get
        {
            return colliderPoints;
        }

        set
        {
            colliderPoints = value;
        }
    }



    private float timer = 0f;
    private float timeToLive = 20f;



    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        //lineRenderer.material = lineMaterial;
        //lineRenderer.widthCurve = widthCurve;
        lineRenderer.useWorldSpace = true;
        lineRenderer.numCapVertices = 100;
        lineRenderer.numCornerVertices = 100;
        lineRenderer.sortingOrder = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        edgeCol = GetComponent<EdgeCollider2D>();
        groundCol = player.transform.Find("GroundCheck");
    }



    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
