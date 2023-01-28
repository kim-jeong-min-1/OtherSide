using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Transform nodePoint;
    public bool isActive = true;
}

public enum WalkableType
{
    Basic,
    TelePort,
    ClearPortal
}

public class Walkable : MonoBehaviour
{
    readonly private float walkPointOffset = 0.5f;

    public WalkableType type;
    public List<Node> neighborNode = new List<Node>();
    public bool isStair = false;
    public Vector3 rotateDirection = Vector3.zero;

    public Vector3 GetWalkPoint()
    {
        return transform.position + transform.up * walkPointOffset; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(GetWalkPoint(), 0.1f);
    }
}
