using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private Animator animator;
    private GameObject gameManager;

    [SerializeField]
    private bool isOpen = false;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.LogError("GameManager is missing.");
        }
        
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator is missing on this object.");
        }

        if (isOpen)
        {
            animator.SetTrigger("OpenDoor");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().LoadNextLevel();
        }
    }

    public void OpenDoor()
    {
        // Animation handler
        animator.SetTrigger("OpenDoor");

        // Setting state
        isOpen = true;
    }

    public void CloseDoor()
    {
        // Animation handler
        animator.SetTrigger("CloseDoor");

        // Setting state
        isOpen = false;
    }
}
