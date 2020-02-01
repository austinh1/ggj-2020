using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    public List<Rigidbody> floaters = new List<Rigidbody>();
    public float pullForce;

    public void Attract()
    {
        foreach (var rbody in floaters)
        {
            Vector3 forceDirection = transform.position - rbody.transform.position;
 
            // apply force on target towards me
            rbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);

        }
    }

    public void Update()
    {
        Attract();
    }
}