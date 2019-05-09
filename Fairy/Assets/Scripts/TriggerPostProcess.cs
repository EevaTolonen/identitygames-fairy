using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPostProcess : MonoBehaviour
{
    public enum DayState { Day, Night }

    [Range(0,1)]
    public float radius = 0.7f;
    [Range(0, 1)]
    public float softness = 0.7f;

    public Camera _camera;
    public DayState onTriggerChangeTo = DayState.Day;

    [Header("Camera zoom")]
    public bool zoomActive = false;
    public float targetOrtographicSize;

    private PostProcess postProcess;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        postProcess = _camera.GetComponent<PostProcess>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(zoomActive)
            {
                _camera.gameObject.GetComponent<CameraController>().currentTargetZoom = targetOrtographicSize;
            }

            switch(onTriggerChangeTo)
            {
                case DayState.Day:
                    postProcess.SetToDay();
                    break;
                case DayState.Night:
                    postProcess.SetToNight();
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
