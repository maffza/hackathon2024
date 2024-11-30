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

    private Vector2 currentVelocity; // Przechowuje pr�dko�� dla SmoothDamp

    void Update() {
        // Rozpocznij odtwarzanie po wci�ni�ciu klawisza K
        if (Input.GetKeyDown(KeyCode.K)) {
            // Je�li duch ju� istnieje, usu� go
            if (ghostObject != null) {
                Destroy(ghostObject);
            }

            // Pobierz list� zapisanych pozycji
            recordedPositions = positionTracker.getPositionsList();

            if (recordedPositions == null || recordedPositions.Count == 0) {
                Debug.LogError("Brak zapisanych pozycji do odtworzenia!");
                return;
            }

            // Utw�rz ducha w pierwszej pozycji
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
                Debug.LogError("Czas ruchu wynosi 0. Sprawd� averageSpeed.");
                return;
            }

            // Aktualizacja czasu sp�dzonego w bie��cej pozycji
            timeSpentAtCurrentPosition += Time.deltaTime;

            // Przej�cie do nast�pnego punktu, je�li czas si� sko�czy�
            if (timeSpentAtCurrentPosition >= timeToMove) {
                currentIndex++;
                timeSpentAtCurrentPosition = 0f;
                return;
            }

            if (currentIndex < recordedPositions.Count - 1) {
                Vector2 targetPosition = recordedPositions[currentIndex + 1].position;

                // Stabilizacja interpolacji: u�yj SmoothDamp
                Vector2 interpolatedPosition = Vector2.SmoothDamp(
                    new Vector2(ghostObject.transform.position.x, ghostObject.transform.position.y),
                    targetPosition,
                    ref currentVelocity,
                    timeToMove
                );

                // Optymalizacja: Je�li ruch jest pionowy, ignoruj zmiany w osi X
                if (Mathf.Abs(startPosition.x - endPosition.x) < 0.01f) {
                    ghostObject.transform.position = new Vector3(startPosition.x, interpolatedPosition.y, 0);
                } else {
                    ghostObject.transform.position = new Vector3(interpolatedPosition.x, interpolatedPosition.y, 0);
                }
            }
        }
    }
}
