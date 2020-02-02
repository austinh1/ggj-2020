using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpright : MonoBehaviour
{
    private GameObject Planet { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Planet = GameObject.FindWithTag("Planet");

        var tr = transform;
        var down = (Planet.transform.position - tr.position).normalized;
        var forward = Vector3.Cross(tr.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
    }

}
