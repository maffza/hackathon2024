using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlatformScript : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.CompareTag("PlayerCollider"))
        {
            other.transform.parent.GetComponent<Movement>().FloorDetected();
        }
    }
}
