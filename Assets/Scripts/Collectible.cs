using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameObject Planet { get; set; }
    private void Start()
    {
        Planet = GameObject.FindWithTag("Planet");

        var tr = transform;
        var down = (Planet.transform.position - tr.position).normalized;
        var forward = Vector3.Cross(tr.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
    }
}
