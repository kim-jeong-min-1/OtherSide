using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    Rotate = 1,
    Move
}

public class Interaction : MonoBehaviour
{
    public Vector3 interactionAngle => transform.eulerAngles;
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
        //스위치식으로 수정하기
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
