using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    
    void Start()
    {
        var scale = Random.Range(.5f, 2f);
        
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
