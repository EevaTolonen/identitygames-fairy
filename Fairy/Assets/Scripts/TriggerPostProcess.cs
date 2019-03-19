using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPostProcess : MonoBehaviour
{
    [Range(0,1)]
    public float radius = 0.7f;
    [Range(0, 1)]
    public float softness = 0.7f;

    public Camera camera;

    private PostProcess postProcess;

    private void Awake()
    {
        if (camera == null)
            camera = Camera.main;

        postProcess = camera.GetComponent<PostProcess>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            postProcess.material.SetFloat("_VRadius", radius);
            postProcess.material.SetFloat("_VSoft", softness); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
