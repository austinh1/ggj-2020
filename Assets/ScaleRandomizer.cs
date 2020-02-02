using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScaleRandomizer : MonoBehaviour
{
    private GameObject Player { get; set; }
    [SerializeField] private GameObject visual;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        var scale = Random.Range(.5f, 2f);
        
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
