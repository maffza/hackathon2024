using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Sprite offSprite;

    [SerializeField]
    private Sprite onSprite;

    private SpriteRenderer sprRenderer;


    private Vector2 startPos;
    [SerializeField]
    private Vector2 endPos;

    [SerializeField]
    private float moveTimeSec;
    private float elapsedTime = 0;

    public bool isOn = false;

    void Start()
    {
        startPos = transform.position;
        sprRenderer = GetComponent<SpriteRenderer>();
        if (sprRenderer == null)
            Debug.LogWarning("Sprite renderer not set!", this);

        sprRenderer.sprite = offSprite;
    }

    void FixedUpdate()
    {
        if (isOn && elapsedTime < moveTimeSec)
        {
            elapsedTime = Mathf.Clamp(elapsedTime + Time.fixedDeltaTime, 0, moveTimeSec);
            float t = Mathf.Clamp01(elapsedTime / moveTimeSec);
            Vector2 currentPosition = Vector2.Lerp(startPos, endPos, t);
            transform.position = currentPosition;
        }
        else if (!isOn && elapsedTime > 0)
        {
            elapsedTime = Mathf.Clamp(elapsedTime - Time.fixedDeltaTime, 0, moveTimeSec);
            float t = Mathf.Clamp01((moveTimeSec - elapsedTime) / moveTimeSec);
            Vector2 currentPosition = Vector2.Lerp(endPos, startPos, t);
            transform.position = currentPosition;
        }
    }


}
