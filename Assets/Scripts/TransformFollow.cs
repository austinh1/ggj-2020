using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollow : MonoBehaviour
{
    public Transform transToFollow;

    public bool followRotation;
    public bool followPosition;
    public bool smoothFollow;

    public float rotSpeed;

    // Update is called once per frame
    void Update()
    {
        var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (followPosition)
        {
            transform.position = transToFollow.position;

        }

        if (followRotation)
        {
            if (!smoothFollow)
            {
                transform.rotation = transToFollow.rotation;
            }
            else if (smoothFollow && targetVelocity.z >= 0)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, transToFollow.localRotation, Time.deltaTime);
            }
        }
        
        
    }
}
