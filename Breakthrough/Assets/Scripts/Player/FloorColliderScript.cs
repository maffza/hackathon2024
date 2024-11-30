using UnityEngine;

public class FloorColliderScript : MonoBehaviour
{
    private Vector3 offset;
    Movement movementScript;
    private void Start()
    {
        movementScript = transform.parent.GetComponent<Movement>();
        if (movementScript == null)
            Debug.LogWarning("movement script not set", this);

        offset = transform.position - movementScript.GetPositon();
    }

    private void Update()
    {
        transform.position = offset + movementScript.GetPositon();
    }
}
