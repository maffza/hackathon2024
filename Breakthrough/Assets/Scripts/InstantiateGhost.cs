using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPlayerMovement : MonoBehaviour {
    public PlayerPositionTracker positionTracker;
    public GameObject ghostPrefab;
    public float averageSpeed = 5f;

    private List<(Vector2 position, float duration)> recordedPositions;
    private int currentIndex = 0;
    private float timeSpentAtCurrentPosition = 0f;

    private GameObject ghostObject;
    private bool isReplaying = false;

    private Vector2 currentVelocity; // Przechowuje prêdkoœæ dla SmoothDamp

    void Update() {
        // Rozpocznij odtwarzanie po wciœniêciu klawisza K
        if (Input.GetKeyDown(KeyCode.K)) {
            // Jeœli duch ju¿ istnieje, usuñ go
            if (ghostObject != null) {
                Destroy(ghostObject);
            }

            // Pobierz listê zapisanych pozycji
            recordedPositions = positionTracker.getPositionsList();

            if (recordedPositions == null || recordedPositions.Count == 0) {
                Debug.LogError("Brak zapisanych pozycji do odtworzenia!");
                return;
            }

            // Utwórz ducha w pierwszej pozycji
            ghostObject = Instantiate(ghostPrefab, recordedPositions[0].position, Quaternion.identity);

            isReplaying = true;
            ghostObject.SetActive(true);
            currentIndex = 0;
            timeSpentAtCurrentPosition = 0f;
        }

        // Odtwarzanie ruchu ducha
        if (isReplaying) {
            if (currentIndex >= recordedPositions.Count - 1) return;

            Vector2 startPosition = recordedPositions[currentIndex].position;
            Vector2 endPosition = recordedPositions[currentIndex + 1].position;

            // Oblicz dystans i czas ruchu
            float distance = Vector2.Distance(startPosition, endPosition);
            if (distance == 0f) {
                currentIndex++;
                return;
            }

            float timeToMove = distance / averageSpeed;
            if (timeToMove <= 0f) {
                Debug.LogError("Czas ruchu wynosi 0. SprawdŸ averageSpeed.");
                return;
            }

            // Aktualizacja czasu spêdzonego w bie¿¹cej pozycji
            timeSpentAtCurrentPosition += Time.deltaTime;

            // Przejœcie do nastêpnego punktu, jeœli czas siê skoñczy³
            if (timeSpentAtCurrentPosition >= timeToMove) {
                currentIndex++;
                timeSpentAtCurrentPosition = 0f;
                return;
            }

            if (currentIndex < recordedPositions.Count - 1) {
                Vector2 targetPosition = recordedPositions[currentIndex + 1].position;

                // Stabilizacja interpolacji: u¿yj SmoothDamp
                Vector2 interpolatedPosition = Vector2.SmoothDamp(
                    new Vector2(ghostObject.transform.position.x, ghostObject.transform.position.y),
                    targetPosition,
                    ref currentVelocity,
                    timeToMove
                );

                // Optymalizacja: Jeœli ruch jest pionowy, ignoruj zmiany w osi X
                if (Mathf.Abs(startPosition.x - endPosition.x) < 0.01f) {
                    ghostObject.transform.position = new Vector3(startPosition.x, interpolatedPosition.y, 0);
                } else {
                    ghostObject.transform.position = new Vector3(interpolatedPosition.x, interpolatedPosition.y, 0);
                }
            }
        }
    }
}
