using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class KeijupolyController : MonoBehaviour
{// miksi float?
    //private float buginKorjausTaikanumero = 0;

    // serializeFieldillä näkyy (ei tallenna RAMiin vaan varsinaiseen muistiin ts. vakiot yms.) inspectorissa vaikkei public
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    [SerializeField]
    private Camera mainCamera;
    private List<Vector2> mousePoints;
    private Vector2[] colliderPoints;

    public PhysicsMaterial2D materiaali;

    //private float polunMaksimi = 100; // lisäämäni muuttuja

    public Material keijupolyMaterial;

    void Awake()
    {
        CreateLineRenderer();
        CreateEdgeCollider();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        mousePoints = new List<Vector2>();
    }


    // Start is called before the first frame update
    void Start()
    {

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

            if (!mousePoints.Contains(mousePosition) /*&& mousePoints.Count < polunMaksimi*/) // tätä riviä muutettu
            {
                mousePoints.Add(mousePosition);
                lineRenderer.positionCount = mousePoints.Count;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
            }

            //mousePoints.GetRange(0, 100);

            // float 
            colliderPoints = mousePoints.ToArray();

             /*for (int i = 0; i < colliderPoints.Length; i++)
             {
                 colliderPoints[i].x += buginKorjausTaikanumero;
             }*/
             edgeCollider.points = colliderPoints;
        }
    }

    private void Reset()
    {
        /* ei ymmärtääkseni tarvita, silloin viiva jää myös olemaan maailmaan ja häviää vasta seuraavan viivan luomiseen
         if (lineRenderer != null)
         {
             lineRenderer.positionCount = 0;
         }*/
        if (mousePoints != null)
        {
            mousePoints.Clear();
        }
        if (edgeCollider != null)
        {
            edgeCollider.Reset();
        }
    }

    private void CreateEdgeCollider()
    {
        edgeCollider = new GameObject("collider").AddComponent<EdgeCollider2D>();
        edgeCollider.sharedMaterial = materiaali;
    }

    private void CreateLineRenderer()
    {
        lineRenderer = new GameObject("renderer").AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = keijupolyMaterial;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.useWorldSpace = true;
    }
}
