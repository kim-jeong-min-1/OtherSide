using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public enum Axisrotation
{
    X, Y, Z
}

public class Turn_Info : MonoBehaviour
{
    [SerializeField] Axisrotation axisrotation;
    [SerializeField] private float Rotation = -90;
    [SerializeField] private float RotationPower = 90;
    [SerializeField] private int rotationdirection;
    public bool isTurn;

    private void FixedUpdate()
    {
        if (Rotation % 90 == 0)
        {
            isTurn = false;
        }
        else
        {
            isTurn = true;
        }
    }

    public void Turn()
    {
        switch (axisrotation)
        {
            case Axisrotation.X:
                gameObject.transform.DORotate(new Vector3(rotationdirection * Rotation, 0, 0)
                    , 1, RotateMode.Fast);

                Rotation -= RotationPower;

                if (gameObject.transform.rotation.x % 90 == 0)
                {
                    Turn();
                }
                break;

            case Axisrotation.Y:
                gameObject.transform.DORotate(new Vector3(0, rotationdirection * Rotation, 0)
                    , 1, RotateMode.Fast);

                Rotation -= RotationPower;

                if (gameObject.transform.rotation.y % 90 == 0)
                {
                    Turn();
                }
                break;

            case Axisrotation.Z:
                gameObject.transform.DORotate(new Vector3(0, 0, rotationdirection * Rotation)
                    , 1, RotateMode.Fast);

                Rotation -= RotationPower;                                 

                if (gameObject.transform.rotation.z % 90 == 0)
                {
                    Turn();
                }
                break;
        }
    }
}
