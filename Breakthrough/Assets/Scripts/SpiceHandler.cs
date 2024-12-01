using UnityEngine;

public class SpiceHandler : MonoBehaviour
{
    private GameObject player;
    private ReplayPlayerMovement movementScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is missing.");
        }
        
        movementScript = player.GetComponent<ReplayPlayerMovement>();
        if (movementScript == null)
        {
            Debug.LogError("Movement script is missing.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movementScript.KillPlayer();
        }
    }
}
