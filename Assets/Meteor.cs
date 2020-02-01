using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private GameObject Planet { get; set; }
    
    [SerializeField]
    private float initialSpeed = 30;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
