using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private bool isOpen = false;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator is missing on this object.");
        }

        if (isOpen)
        {
            animator.SetTrigger("OpenDoor");
        }
        else
        {
            animator.SetTrigger("CloseDoor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
