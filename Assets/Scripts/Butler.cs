using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Planet { get; set; }
    [SerializeField] private Transform targetPostion;

    private Rigidbody ourRigidBody;
    
    void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>();
        Planet = GameObject.FindWithTag("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        

        var tr = transform;
        var down = (Planet.transform.position - tr.position).normalized;
        var forward = Vector3.Cross(tr.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
        
        
        
        // ourRigidBody.AddForce(Vector3.MoveTowards(transform.position, targetPostion.position, .2f));
        transform.position = Vector3.Lerp(transform.position, targetPostion.position, Time.deltaTime*3f);
        



    }

    private void FixedUpdate()
    {
        // var lerpy = Vector3.Lerp(targetPostion.position, transform.position, 3);
        // ourRigidBody.AddForce(Vector3.MoveTowards(transform.position, targetPostion.position, .2f));
        // transform.position = Vector3.Lerp(transform.position, targetPostion.position, .2f);    }
        // ourRigidBody.velocity = lerpy;

        // ourRigidBody.MovePosition(targetPostion.position * Time.fixedDeltaTime);
        

    }
}
