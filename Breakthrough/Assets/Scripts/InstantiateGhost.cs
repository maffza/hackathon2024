using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPlayerMovement : MonoBehaviour
{
    public PlayerPositionTracker positionTracker;
    public GameObject ghostPrefab;
    public float averageSpeed = 5f;

    private List<(Vector2 position, float duration)> recordedPositions;
    private int currentIndex = 0;
    private float timeSpentAtCurrentPosition = 0f;

    private GameObject ghostObject;
    private bool isReplaying = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            recordedPositions = positionTracker.getPositionsList();

            ghostObject = Instantiate(ghostPrefab, recordedPositions[0].position, Quaternion.identity);

            isReplaying = true;
            ghostObject.SetActive(true);
            currentIndex = 0;
            timeSpentAtCurrentPosition = 0f;
        }

        if (isReplaying)
        {
            if (currentIndex >= recordedPositions.Count - 1) return;

            Vector2 startPosition = recordedPositions[currentIndex].position;
            Vector2 endPosition = recordedPositions[currentIndex + 1].position;

            float distance = Vector2.Distance(startPosition, endPosition);

            float timeToMove = distance / averageSpeed;

            timeSpentAtCurrentPosition += Time.deltaTime;

            if (timeSpentAtCurrentPosition >= timeToMove)
            {
                currentIndex++;
                timeSpentAtCurrentPosition = 0f;
            }

            if (currentIndex < recordedPositions.Count - 1)
            {
                Vector2 targetPosition = recordedPositions[currentIndex + 1].position;
                ghostObject.transform.position = Vector2.Lerp(ghostObject.transform.position, targetPosition, timeSpentAtCurrentPosition / timeToMove);
            }
        }
    }

}
