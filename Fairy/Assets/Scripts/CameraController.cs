using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float currentTargetZoom;
    public float zoomSpeed = 50f;

    private void Awake()
    {
        currentTargetZoom = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (Camera.main.orthographicSize != currentTargetZoom)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, currentTargetZoom, zoomSpeed * Time.deltaTime);
        }
    }
}
