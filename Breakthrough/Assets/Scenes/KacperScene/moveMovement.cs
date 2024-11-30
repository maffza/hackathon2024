using UnityEngine;

public class moveMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
