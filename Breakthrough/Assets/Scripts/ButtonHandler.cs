using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject targetGameObject; // Nazwa zmienna bardziej opisowa.
    [SerializeField]
    private bool door = true;

    private DoorHandler doorHandler;

    private bool debug = false;

    [SerializeField]
    private Sprite offSprite;
    [SerializeField]
    private Sprite onSprite;

    private SpriteRenderer sprRenderer;


    void Start()
    {

        sprRenderer = GetComponent<SpriteRenderer>();
        if (sprRenderer == null)
        {
            Debug.LogWarning("Renderer not set", this);
        }



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

        sprRenderer.sprite = offSprite;
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

        sprRenderer.sprite = onSprite;
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

        sprRenderer.sprite = offSprite;
    }
}
