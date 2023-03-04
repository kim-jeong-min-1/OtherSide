using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2CheckPoints : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Player player1;
    [SerializeField] private Stage2ObjMg Stage2Objs;

    [SerializeField] private bool isWhite = true;
    [SerializeField] private bool isOn = false;
    private void Update()
    {
        if (isOn == true) return;

        if(isWhite == true)
        {
            if(player.currentNode == transform)
            {
                TRUE();
            }
        }
        else
        {
            if (player1.currentNode == transform)
            {
                TRUE();
            }
        }
    }

    private void TRUE()
    {
        isOn = true;
        Stage2Objs.CheckPoints++;
    }
}
