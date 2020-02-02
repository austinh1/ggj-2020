using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Planet { get; set; }
    private GameObject Player { get; set; }
    [SerializeField] private Transform targetPostion;

    void Start()
    {
        Planet = GameObject.FindWithTag("Planet");
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var tr = transform;
        var down = (Planet.transform.position - tr.position).normalized;
        var towardsPlayer = (Player.transform.position - tr.position).normalized;
        var forward = Vector3.Cross(towardsPlayer, down);
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x + 120, forward.y, forward.z), -down);
        
        transform.position = Vector3.Lerp(tr.position, targetPostion.position, Time.deltaTime * 3f);
    }
}
