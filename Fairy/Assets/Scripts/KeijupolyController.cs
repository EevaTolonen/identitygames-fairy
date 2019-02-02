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
    
    [Header("Edge Collider")]
    public PhysicsMaterial2D materiaali;

    [Header("Line Renderer")]
    public Material keijupolyMaterial;

    private List<Vector2> mousePoints;
    private Vector2[] colliderPoints;

    void Awake() {
        SetupLineRenderer();
        SetupEdgeCollider();
        SetupCamera();

        mousePoints = new List<Vector2>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (!mousePoints.Contains(mousePosition))
            {
                mousePoints.Add(mousePosition);
                lineRenderer.positionCount = mousePoints.Count;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
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
        }

        edgeCollider.sharedMaterial = materiaali;
    }

    private void SetupLineRenderer()
    {
        if (lineRenderer == null) {
            lineRenderer = new GameObject("renderer").AddComponent<LineRenderer>(); 
        }

        lineRenderer.positionCount = 0;
        lineRenderer.material = keijupolyMaterial;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.useWorldSpace = true;
    }

    private void SetupCamera() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }
    }
}
