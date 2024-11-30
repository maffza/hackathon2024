using UnityEngine;

public class FloorDetection : MonoBehaviour
{

    private Collider2D floorDetectCol;
    private Movement parentMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floorDetectCol = transform.GetComponent<BoxCollider2D>();
        if (floorDetectCol == null)
            Debug.LogWarning("Collider not set", this);

        parentMovement = transform.parent.GetComponent<Movement>();
        if (parentMovement == null)
            Debug.LogWarning("Parent not present", this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
