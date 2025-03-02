using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    private float dirX;
    private float dirY;
    private float playerSpeed = 6f;
    private float maxSpeed = 10f;
    private float minSpeed = 0.2f;
    private float jumpHeight = 9f;
    public static bool rotation = true;
    private bool flipped = false;
    private float direction;

    [SerializeField] private Animator anim;
    private bool isJumping;
    private bool isJumpEnd;
    private bool isJumpStart;

    [SerializeField] GameObject mCamera;
    [SerializeField] LayerMask jumpReset;
    [SerializeField] LayerMask slippery;
    [SerializeField] SpriteRenderer sRend;

    private float isDashingTimer = 0.1f;
    private float isDashingCounter = 0.1f;
    [SerializeField] float dashingPower = 5f;
    [SerializeField] float dashingPower3d = 5f;
    private float dashCooldownTimer = 0.75f;
    private float dashCooldownCounter = 0.75f;

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
    private bool isSlippery = false;
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
        playerHeight = GetComponent<BoxCollider>().bounds.extents.y * transform.localScale.y;
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
        
        if (transform.position.y < -20)
        {
            PlayerDead();
        }

        if (LeverScript.leverActive == false)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            sRend.gameObject.SetActive(true);

            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Jump");
            
            if (dirX < 0)
            {
                sRend.flipX = true;
                flipped = true;
            }
            else if (dirX > 0)
            {
                sRend.flipX = false;
                flipped = false;
            }

            if (Mathf.Abs(dirX) != 0)
            {
                anim.SetBool("Running", true);
            }
            else
            {
                anim.SetBool("Running", false);
            }

            if (isGrounded || isSlippery && isJumpStart == true)
            {
                isJumping = false;
                isJumpStart = false;
                isJumpEnd = true;
            }
            if ((!isGrounded || !isSlippery) && Mathf.Abs(dirY) > 0)
            {
                isJumping = true;
            }
            
            if (isJumping)
            {
                anim.SetBool("Jumping", true);
            }
            else
            {
                anim.SetBool("Jumping", false);
            }
            if (isJumpEnd)
            {
                anim.SetBool("JumpEnd", true);
                isJumpEnd = false;
            }
            else
            {
                anim.SetBool("JumpEnd", false);
            }

            if (flipped == true)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            if ((isGrounded || isSlippery) && rb.linearVelocity.y == 0)
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
                isJumpStart = true;
            }

            if (dirY > 0 && (rb.linearVelocity.y > 0f))
            {
                groundCheckTimer = 0f;
            }

            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, jumpReset);
            isSlippery = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, slippery);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            
            if (rotation == false)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                rotation = true;
            }
            if (transform.rotation.y != 0 || transform.rotation.y != 180)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if (ScenesManager.instance.getCurrentScene() >= 3)
            {
                if (Input.GetKeyDown(KeyCode.F) && isDashingTimer <= 0)
                {
                    rb.linearVelocity = new Vector3(Mathf.Abs(rb.linearVelocity.x) * (direction * dashingPower), 0, 0);
                    isDashingTimer = isDashingCounter;
                }
                else if (isSlippery && Mathf.Abs(rb.linearVelocity.x) <= playerSpeed + 2f && Mathf.Abs(rb.linearVelocity.x) >= minSpeed && dirX != 0)
                {
                    rb.AddForce(new Vector3(Mathf.Abs(rb.linearVelocity.x) * dirX * 1.2f, rb.linearVelocity.y)); //Maybe delete rb.linearVelocity.y
                }
                else if (isDashingTimer <= 0 && !isGrounded)
                {
                    if (Mathf.Abs(rb.linearVelocity.x) < playerSpeed)
                    {
                        rb.linearVelocity = new Vector3((dirX * playerSpeed), rb.linearVelocity.y);
                    }
                    else if (Mathf.Abs(rb.linearVelocity.x) >= maxSpeed)
                    {
                        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
                    }
                    else if (Mathf.Abs(rb.linearVelocity.x) < minSpeed)
                    {
                        rb.linearVelocity = new Vector3((dirX * playerSpeed), rb.linearVelocity.y);
                    }
                    else if (isSlippery && dirX == 0)
                    {
                        rb.AddForce(new Vector3((-rb.linearVelocity.x) * 0.1f, 0));
                    }
                    else if (!isSlippery && rb.linearVelocity.x != 0)
                    {
                        rb.AddForce(new Vector3((-rb.linearVelocity.x) * 0.1f, 0));
                    }
                }
                else if (isDashingTimer <= 0)
                {
                    rb.linearVelocity = new Vector3((dirX * playerSpeed), rb.linearVelocity.y);
                }
                else
                {
                    isDashingTimer -= Time.deltaTime;
                }
            }
            else
            {
                if (isGrounded & dirY == 0)
                {
                    rb.linearVelocity = new Vector3(dirX * playerSpeed, 0, 0);
                }
                else
                {
                    rb.linearVelocity = new Vector3(dirX * playerSpeed, rb.linearVelocity.y, 0);
                }
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
        if (ScenesManager.instance.getCurrentScene() >= 3)
        {
            if (Input.GetKey(KeyCode.F) && isDashingTimer <= 0 && dashCooldownTimer <= 0)
            {
                movement = (transform.right * moveHorizontal + transform.forward * moveForward * dashingPower3d);
                targetVelocity = movement * MoveSpeed;
                velocity = rb.linearVelocity;
                velocity.x = targetVelocity.x;
                velocity.z = targetVelocity.z;
                rb.linearVelocity = velocity;
                isDashingTimer = isDashingCounter;
                dashCooldownTimer = dashCooldownCounter;
            }
            else if (isDashingTimer <= 0)
            {
                rb.linearVelocity = velocity;
            }
            else
            {
                isDashingTimer -= Time.deltaTime;
            }
            if (dashCooldownTimer >= 0)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
        }
        else
        {
            rb.linearVelocity = velocity;
        }

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
        rb.linearVelocity = Vector3.zero;
        transform.position = new Vector3(0, 1.5f, 0);
        CameraScript.rotation = false;
        rotation = false;
        SpikeScript.playerDead = false;
        LeverScript.resetLevers();
        DoorControllerScript.keyReset();
    }
}
