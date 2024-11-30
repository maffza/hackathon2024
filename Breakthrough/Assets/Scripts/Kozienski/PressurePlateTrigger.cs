using UnityEngine;

public class PressurePlateTriggered : MonoBehaviour {

    [SerializeField]
    private bool plateTriggered = false; 

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Gracz stoi na p�ytce!");
            plateTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Gracz zszed� z p�ytki!");
            plateTriggered = false;
        }
    }

    public bool IsPlateTriggered() {
        return plateTriggered;
    }
}
