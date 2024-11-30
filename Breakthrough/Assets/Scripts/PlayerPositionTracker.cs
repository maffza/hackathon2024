using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionTracker : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();

    void Update()
    {
        positions.Add(transform.position);
    }

    public Vector3 GetLastPosition()
    {
        if (positions.Count > 0)
        {
            return positions[positions.Count - 1];
        }
        return Vector3.zero;
    }

    public Vector3 GetPositionAt(int index)
    {
        if (index >= 0 && index < positions.Count)
        {
            return positions[index];
        }
        return Vector3.zero;
    }
}
