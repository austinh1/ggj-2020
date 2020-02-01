using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyFPSWalker : MonoBehaviour
{
    public int walkspeed;
    public int jumpHeight;
    public float distToGround;
    public float maxVelocityChange;

    public RaycastHit hit;
    public Vector3 castPos; //ray start

    public GameObject planet; //set in inspector
    public GameObject graphics;
    public GameObject cameraPivot;
    //public GameObject camera;

    void Start()
    {
        // Get the distance to ground
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        UpdateGraphics(targetVelocity);
        
        if (IsGrounded())
        {
            // Calculate how fast we should be moving
            
            targetVelocity = Camera.main.transform.TransformDirection(targetVelocity);
            targetVelocity *= walkspeed;

            // Apply a force that attempts to reach our target velocity
            var velocity = GetComponent<Rigidbody>().velocity;
            var velocityChange = (targetVelocity - velocity);
            Debug.DrawRay(transform.position, velocityChange * 5, Color.magenta);
            Debug.DrawRay(transform.position, velocity * 5, Color.green);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

            // Jumping (this is borked atm but idc)
            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, -CalculateJumpVerticalSpeed(), velocity.z);
            }
        }
    }

    private void UpdateGraphics(Vector3 targetVelocity)
    {
        if (targetVelocity.x > 0)
        {
            graphics.transform.localRotation = cameraPivot.transform.localRotation * Quaternion.Euler(graphics.transform.localRotation.x, 90, graphics.transform.localRotation.z);
        }
        if (targetVelocity.x < 0)
        {
            graphics.transform.localRotation =  cameraPivot.transform.localRotation * Quaternion.Euler(0, -90, 0);
        }

        if (targetVelocity.z > 0)
        {
            graphics.transform.localRotation = cameraPivot.transform.localRotation * Quaternion.Euler(0, 0, 0);
        }
        if (targetVelocity.z < 0)
        {
            graphics.transform.localRotation =  cameraPivot.transform.localRotation * Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {
        // Orient player upright
        var down = (planet.transform.position - transform.position).normalized;
        var forward = Vector3.Cross(transform.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
    }

    bool IsGrounded()
    {
        // Check if player is close to ground
        Debug.DrawRay(transform.position, -transform.up * (distToGround + 0.1f), Color.red);
        return Physics.Raycast(transform.position, -transform.up, distToGround + 0.01f);
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height we deduce the upwards speed
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight);
    }

    public void OnTriggerEnter(Collider other)
    {
        Collectable obj;
        if ((obj = other.GetComponent<Collectable>()) != null)
        {
            obj.Collect(gameObject);
        }
    }
}