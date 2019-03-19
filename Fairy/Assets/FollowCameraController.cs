using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 vali;

    // Start is called before the first frame update
    void Start()
    {
        vali = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + vali;
    }
}
