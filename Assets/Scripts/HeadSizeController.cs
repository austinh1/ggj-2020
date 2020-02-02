using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSizeController : MonoBehaviour
{
    private Vector3 DesiredLocalScale { get; set; }

    private void Start()
    {
        DesiredLocalScale = transform.localScale;
    }

    public void SetDesiredLocalScale(Vector3 desiredScale)
    {
        DesiredLocalScale = desiredScale;
    }
    
    private void Update ()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, DesiredLocalScale, 2f * Time.deltaTime);
    }
}
