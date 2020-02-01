using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Vector3 DesiredLocalPosition { get; set; }

    private void Start()
    {
        DesiredLocalPosition = transform.localPosition;
    }

    public void SetDesiredLocalPosition(Vector3 desiredPosition)
    {
        DesiredLocalPosition = desiredPosition;
    }
    
    private void Update ()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, DesiredLocalPosition, 2f * Time.deltaTime);
    }
}