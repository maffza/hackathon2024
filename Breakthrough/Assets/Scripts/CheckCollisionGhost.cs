using UnityEngine;

public class CheckCollisionGhost : MonoBehaviour {
    private GameObject player;
    public bool iAmCollider = false;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update() {
        if (iAmCollider) {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        player.gameObject.GetComponent<Movement>().FloorDetected();
    }

}
