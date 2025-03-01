using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    private float dirX;
    private float dirY;
    private float playerSpeed = 6f;
    private float jumpHeight = 8f;
    public static bool rotation = true;

    [SerializeField] GameObject mCamera;
    [SerializeField] LayerMask jumpReset;
    [SerializeField] SpriteRenderer sRend;

    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 8f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    public float jumpForce = 15f;
    public float fallMultiplier = 1f; // Multiplies gravity when falling down
    public float ascendMultiplier = 20f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.2f;
    private float playerHeight;
    private float raycastDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.15f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpikeScript.playerDead == true)
        {
            PlayerDead();
        }

        if (transform.position.y < -5)
        {
            PlayerDead();
        }

        if (LeverScript.leverActive == false)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            sRend.gameObject.SetActive(true);

            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Jump");
            
            if (isGrounded & dirY == 0)
            {
                rb.linearVelocity = new Vector3(dirX * playerSpeed, 0, 0);
            }
            else
            {
                rb.linearVelocity = new Vector3(dirX * playerSpeed, rb.linearVelocity.y, 0);
            }
            
            if (dirX < 0)
            {
                sRend.flipX = true;
            }
            else if (dirX > 0)
            {
                sRend.flipX = false;
            }

            if ((isGrounded) && rb.linearVelocity.y == 0)
            {
                groundCheckTimer = groundCheckDelay;
            }
            else
            {
                groundCheckTimer -= Time.deltaTime;
            }

            if (dirY > 0 && groundCheckTimer >= 0f)
            {
                Jump2D();
            }

            if (dirY > 0 && (rb.linearVelocity.y > 0f))
            {
                groundCheckTimer = 0f;
            }

            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, jumpReset);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if (rotation == false)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                rotation = true;
            }
        }
        else
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveForward = Input.GetAxisRaw("Vertical");

            sRend.gameObject.SetActive(false);

            if (mCamera.transform.rotation.y != transform.rotation.y)
            {
                transform.rotation = new Quaternion(0, mCamera.transform.rotation.y, 0, mCamera.transform.rotation.w);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }

            // Checking when we're on the ground and keeping track of our ground check delay
            if (!isGrounded && groundCheckTimer <= 0f)
            {
                Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
                isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, jumpReset);
            }
            else
            {
                groundCheckTimer -= Time.deltaTime;
            }
        }

}


    void FixedUpdate()
    {
        if (LeverScript.leverActive == true)
        {
            MovePlayer();
            ApplyJumpPhysics();
        }
    }

    void MovePlayer()
    {

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Initial burst for the jump
    }

    void Jump2D()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.linearVelocity.y > 0)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

    public void PlayerDead()
    {
        transform.position = new Vector3(0, 1.5f, 0);
        SpikeScript.playerDead = false;
        LeverScript.resetLevers();
        KeyScript.keyReset();
    }
}
