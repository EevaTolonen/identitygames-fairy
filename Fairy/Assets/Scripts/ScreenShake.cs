// @author Olli Paakkunainen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public float damping = 1f;    
    public float magnitude = 1f;

    private float shakeTimeLeft = 25f;
    private GameObject camera;
    private Vector2 startPosition;

    private void Awake()
    {
        camera = Camera.main.gameObject;
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        if(shakeTimeLeft > 0)
        {
            Vector3 shake = startPosition + Random.insideUnitCircle * magnitude;
            transform.localPosition = new Vector3(shake.x, shake.y, -25);
            shakeTimeLeft -= Time.deltaTime * damping;
        } else
        {
            startPosition = transform.localPosition;
        }
    }
}
