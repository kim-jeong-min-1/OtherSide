using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType1
{
    Rotate = 1,
    Move
}

public class Interaction1 : MonoBehaviour
{
    public Vector3 interactionAngle
    {
        get
        {
            var angle = transform.eulerAngles;
            angle.x = (angle.x > 180) ? angle.x - 360 : angle.x;
            angle.y = (angle.y > 180) ? angle.y - 360 : angle.y;
            angle.z = (angle.z > 180) ? angle.z - 360 : angle.z;

            return angle;
        }
    }

    public Vector3 interactionPosition => transform.position;

    public Vector3[] activeValues;
    public int RotateDirection;

    public bool isInteract;
    public InteractType interactType;

    protected virtual void Update()
    {
        if (ActiveChecking()) isInteract = true;
        else isInteract = false;
    }

    protected bool ActiveChecking()
    {
        if (interactType == InteractType.Rotate)
            for (int i = 0; i < activeValues.Length; i++)
            {
                if (interactionAngle == activeValues[i])
                {
                    return true;
                }
            }
        else if (interactType == InteractType.Move)
            for (int i = 0; i < activeValues.Length; i++)
            {
                if (interactionPosition == activeValues[i])
                {
                    return true;
                }
            }
        return false;
    }
}
