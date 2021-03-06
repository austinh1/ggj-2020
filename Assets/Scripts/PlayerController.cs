﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Transform target;
    private Rigidbody rb;
    private int count;

    public float height;
    public float distance;
    public float damping;
    public float rotationDamping;
    public Transform cameraTransform;

    void Update () 
    {        
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            movementDirection += cameraTransform.up;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            movementDirection += -cameraTransform.up;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            movementDirection += cameraTransform.right;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movementDirection += -cameraTransform.right;
        }

        movementDirection.Normalize();

        UpdatePlayerTransform(movementDirection);
    }
    
    private void FixedUpdate()
    {
        // Calculate and set camera position
        Vector3 desiredPosition = this.target.TransformPoint(0, height, -this.distance);
        this.transform.position = Vector3.Lerp(this.transform.position, desiredPosition, Time.deltaTime * this.damping);

        // Calculate and set camera rotation
        Quaternion desiredRotation = Quaternion.LookRotation(this.target.position - this.transform.position, this.target.up);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, desiredRotation, Time.deltaTime * this.rotationDamping);
    }

    private void UpdatePlayerTransform(Vector3 movementDirection)
    {                
        RaycastHit hitInfo;

        if (GetRaycastDownAtNewPosition(movementDirection, out hitInfo))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, float.PositiveInfinity);

            transform.rotation = finalRotation;
            transform.position = hitInfo.point + hitInfo.normal * .5f;
        }
    }

    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit hitInfo)
    {
        Vector3 newPosition = transform.position;
        Ray ray = new Ray(transform.position + movementDirection * speed, -transform.up);        

        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity))
        {
            return true;
        }

        return false;
    }
}

