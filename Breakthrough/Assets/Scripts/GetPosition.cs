using UnityEngine;

public class GetPosition : MonoBehaviour
{
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ReplayPlayerMovement>().startingPosition = gameObject.transform.position;
    }
}
