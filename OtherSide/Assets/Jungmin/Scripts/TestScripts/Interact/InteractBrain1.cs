using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractBrain1 : MonoBehaviour
{
    [SerializeField] float rotateValue;

    public List<Interaction1> interactions;
    private bool isActive;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;  
            
            if(Physics.Raycast(ray, out Hit))
            {
                if(Hit.collider.GetComponent<InteractBrain>() != null)
                {
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
        transform.Rotate(Input.GetAxisRaw("Mouse Y") * rotateValue, 0f, 0f, Space.World);

        for (int i = 0; i < interactions.Count; i++)
        {
            interactions[i].transform.Rotate
                ((interactions[i].RotateDirection * Input.GetAxisRaw("Mouse Y")) * rotateValue, 0f, 0f, Space.Self);
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
        
        return (90 * angleMultiply) * abs;
    }

    private int RoundToInt(float value)
    {
        var round = (int)(value * 10) % 10;
        if (round >= 5) value += 1;
        return (int)value;
    }
}
