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
    
    [Header("Physics")]
    public PhysicsMaterial2D materiaali;

    [Header("Appearance")]
    public Material lineMaterial;
    public AnimationCurve widthCurve = AnimationCurve.Linear(0, .2f, 1, .2f);

    private List<Vector2> mousePoints;
    private Vector2[] colliderPoints;
    private GameObject mouseDrawObject;

    void Awake() {
        SetupLineRenderer();
        SetupEdgeCollider();
        SetupCamera();

        mouseDrawObject = GameObject.Find("DrawEffect");

        mousePoints = new List<Vector2>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseDrawObject.active = false;
            Reset();
        }

        if (Input.GetMouseButton(0))
        {
            mouseDrawObject.active = true;
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (!mousePoints.Contains(mousePosition))
            {
                mousePoints.Add(mousePosition);
                lineRenderer.positionCount = mousePoints.Count;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
                mouseDrawObject.transform.position = mousePosition;
            }
            
            edgeCollider.points = mousePoints.ToArray();
        }
    }

    private void Reset()
    {
        mousePoints.Clear();
        edgeCollider.Reset();
    }

    private void SetupEdgeCollider()
    {
        if (edgeCollider == null) {
            edgeCollider = new GameObject("collider").AddComponent<EdgeCollider2D>();
            edgeCollider.transform.SetParent(gameObject.transform); 
        }

        edgeCollider.sharedMaterial = materiaali;
    }

    private void SetupLineRenderer()
    {
        if (lineRenderer == null) {
            lineRenderer = new GameObject("renderer").AddComponent<LineRenderer>(); 
            lineRenderer.transform.SetParent(gameObject.transform);
        }

        lineRenderer.positionCount = 0;
        lineRenderer.material = lineMaterial;
        lineRenderer.widthCurve = widthCurve;
        lineRenderer.useWorldSpace = true;
        
    }

    private void SetupCamera() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }
    }
}
