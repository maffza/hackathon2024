using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPlayerMovement : MonoBehaviour {
    public GameObject ghostPrefab;
    public float averageSpeed = 15f;
    private GameObject spawnPoint;
    public Vector3 startingPosition = Vector3.zero;

    private List<List<Vector2>> ghostPaths = new List<List<Vector2>>();
    private List<GameObject> ghosts = new List<GameObject>();
    private List<Vector2> currentPath = new List<Vector2>();
    private bool isReplaying = false;
    private Vector2 currentVelocity;

    private bool canPressKillButton = true;
    private bool ghostFinishPath = false;
    private bool inStartZone = true;

    private GameObject gameManager;
    private AudioPlayer audioPlayer;

    private float killTime = 2f;
    public float killTimer { get; private set; } = 0f;
    private bool killPlayer = false;

    public bool isDying { get; private set; } = false;

    void Start () {
        spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not found");
        }

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        audioPlayer = gameManager.GetComponent<AudioPlayer>();
    }
    
    void Update() {
        killTimer -= Time.deltaTime;

        if (!isReplaying && (currentPath.Count == 0 || currentPath[currentPath.Count - 1] != (Vector2)transform.position)) {
            currentPath.Add(transform.position);
        }


        if (Input.GetKeyDown(KeyCode.K)) {
            if (canPressKillButton && !inStartZone) {

                killTimer = killTime;
                killPlayer = true;
                isDying = true;

                audioPlayer.PlaySound(3);
                canPressKillButton = false;
            }
            

        }

        if(killPlayer && killTimer <= 0)
        {
            isDying = false;
            killPlayer = false;
            KillPlayer();
        }

        UpdateGhosts();
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Spawner")) {
            inStartZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Spawner")) {
            inStartZone = false;
        }
    }

    public void KillPlayer() {
        RespawnPlayer();
        StartGhostReplay();
    }

    private void RespawnPlayer() {
        transform.position = startingPosition;
        Debug.Log("Gracz zosta� przeniesiony na pocz�tek mapy.");
    }

    private void StartGhostReplay() {
        if (currentPath.Count > 0) {
            List<Vector2> newPath = new List<Vector2>(currentPath);
            ghostPaths.Add(newPath);
            currentPath.Clear();
        }

        if (ghostPaths.Count > 0) {
            Vector2 spawnPosition = ghostPaths[ghostPaths.Count - 1][0];
            GameObject newGhost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            ghosts.Add(newGhost);

           
        }
    }
    private void UpdateGhosts() {
        for (int i = 0; i < ghosts.Count; i++) {
            GameObject ghost = ghosts[i];
            if (ghost == null) continue;

            List<Vector2> path = ghostPaths[i];
            if (path == null || path.Count == 0) {
                // Duch zakonczy� trase
                CheckCollisionGhost ccg = ghost.GetComponent<CheckCollisionGhost>();
                if (ccg != null && !ccg.iAmCollider) {
                    ccg.iAmCollider = true; 
                    Debug.Log($"Duch {i} zako�czy� tras�. Kolizje aktywne.");
                    canPressKillButton = true;
                    ghostFinishPath = true;
                }
                continue;
            }

            Vector2 currentPos = ghost.transform.position;
            Vector2 targetPos = path[0]; 

            if (Vector2.Distance(currentPos, targetPos) < 0.01f) {
                path.RemoveAt(0);
                continue;
            }

            Vector2 interpolatedPosition = Vector2.SmoothDamp(
                currentPos,
                targetPos,
                ref currentVelocity,
                averageSpeed * Time.deltaTime * 0.1f
            );

            ghost.transform.position = new Vector3(interpolatedPosition.x, interpolatedPosition.y, ghost.transform.position.z);
        }
    }

}
