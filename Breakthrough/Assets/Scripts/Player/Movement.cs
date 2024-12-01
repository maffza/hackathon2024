using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float gravity = 2f;

    private Rigidbody2D rb;

    [SerializeField]
    private float jumpCollideWindowSec = 0.2f;
    private float jumpTimer = 0f;

    [SerializeField]
    private float jumpCooldownTime = 0.5f;
    private float jumpCooldown = 0f;

    [SerializeField]
    private float coyoteBaseTime = 0.2f;
    private float coyoteTimer = 0f;

    //used for animation getter
    public float HorizontalInputData { get; private set; } = 0f;
    public bool OnGround { get; private set; } = false;

    private AudioPlayer audioPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on this object.");
        }

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        audioPlayer = gameManager.GetComponent<AudioPlayer>();

    }

    void FixedUpdate()
    {
        jumpTimer = Mathf.Clamp(jumpTimer - Time.fixedDeltaTime, 0, jumpCollideWindowSec);
        coyoteTimer = Mathf.Clamp(coyoteTimer - Time.fixedDeltaTime, 0, coyoteBaseTime);
        jumpCooldown = Mathf.Clamp(jumpCooldown - Time.fixedDeltaTime, 0, jumpCooldownTime);

        // Debug.Log(coyoteTimer);

        HandleMovement(); 
        Jump();
        CheckInstantFall();
        ApplyBounds();
    }

    private void Update()
    {
        if (JumpInputPress())
        {
            jumpTimer = jumpCollideWindowSec;
        }
    }

    private void LateUpdate()
    {
        OnGround = false;
    }

    private void HandleMovement()
    {
        float horizontalInput = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput += 1f;
        }

        HorizontalInputData = horizontalInput;

        Vector2 movement = speed * Time.deltaTime * new Vector2(horizontalInput, 0).normalized;
        transform.Translate(movement);

        if (horizontalInput != 0 && Mathf.Abs(rb.linearVelocityY) < 0.01)
        {
            audioPlayer.PlaySoundIfNotPlaying(0);
        }
    }

    private void CheckInstantFall()
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
            audioPlayer.PlaySound(1);
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
        OnGround = true;
    }

    public Vector3 GetPositon()
    {
        return transform.position;
    }

    private void ApplyBounds() 
    {
        Vector3 position = transform.position;

        if (position.x > 7.2f)
            transform.position = new Vector3(7.2f, position.y, position.z);
        else if (position.x < -7.2f)
            transform.position = new Vector3(-7.2f, position.y, position.z);

        if (position.y > 3.8f)
        {
            transform.position = new Vector3(position.x, 3.8f, position.z);
            rb.linearVelocityY = 0f;
        }
        else if (position.y < -3.8f)
        {
            transform.position = new Vector3(position.x, -3.8f, position.z);
            rb.linearVelocityY = 0f;
        }
    }

    public float GetVelocityY()
    {
        return rb.linearVelocityY;
    }
}
