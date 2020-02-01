using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        //rb.velocity = movement * speed;
        
        RaycastHit hit = new RaycastHit();
        //Vector3 castPos = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
       
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.position = hit.point + transform.up*GetComponent<CapsuleCollider>().height/2;
            Vector3 forceVector = Vector3.ProjectOnPlane(movement, hit.normal);
            rb.AddForce (forceVector * speed);
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ( "Pick Up"))
        {
            other.gameObject.SetActive (false);
        }
    }
}

