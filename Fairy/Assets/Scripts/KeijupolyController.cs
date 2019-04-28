using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class KeijupolyController : MonoBehaviour
{
    [Header("Script created components")]
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    [SerializeField]
    private Camera mainCamera;

    //[SerializeField]
    //private PlatformEffector2D oneWayEffector;

    [Header("Physics")]
    public PhysicsMaterial2D materiaali;
    public float maxLength = 2;

    [Header("Appearance")]
    public float zPosition = -14;
    public Material lineMaterial;
    public AnimationCurve widthCurve = AnimationCurve.Linear(0, .2f, 1, .2f);

    private List<Vector2> mousePoints;
    private Vector2[] colliderPoints;
    private GameObject mouseDrawObject;
    private float lineLength = 0;
    private GameObject newLine;


    void Awake()
    {
        //SetUpOneWayEffector();
        SetupCamera();

        //Set reference for mouseDrawObject which is used to hold particle system
        mouseDrawObject = GameObject.Find("DrawEffect");

        //Inits list for captured mouse positions, used by collider and renderer
        mousePoints = new List<Vector2>();
    }

    void Update()
    {

        //edgeCollider.usedByEffector = true;
        if (Input.GetMouseButtonUp(0))
        {
            //Makes particle system thats following mouse hidden
            mouseDrawObject.SetActive(false);

            Reset();
        }

        if(Input.GetMouseButtonDown(0))
        {
            newLine = new GameObject("Keijupoly");
            newLine.AddComponent<KeijupolyLine>();
            newLine.GetComponent<LineRenderer>().material = lineMaterial;

            newLine.layer = 8;
        }

        if (Input.GetMouseButton(0))
        {

            //Makes particle system thats following mouse visible
            mouseDrawObject.SetActive(true);

            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (!mousePoints.Contains(mousePosition) && lineLength < maxLength)
            {
                mousePoints.Add(mousePosition);
                mouseDrawObject.transform.position = mousePosition;

                LineRenderer newRenderer = newLine.GetComponent<LineRenderer>();
                newRenderer.positionCount = mousePoints.Count;
                newRenderer.SetPosition(newRenderer.positionCount - 1, new Vector3(mousePosition.x, mousePosition.y, zPosition));
            }

            lineLength = GetDistanceBetweenFromArray(mousePoints.ToArray());
            //oneWayEffector.transform.position = edgeCollider.points[0];

            newLine.GetComponent<EdgeCollider2D>().points = mousePoints.ToArray();
        }
    }

    private void Reset()
    {
        mousePoints.Clear();
    }


    /// <summary>
    /// Try to give our drawn line some platform behaviour: now player should be able to jump through platform and land on it
    /// </summary>
    /*private void SetUpOneWayEffector()
    {
        if (oneWayEffector == null)
        {
            oneWayEffector = new GameObject("oneWayEffector").AddComponent<PlatformEffector2D>();
            oneWayEffector.transform.SetParent(gameObject.transform);
            //oneWayEffector.useColliderMask = true;

            /*oneWayEffector.colliderMask = 1;
            oneWayEffector.colliderMask = 2;
            oneWayEffector.colliderMask = 3;
            oneWayEffector.colliderMask = 4;

            oneWayEffector.useOneWay = true;
            oneWayEffector.surfaceArc = 180;
            oneWayEffector.sideArc = 0;
        }
    }*/

    /// <summary>
    /// Initializes edge collider and loads default configs
    /// </summary>
    private void SetupEdgeCollider()
    {
        if (edgeCollider == null)
        {
            edgeCollider = new GameObject("collider").AddComponent<EdgeCollider2D>();
            edgeCollider.transform.SetParent(gameObject.transform);
        }

        edgeCollider.sharedMaterial = materiaali;
    }

    /// <summary>
    /// Initializes line renderer and loads default configs
    /// </summary>
    private void SetupLineRenderer()
    {
        if (lineRenderer == null)
        {
            lineRenderer = new GameObject("renderer").AddComponent<LineRenderer>();
            lineRenderer.transform.SetParent(gameObject.transform);
        }

        lineRenderer.positionCount = 0;
        lineRenderer.material = lineMaterial;
        lineRenderer.widthCurve = widthCurve;
        lineRenderer.useWorldSpace = true;
    }

    /// <summary>
    /// Initializes camera used by line renderer
    /// </summary>
    private void SetupCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    /// <summary>
    /// Returns total distance of points in Vector2 array
    /// (V1 -> V2) + (V2 -> V3) + ... + (Vn-1) -> (Vn)
    /// </summary>
    private float GetDistanceBetweenFromArray(Vector2[] arr)
    {
        float totalDistance = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            totalDistance += GetDistanceBetween(arr[i - 1], arr[i]);
        }

        return totalDistance;
    }

    /// <summary>
    /// Returns distance between two points in 2d space
    /// </summary>
    private float GetDistanceBetween(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }
}
