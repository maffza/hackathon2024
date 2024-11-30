using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float gravity = 2f;

    private Rigidbody2D rb;
    private Collider2D floorDetectCol;

    [SerializeField]
    private float jumpCollideWindowSec = 0.2f;
    private float jumpTimer = 0f;

    [SerializeField]
    private float jumpCooldownTime = 0.5f;
    private float jumpCooldown = 0f;

    [SerializeField]
    private float coyoteBaseTime = 0.2f;
    private float coyoteTimer = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on this object.");
        }

        floorDetectCol = transform.GetComponentInChildren<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        jumpTimer = Mathf.Clamp(jumpTimer - Time.fixedDeltaTime, 0, jumpCollideWindowSec);
        coyoteTimer = Mathf.Clamp(coyoteTimer - Time.fixedDeltaTime, 0, coyoteBaseTime);
        jumpCooldown = Mathf.Clamp(jumpCooldown - Time.fixedDeltaTime, 0, jumpCooldownTime);

        // Debug.Log(coyoteTimer);

        HandleMovement(); 
        Jump();

        checkInstantFall();
    }

    private void Update()
    {
        if (JumpInputPress())
        {
            jumpTimer = jumpCollideWindowSec;
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }


        Vector2 movement = new Vector2(horizontalInput, 0).normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void checkInstantFall()
    {
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Space)) {
            if(rb.linearVelocityY > 0.001f)
            {
                rb.linearVelocityY = 0f;
            }
        }
    }

    private void Jump()
    {
        if (coyoteTimer > 0 && JumpInputHold() && jumpTimer > 0 && jumpCooldown <= 0)
        {
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            coyoteTimer = 0;
            jumpCooldown = jumpCooldownTime;
        }
    }

    private bool JumpInputPress()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            return true;
        else
            return false;
    }

    private bool JumpInputHold()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            return true;
        else
            return false;
    }

    public void FloorDetected()
    {
        coyoteTimer = coyoteBaseTime;
    }

    public Vector3 GetPositon()
    {
        return transform.position;
    }
}
