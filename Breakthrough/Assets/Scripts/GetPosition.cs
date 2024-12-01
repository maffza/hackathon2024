using UnityEngine;

public class GetPosition : MonoBehaviour
{
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ReplayPlayerMovement>().startingPosition = gameObject.transform.position;
    }
}
