using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
// Anything with a rigidbody and inside the planets trigger collidor will be effected by gravity

    float gravity = -9.81f;

    void OnTriggerStay(Collider other)
    {
        // get the direction vector and normalize it (magnitude = 1)
        Vector3 direction = other.transform.position - transform.position;
        Vector3 force = direction.normalized * gravity;

        // accelerate the object in that direction
        Rigidbody rbody;
        if ((rbody = other.GetComponent<Rigidbody>()) != null)
            other.GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
    }
}