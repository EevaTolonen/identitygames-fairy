using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayState { Day, Night }

public class TriggerPostProcess : MonoBehaviour
{
    [Range(0,1)]
    public float radius = 0.7f;
    [Range(0, 1)]
    public float softness = 0.7f;

    public Camera _camera;
    public DayState onTriggerChangeTo = DayState.Day;

    private PostProcess postProcess;
    private SoundController soundController;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        postProcess = _camera.GetComponent<PostProcess>();

        soundController = GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch(onTriggerChangeTo)
            {
                case DayState.Day:
                    postProcess.SetToDay();
                    soundController.SetToDay();
                    break;
                case DayState.Night:
                    postProcess.SetToNight();
                    soundController.SetToNight();
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
