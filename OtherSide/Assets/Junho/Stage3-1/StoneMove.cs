using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;

    [SerializeField] private Stage3_1MG mg;

    [SerializeField] private Vector3 startStonePos;
    [SerializeField] private Vector3 endStonePos;
    [SerializeField] private GameObject stone;

    [SerializeField] private bool is1Floor;

    private float t;
    readonly float maxT = 1;

    private Vector3 startMousePos;
    private Vector3 endMousePos;

    private void OnMouseDrag()
    {
        if (p1.isWalking == true || p2.isWalking == true) return;

        endMousePos = Input.mousePosition;

        Vector3 dir = endMousePos - startMousePos;

        if(dir.x > 0)
        {
            t += Time.deltaTime;
        }
        else if(dir.x < 0)
        {
            t -= Time.deltaTime;
        }

        if(t >= maxT)
        {
            t = maxT;

            mg.EndChangeNode(is1Floor);
        }else if(t <= 0)
        {
            t = 0;
            mg.StartChangeNode(is1Floor);
        }
        else
        {
            mg.NodeCut(is1Floor);
        }

        stone.transform.position = Vector3.Lerp(startStonePos, endStonePos, t / maxT);
    }

    private void OnMouseDown()
    {
        startMousePos = Input.mousePosition;
    }
}
