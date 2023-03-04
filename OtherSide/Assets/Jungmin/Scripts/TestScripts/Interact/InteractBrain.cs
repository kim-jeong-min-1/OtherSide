using Jungmin;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameManager = Jungmin.GameManager;

public class InteractBrain : MonoBehaviour
{
    [SerializeField] float rotateValue;

    public List<Interaction> interactions;
    private bool isActive;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit))
            {
                if (Hit.collider.GetComponent<InteractBrain>() != null)
                {
                    var currentStage = GameManager.Instance.currentStage;
                    if (currentStage.player1.isWalking) currentStage.player1.StopWalking();
                    if (currentStage.player2.isWalking) currentStage.player2.StopWalking();

                    //currentStage.player1.transform.SetParent(currentStage.player1.targetNode);
                    //currentStage.player2.transform.SetParent(currentStage.player2.targetNode);

                    isActive = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isActive = false;
            RoundToRotateValue();
        }

        if (isActive)
        {
            RotateToInteract();
        }
    }

    private void RotateToInteract()
    {
        transform.Rotate(0f, 0f, Input.GetAxisRaw("Mouse Y") * rotateValue, Space.World);

        for (int i = 0; i < interactions.Count; i++)
        {
            interactions[i].transform.Rotate
                (0f, (interactions[i].RotateDirection * Input.GetAxisRaw("Mouse Y")) * rotateValue, 0f, Space.World);
        }
    }

    private void RoundToRotateValue()
    {
        for (int i = 0; i < interactions.Count; i++)
        {
            var angle = interactions[i].transform.eulerAngles;

            angle.x = RoundToRightAngle(angle.x);
            angle.y = RoundToRightAngle(angle.y);
            angle.z = RoundToRightAngle(angle.z);

            interactions[i].transform.eulerAngles = angle;
        }
    }

    private int RoundToRightAngle(float angle)
    {
        var abs = (angle >= 0) ? 1 : -1;

        angle = RoundToInt(Mathf.Abs(angle) * 0.1f) * 10;
        int angleMultiply = (int)angle / 90;
        int anglePercent = (int)angle % 90;

        if (anglePercent >= 45)
        {
            angleMultiply++;
        }

        return (90 * angleMultiply) * abs;
    }

    private int RoundToInt(float value)
    {
        var round = (int)(value * 10) % 10;
        if (round >= 5) value += 1;
        return (int)value;
    }
}