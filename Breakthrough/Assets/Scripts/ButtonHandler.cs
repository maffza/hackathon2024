using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject targetGameObject; // Nazwa zmienna bardziej opisowa.
    [SerializeField]
    private bool door = true;

    private DoorHandler doorHandler;

    private bool debug = false;

    void Start()
    {
        if (targetGameObject == null)
        {
            Debug.LogError("Target GameObject is not assigned in the Inspector.");
            return; // Kończymy dalsze wykonywanie, bo nie mamy obiektu.
        }

        if (door)
        {
            doorHandler = targetGameObject.GetComponent<DoorHandler>();
            if (doorHandler == null)
            {
                Debug.LogError("DoorHandler component is missing on the assigned GameObject.");
            }
            else if (debug)
            {
                Debug.Log("DoorHandler component found.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (debug)
                Debug.Log("Player entered the trigger zone!");

            if (door && doorHandler != null)
            {
                doorHandler.OpenDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (debug)
                Debug.Log("Player exited the trigger zone!");

            if (door && doorHandler != null)
            {
                doorHandler.CloseDoor();
            }
        }
    }
}
