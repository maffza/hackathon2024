using UnityEngine;

public class moveMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    private bool isJumping = false; 
    private Rigidbody2D rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on this object.");
        }
    }

    void Update()
    {
        HandleMovement(); 
        if (Input.GetKeyDown(KeyCode.Space) || 
        Input.GetKeyDown(KeyCode.UpArrow) || 
        Input.GetKeyDown(KeyCode.W)) 
        {
            Jump();
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

    private void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isJumping = false;
        }
    }
}
