using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float refY = 0f;
    public float smoothTime;

    public Transform target;

    private void Update()
    {
        var yPos = Mathf.SmoothDamp(transform.position.y, target.position.y - 4f, ref refY, smoothTime);
        transform.position = new Vector3(0f, yPos, -5f);
    }
}
