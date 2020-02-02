using UnityEngine;

public class RigidBodyFPSWalker : MonoBehaviour
{
    public int walkspeed;
    public int jumpHeight;
    public float distToGround;
    public float maxVelocityChange;
    public Animator animator;

    public RaycastHit hit;
    public Vector3 castPos; //ray start

    private GameObject planet; //set in inspector
    public GameObject graphics;
    public GameObject cameraPivot;

    public GameObject pauseMenu;

    private static readonly int Running = Animator.StringToHash("Running");

    //public GameObject camera;
    private bool CollectedPlanet { get; set; }

    void Start()
    {
        planet = GameObject.FindWithTag("Planet");
        // Get the distance to ground
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    public void CollectPlanet()
    {
        CollectedPlanet = true;
    }

    void FixedUpdate()
    {
        var targetVelocity = Vector3.zero;
        if (!GetComponent<PlayerScore>().GameEnded)
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        animator.SetBool(Running, targetVelocity != Vector3.zero);
        
        UpdateGraphics(targetVelocity);
        
        if (IsGrounded())
        {
            // Calculate how fast we should be moving
            
            targetVelocity = cameraPivot.transform.TransformDirection(targetVelocity);
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
            graphics.transform.localRotation = Quaternion.Lerp(graphics.transform.localRotation, cameraPivot.transform.localRotation * Quaternion.Euler(0, 90, 0), Time.deltaTime*3);
        }
        if (targetVelocity.x < 0)
        {
            graphics.transform.localRotation =  Quaternion.Lerp(graphics.transform.localRotation, cameraPivot.transform.localRotation * Quaternion.Euler(0, -90, 0), Time.deltaTime*3);
        }

        if (targetVelocity.z > 0)
        {
            graphics.transform.localRotation = Quaternion.Lerp(graphics.transform.localRotation, cameraPivot.transform.localRotation * Quaternion.Euler(0, 0, 0), Time.deltaTime*3);
        }
        if (targetVelocity.z < 0)
        {
            graphics.transform.localRotation =  Quaternion.Lerp(graphics.transform.localRotation, cameraPivot.transform.localRotation * Quaternion.Euler(0, 180, 0), Time.deltaTime*3);
        }
    }

    void Update()
    {
        if (!CollectedPlanet)
        {
            // Orient player upright
            var down = (planet.transform.position - transform.position).normalized;
            var forward = Vector3.Cross(transform.right, down);
            transform.rotation = Quaternion.LookRotation(-forward, -down);    
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);

                Time.timeScale = 0;
            }
            else if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
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
}