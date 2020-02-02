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
    private float timeToWait = 0;

    // Update is called once per frame
    void Update()
    {
        var targetVelocity = Vector3.zero;
        if (!transform.parent.GetComponent<PlayerScore>().GameEnded)
        {
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

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

            // if (smoothFollow && targetVelocity.x != 0 ||
            //     smoothFollow && targetVelocity.z != 0 )
            // {
            //     timeToWait = 0;
            // }

            timeToWait += Time.deltaTime;
            if (smoothFollow && targetVelocity.x != 0 && targetVelocity.z == 0)
            //if (timeToWait > 0.5 )
            {
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, transToFollow.localRotation, Time.deltaTime);
            }
        }
    }
}