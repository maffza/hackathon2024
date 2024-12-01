using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPlayerMovement : MonoBehaviour {
    public GameObject ghostPrefab;
    public float averageSpeed = 15f;

    public Vector3 startingPosition = Vector3.zero;

    private List<List<Vector2>> ghostPaths = new List<List<Vector2>>();
    private List<GameObject> ghosts = new List<GameObject>();
    private List<Vector2> currentPath = new List<Vector2>();
    private bool isReplaying = false;
    private Vector2 currentVelocity;

    void Update() {

        if (!isReplaying && (currentPath.Count == 0 || currentPath[currentPath.Count - 1] != (Vector2)transform.position)) {
            currentPath.Add(transform.position);
        }


        if (Input.GetKeyDown(KeyCode.K)) {
            KillPlayer();

        }

        UpdateGhosts();
    }

    public void KillPlayer() {
        RespawnPlayer();
        StartGhostReplay();
    }

    private void RespawnPlayer() {
        transform.position = startingPosition;
        Debug.Log("Gracz zosta³ przeniesiony na pocz¹tek mapy.");
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
                // Duch zakonczy³ trase
                CheckCollisionGhost ccg = ghost.GetComponent<CheckCollisionGhost>();
                if (ccg != null && !ccg.iAmCollider) {
                    ccg.iAmCollider = true; 
                    Debug.Log($"Duch {i} zakoñczy³ trasê. Kolizje aktywne.");
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
                averageSpeed * Time.deltaTime
            );

            ghost.transform.position = new Vector3(interpolatedPosition.x, interpolatedPosition.y, ghost.transform.position.z);
        }
    }

}
