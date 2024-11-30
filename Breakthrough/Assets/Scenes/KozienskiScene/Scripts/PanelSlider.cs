using UnityEngine;

public class PanelSlider : MonoBehaviour {
    public RectTransform rightPanel; 
    public float slideSpeed = 75f;  
    public Vector2 targetPosition;  

    private Vector2 hiddenPosition; 
    private bool isPanelVisible = false; 

    private void Start() {
        
        hiddenPosition = rightPanel.anchoredPosition;
    }

    public void TogglePanel() {
        StopAllCoroutines();
        if (isPanelVisible) {
            StartCoroutine(SlideTo(hiddenPosition)); // (ukryj panel)
        } else {
            StartCoroutine(SlideTo(targetPosition)); //  (poka¿ panel)
        }
        isPanelVisible = !isPanelVisible;
    }

    private System.Collections.IEnumerator SlideTo(Vector2 target) {
        while (Vector2.Distance(rightPanel.anchoredPosition, target) > 0.1f) {
            rightPanel.anchoredPosition = Vector2.Lerp(
                rightPanel.anchoredPosition,
                target,
                Time.deltaTime * slideSpeed
            );
            yield return null;
        }
        rightPanel.anchoredPosition = target; 
    }
}
