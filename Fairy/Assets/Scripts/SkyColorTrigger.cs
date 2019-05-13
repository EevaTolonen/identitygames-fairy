using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColorTrigger : MonoBehaviour
{
    public Color color;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.backgroundColor = color;
    }
}
