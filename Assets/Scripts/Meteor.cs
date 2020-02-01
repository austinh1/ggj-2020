using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour
{
    private GameObject Planet { get; set; }
    
    public float distToGround;

    [SerializeField]
    private float initialSpeed = 30;

    private const String PLANET = "Planet";

    // Start is called before the first frame update
    void Start()
    {
        var tr = transform;
        Planet = GameObject.FindWithTag("Planet");
        var down = (Planet.transform.position - tr.position).normalized;
        var direction = new Vector3(0, 0, Random.Range(0, 360));
        var forward = Vector3.Cross(direction, down);
        tr.rotation = Quaternion.LookRotation(forward, -down);
        GetComponent<Rigidbody>().AddForce(tr.forward * initialSpeed, ForceMode.VelocityChange);
        Debug.DrawRay(transform.position, tr.forward * 50, Color.green);
        var initialTorque = new Vector3(Random.Range(-1,1), Random.Range(-1,1), Random.Range(-1,1)) * 50;
        GetComponent<Rigidbody>().AddTorque(initialTorque);
    }

    private void OnCollisionEnter(Collision other)
    {
        //doesn't work, would like to fix if we have time for pollishing.
        if(other.collider.tag.Equals(PLANET) && GetComponent<Rigidbody>().angularDrag != 0)
        {
            Debug.Log("HIT");
            GetComponent<Rigidbody>().angularDrag = 1f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
